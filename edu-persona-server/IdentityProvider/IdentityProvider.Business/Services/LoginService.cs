using AutoMapper;
using Google.Apis.Auth;
using IdentityProvider.Business.IServices;
using IdentityProvider.Data.Entities;
using IdentityProvider.Shared.Constants;
using IdentityProvider.Shared.Helper;
using IdentityProvider.Shared.Models.Helper;
using IdentityProvider.Shared.Models.Request;
using IdentityProvider.Shared.Models.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using static IdentityProvider.Shared.ExceptionHandler.SpecificExceptions;

namespace IdentityProvider.Business.Services
{
    public class LoginService : BaseService<User>, ILoginService
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public LoginService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration) : base(unitOfWork)
        {
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<IdpLoginResponse> LoginAsync(LoginRequest loginRequest)
        {
            User? user = await GetFirstOrDefaultAsync(x => x.Email == loginRequest.Email && x.IsActive, query => query.Include(x => x.Role));
            if (user == null)
                throw new BadRequestException(ApiMessages.NotFoundMessage("User"));

            bool isPwdVerify = PasswordHelper.Verify(loginRequest.Password, user.PasswordHash);

            if (!isPwdVerify)
                throw new BadRequestException(ApiMessages.InvalidMessage("Password"));

            //Get session 
            Session? storedSession = await _unitOfWork.SessionRepository.GetFirstOrDefaultAsync(x => x.UserId == user.Id && x.IsActive && x.ExpiredAt > DateTime.UtcNow);

            if (storedSession == null)
            {
                //create session
                await InActivePreviousSession(user.Id);
                Session session = await CreateSession(user.Id);
                IdpLoginResponse loginResponse = new IdpLoginResponse
                {
                    SessionId = session.Id
                };
                return loginResponse;
            }

            IdpLoginResponse idpLoginResponse = new IdpLoginResponse
            {
                SessionId = storedSession.Id,
            };

            return idpLoginResponse;
        }

        public async Task<LoginResponse> ValidateSessionAsync(int sessionId)
        {
            Session? session = await _unitOfWork.SessionRepository.GetFirstOrDefaultAsync(x => x.Id == sessionId && x.IsActive && x.ExpiredAt > DateTime.UtcNow,
            query => query.Include(x => x.User).ThenInclude(x => x.Role));

            if (session == null)
                throw new BadRequestException(ApiMessages.InvalidMessage("Session"));

            RefreshToken? storedRefreshToken = await _unitOfWork.RefreshTokenRepository.GetFirstOrDefaultAsync(x => x.SessionId == sessionId && !x.Revoked);

            //generate access and refresh token
            string accessToken = await GenerateAccessToken(session.User);
            string refreshToken = RefreshTokenGenerator.GenerateRefreshToken();
            if (storedRefreshToken == null)
            {
                //create refresh token
                await CreateRefreshToken(session.UserId, session.Id, refreshToken);
            }

            LoginResponse loginResponse = new LoginResponse
            {
                AccessToken = accessToken,
                RefreshToken = storedRefreshToken == null ? refreshToken : storedRefreshToken.Token,
                SessionId = session.Id,
                UserInfo = _mapper.Map<UserInfo>(session.User)
            };

            return loginResponse;
        }

        public async Task<AccessTokenByRefreshTokenResponse> GetAccessTokenAsync(string refreshToken)
        {
            RefreshToken? token = await _unitOfWork.RefreshTokenRepository.GetFirstOrDefaultAsync(x => x.Token == refreshToken && !x.Revoked,
            query => query.Include(x => x.User).ThenInclude(x => x.Role));

            Session session = await _unitOfWork.SessionRepository.GetFirstOrDefaultAsync(s => s.Id == token.SessionId);

            if (token == null)
                throw new BadRequestException(ApiMessages.NotFoundMessage("RefreshToken"));

            if (token.ExpiredAt < DateTime.UtcNow)
                throw new BadRequestException(ApiMessages.RefreshTokenExpired);

            if (session == null)
                throw new BadRequestException(ApiMessages.NotFoundMessage("Session"));

            if (session.ExpiredAt < DateTime.UtcNow && session.IsActive)
                throw new BadRequestException(ApiMessages.SessionExpired);

            string newAccessToken = await GenerateAccessToken(token.User);

            session.ExpiredAt = DateTime.UtcNow.AddMinutes(5);
            await _unitOfWork.SessionRepository.UpdateAsync(session);

            token.ExpiredAt = DateTime.UtcNow.AddMinutes(5);
            await _unitOfWork.RefreshTokenRepository.UpdateAsync(token);
            await _unitOfWork.SaveAsync();

            AccessTokenByRefreshTokenResponse response = new AccessTokenByRefreshTokenResponse
            {
                NewAccessToken = newAccessToken,
                RefreshTokenExpiredAt = token.ExpiredAt,
                SessionExpiredAt = session.ExpiredAt,
                RefreshToken = token.Token,
                SessionId = session.Id
            };

            return response;
        }

        public async Task LogoutAsync(int userID)
        {
            // Get all active sessions of the user
            var sessions = await _unitOfWork.SessionRepository
                .FindAsync(x => x.UserId == userID && x.IsActive);

            foreach (var session in sessions)
            {
                session.IsActive = false;
                session.ExpiredAt = DateTime.UtcNow;
            }

            // Get all non-revoked refresh tokens linked to those sessions
            var sessionIds = sessions.Select(s => s.Id).ToList();

            var refreshTokens = await _unitOfWork.RefreshTokenRepository
                .FindAsync(x => sessionIds.Contains(x.SessionId) && !x.Revoked);

            await _unitOfWork.RefreshTokenRepository.DeleteRangeAsync(refreshTokens);
            await _unitOfWork.SessionRepository.UpdateRangeAsync(sessions);

            await _unitOfWork.SaveAsync();
        }

        public async Task<IdpLoginResponse> GoogleLoginAsync(string code)
        {
            var googleToken = await ExchangeCodeAsync(code);

            var payload = await ValidateGoogleTokenAsync(googleToken.id_token);

            var user = await GetOrCreateGoogleUserAsync(payload);

            return await CreateOrReuseSessionAsync(user.Id);
        }

        private async Task<GoogleTokenResponse> ExchangeCodeAsync(string code)
        {
            var clientId = _configuration["GoogleAuth:ClientId"];
            var clientSecret = _configuration["GoogleAuth:ClientSecret"];
            var redirectUri = _configuration["GoogleAuth:RedirectUri"];

            using var httpClient = new HttpClient();

            var values = new Dictionary<string, string>
            {
                { "code", code },
                { "client_id", clientId! },
                { "client_secret", clientSecret! },
                { "redirect_uri", redirectUri! },
                { "grant_type", GoogleAuthConstants.GrantType }
            };

            var response = await httpClient.PostAsync(
                GoogleAuthConstants.TokenEndpoint,
                new FormUrlEncodedContent(values));

            if (!response.IsSuccessStatusCode)
                throw new BadRequestException(ApiMessages.TokenExchangeFailed);

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<GoogleTokenResponse>(json);

            if (result == null || string.IsNullOrEmpty(result.id_token))
                throw new BadRequestException(ApiMessages.InvalidTokenResponse);

            return result;
        }

        private async Task<GoogleJsonWebSignature.Payload> ValidateGoogleTokenAsync(string idToken)
        {
            var clientId = _configuration["GoogleAuth:ClientId"];

            var payload = await GoogleJsonWebSignature.ValidateAsync(
                idToken,
                new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { clientId }
                });

            if (!payload.EmailVerified)
                throw new BadRequestException(ApiMessages.EmailNotVerified);

            return payload;
        }

        private async Task<User> GetOrCreateGoogleUserAsync(GoogleJsonWebSignature.Payload payload)
        {
            string email = payload.Email;
            string googleId = payload.Subject;

            var user = await GetFirstOrDefaultAsync(
                x => x.GoogleId == googleId || x.Email == email,
                query => query.Include(x => x.Role));

            if (user == null)
            {
                user = new User
                {
                    Email = email,
                    GoogleId = googleId,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    RoleId = 2,
                    FirstName = payload.Name.Split(" ")[0],
                    LastName = payload.Name.Split(" ")[1]
                };

                await AddAsync(user);
                await _unitOfWork.SaveAsync();

                return user;
            }

            if (!string.IsNullOrEmpty(user.GoogleId) &&
                user.GoogleId != googleId)
                throw new BadRequestException(ApiMessages.AccountMismatch);

            if (string.IsNullOrEmpty(user.GoogleId))
            {
                user.GoogleId = googleId;
                await _unitOfWork.SaveAsync();
            }

            return user;
        }

        private async Task<IdpLoginResponse> CreateOrReuseSessionAsync(int userId)
        {
            var storedSession =
                await _unitOfWork.SessionRepository.GetFirstOrDefaultAsync(
                    x => x.UserId == userId &&
                         x.IsActive &&
                         x.ExpiredAt > DateTime.UtcNow);

            if (storedSession == null)
            {
                var session = await CreateSession(userId);

                return new IdpLoginResponse
                {
                    SessionId = session.Id
                };
            }

            return new IdpLoginResponse
            {
                SessionId = storedSession.Id
            };
        }

        private async Task<string> GenerateAccessToken(User user)
        {
            GenerateTokenRequest tokenRequest = new GenerateTokenRequest
            {
                Email = user.Email,
                UserId = user.Id,
                Role = user.Role.Name
            };

            return JwtTokenService.GenerateToken(tokenRequest, _configuration);
        }

        private async Task<Session> CreateSession(int userID)
        {
            Session session = new Session
            {
                UserId = userID,
                ExpiredAt = DateTime.UtcNow.AddMinutes(5)
            };

            Session sessionResponse = await _unitOfWork.SessionRepository.AddAsync(session);

            await _unitOfWork.SaveAsync();

            return sessionResponse;
        }

        private async Task CreateRefreshToken(int userID, int sessionId, string refreshToken)
        {
            RefreshToken refreshTokenData = new RefreshToken
            {
                UserId = userID,
                SessionId = sessionId,
                ExpiredAt = DateTime.UtcNow.AddMinutes(5),
                Token = refreshToken
            };

            await _unitOfWork.RefreshTokenRepository.AddAsync(refreshTokenData);

            await _unitOfWork.SaveAsync();
        }

        private async Task InActivePreviousSession(int userId)
        {
            IEnumerable<Session> sessions = await _unitOfWork.SessionRepository.FindAsync(s => s.UserId == userId && s.IsActive);
            await _unitOfWork.SessionRepository.UpdateRangeAsync(sessions);
            await _unitOfWork.SaveAsync();
        }
    }
}