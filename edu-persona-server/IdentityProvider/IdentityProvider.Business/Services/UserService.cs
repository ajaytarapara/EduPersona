using AutoMapper;
using IdentityProvider.Business.IServices;
using IdentityProvider.Data.Entities;
using IdentityProvider.Shared.Constants;
using IdentityProvider.Shared.Helper;
using IdentityProvider.Shared.Models.Request;
using static IdentityProvider.Shared.ExceptionHandler.SpecificExceptions;

namespace IdentityProvider.Business.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper): base(unitOfWork)
        {
            _mapper = mapper;
        }
        public async Task<User> RegisterAsync(Register request)
        {
            bool exists = await ExistsAsync(x => x.Email == request.Email && x.IsActive);

            if (exists)
                throw new BadRequestException(ApiMessages.UserAlreadyExists);

            User user = _mapper.Map<User>(request);
     
            string passwordHash = PasswordHelper.CreateHash(request.Password!);
            user.PasswordHash = passwordHash;

            return await AddAsync(user);
        }


    }
}
