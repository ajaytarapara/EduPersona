using EduPersona.Core.Business.IServices;
using EduPersona.Core.Data.Entities;
using EduPersona.Core.Shared.Constants;
using EduPersona.Core.Shared.Models.Request;
using EduPersona.Core.Shared.Models.Response;
using Microsoft.EntityFrameworkCore;
using static EduPersona.Core.Shared.Constants.Messages;
using static EduPersona.Core.Shared.ExceptionHandler.SpecificExceptions;

namespace EduPersona.Core.Business.Services
{
    public class MasterDataService : IMasterDataService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MasterDataService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Profession

        public async Task<IEnumerable<DropdownResponse>> GetProfessionDropdownAsync()
        {
            var data = await _unitOfWork.ProfessionRepository
                .FindAsync(x => !x.IsDeleted && x.IsActive);

            return data.Select(x => new DropdownResponse(x.Id, x.Name));
        }

        public async Task AddProfessionAsync(ProfessionRequest request)
        {
            bool exists = await _unitOfWork.ProfessionRepository
                .ExistsAsync(x => x.Name == request.Name && !x.IsDeleted);

            if (exists)
                throw new BadRequestException(ErrorMessage.AlreadyExistsMessage("Profession"));

            var entity = new Profession
            {
                Name = request.Name,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.ProfessionRepository.AddAsync(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateProfessionAsync(int id, ProfessionRequest request)
        {
            var entity = await _unitOfWork.ProfessionRepository.GetByIdAsync(id);

            if (entity == null || entity.IsDeleted)
                throw new BadRequestException(ErrorMessage.NotFoundMessage("Profession"));

            entity.Name = request.Name;
            entity.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.ProfessionRepository.UpdateAsync(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteProfessionAsync(int id)
        {
            var entity = await _unitOfWork.ProfessionRepository.GetByIdAsync(id);

            if (entity == null)
                throw new BadRequestException(ErrorMessage.NotFoundMessage("Profession"));

            entity.IsDeleted = true;
            entity.IsActive = false;
            entity.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.ProfessionRepository.UpdateAsync(entity);
            await _unitOfWork.SaveAsync();
        }

        #endregion


        #region Skill

        public async Task<IEnumerable<DropdownResponse>> GetSkillDropdownAsync()
        {
            var data = await _unitOfWork.SkillRepository
                .FindAsync(x => !x.IsDeleted && x.IsActive);

            return data.Select(x => new DropdownResponse(x.Id, x.Name));
        }

        public async Task AddSkillAsync(SkillRequest request)
        {
            var entity = new Skill
            {
                Name = request.Name,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.SkillRepository.AddAsync(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateSkillAsync(int id, SkillRequest request)
        {
            var entity = await _unitOfWork.SkillRepository.GetByIdAsync(id);

            if (entity == null || entity.IsDeleted)
                throw new BadRequestException(ErrorMessage.NotFoundMessage("Skill"));

            entity.Name = request.Name;
            entity.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.SkillRepository.UpdateAsync(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteSkillAsync(int id)
        {
            var entity = await _unitOfWork.SkillRepository.GetByIdAsync(id);

            if (entity == null)
                throw new BadRequestException(ErrorMessage.NotFoundMessage("Skill"));

            entity.IsDeleted = true;
            entity.IsActive = false;
            entity.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.SkillRepository.UpdateAsync(entity);
            await _unitOfWork.SaveAsync();
        }

        #endregion


        #region Designation

        public async Task<IEnumerable<DropdownResponse>> GetDesignationByProfessionAsync(int professionId)
        {
            var data = await _unitOfWork.DesignationRepository
                .FindAsync(x =>
                    x.ProfessionId == professionId &&
                    !x.IsDeleted &&
                    x.IsActive);

            return data.Select(x => new DropdownResponse(x.Id, x.Name));
        }

        public async Task AddDesignationAsync(DesignationRequest request)
        {
            // Check if profession exists
            var professionExists = await _unitOfWork.ProfessionRepository
                .ExistsAsync(x => x.Id == request.ProfessionId && !x.IsDeleted);

            if (!professionExists)
               throw new BadRequestException(ErrorMessage.NotFoundMessage("Profession"));

            // Check duplicate inside same profession
            bool exists = await _unitOfWork.DesignationRepository
                .ExistsAsync(x =>
                    x.Name.ToLower() == request.Name.ToLower() &&
                    x.ProfessionId == request.ProfessionId &&
                    !x.IsDeleted);

            if (exists)
                throw new BadRequestException(ErrorMessage.AlreadyExistsMessage("Designation").
                    Split(".")[0] +" in this profession.");

            var entity = new Designation
            {
                Name = request.Name.Trim(),
                ProfessionId = request.ProfessionId,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.DesignationRepository.AddAsync(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateDesignationAsync(int id, DesignationRequest request)
        {
            var entity = await _unitOfWork.DesignationRepository.GetByIdAsync(id);

            if (entity == null || entity.IsDeleted)
                throw new BadRequestException(ErrorMessage.NotFoundMessage("Designation"));

            bool exists = await _unitOfWork.DesignationRepository
                .ExistsAsync(x =>
                    x.Id != id &&
                    x.Name.ToLower() == request.Name.ToLower() &&
                    x.ProfessionId == request.ProfessionId &&
                    !x.IsDeleted);

            if (exists)
                throw new BadRequestException(ErrorMessage.AlreadyExistsMessage("Designation").
                      Split(".")[0] + " in this profession.");

            entity.Name = request.Name.Trim();
            entity.ProfessionId = request.ProfessionId;
            entity.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.DesignationRepository.UpdateAsync(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteDesignationAsync(int id)
        {
            var entity = await _unitOfWork.DesignationRepository.GetByIdAsync(id);

            if (entity == null)
                throw new BadRequestException(ErrorMessage.NotFoundMessage("Designation"));

            entity.IsDeleted = true;
            entity.IsActive = false;
            entity.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.DesignationRepository.UpdateAsync(entity);
            await _unitOfWork.SaveAsync();
        }

        #endregion
    }
}
