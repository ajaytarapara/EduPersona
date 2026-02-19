using AutoMapper;
using IdentityProvider.Business.IServices;
using IdentityProvider.Data.Entities;
using IdentityProvider.Shared.Constants;
using IdentityProvider.Shared.Helper;
using IdentityProvider.Shared.Models.Helper;
using IdentityProvider.Shared.Models.Request;
using IdentityProvider.Shared.Models.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

        public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
        {
            User? user = await GetFirstOrDefaultAsync(x => x.Email == loginRequest.Email && x.IsActive, query => query.Include(x => x.Role));
            if (user == null)
                throw new BadRequestException(ApiMessages.NotFoundMessage("User"));

            bool isPwdVerify = PasswordHelper.Verify(loginRequest.Password, user.PasswordHash);

            if (!isPwdVerify)
                throw new BadRequestException(ApiMessages.InvalidPassword);

            //generate access and refresh token
            string accessToken = await GenerateAccessToken(user);
            string refreshToken = RefreshTokenGenerator.GenerateRefreshToken();

            //create session
            Session session = await CreateSession(user.Id);

            //create refresh token
            await CreateRefreshToken(user.Id, session.Id, refreshToken);

            LoginResponse loginResponse = new LoginResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                SessionId = session.Id,
                UserInfo = _mapper.Map<UserInfo>(user)
            };

            return loginResponse;
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
                ExpiredAt = DateTime.UtcNow.AddHours(8)
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
                ExpiredAt = DateTime.UtcNow.AddDays(7),
                Token = refreshToken
            };

            await _unitOfWork.RefreshTokenRepository.AddAsync(refreshTokenData);

            await _unitOfWork.SaveAsync();
        }
    }
}