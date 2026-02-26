using AutoMapper;
using IdentityProvider.Business.IServices;
using IdentityProvider.Data.Entities;
using IdentityProvider.Shared.Constants;
using IdentityProvider.Shared.Models.Request;
using IdentityProvider.Shared.Models.Response;
using Microsoft.EntityFrameworkCore;
using static IdentityProvider.Shared.ExceptionHandler.SpecificExceptions;

namespace IdentityProvider.Business.Services
{
    public class ProfileService : BaseService<User>, IProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProfileService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task UpdateProfileAsync(int userId, UpdateProfileRequest request)
        {
            var user = await _unitOfWork.UserRepository
                .GetFirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
                throw new NotFoundException(ApiMessages.NotFoundMessage("User"));

            if (!user.IsActive)
                throw new BadRequestException(ApiMessages.NotActive("User"));


            if (!string.IsNullOrEmpty(user.GoogleId))
                throw new BadRequestException(ApiMessages.GoogleUserNotAbleToUpdate);

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.SaveAsync();
        }

        public async Task<BasicProfileResponse> GetUserBasicProfile(int userId)
        {
            User? user = await GetFirstOrDefaultAsync(x => x.Id == userId, e => e.Include(x=>x.Role));
            if(user == null)
                throw new NotFoundException(ApiMessages.NotFoundMessage("User"));
            BasicProfileResponse profile = _mapper.Map<BasicProfileResponse>(user);
            return profile;
        }
    }
}
