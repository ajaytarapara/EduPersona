using EduPersona.Core.Shared.Models.Request;
using EduPersona.Core.Shared.Models.Response;

namespace EduPersona.Core.Business.IServices
{
    public interface IMasterDataService
    {
        // Profession
        Task<IEnumerable<DropdownResponse>> GetProfessionDropdownAsync();
        Task AddProfessionAsync(ProfessionRequest request);
        Task UpdateProfessionAsync(int id, ProfessionRequest request);
        Task DeleteProfessionAsync(int id);

        // Skill
        Task<IEnumerable<DropdownResponse>> GetSkillDropdownAsync();
        Task AddSkillAsync(SkillRequest request);
        Task UpdateSkillAsync(int id, SkillRequest request);
        Task DeleteSkillAsync(int id);

        // Designation
        Task<IEnumerable<DropdownResponse>> GetDesignationByProfessionAsync(int professionId);
        Task AddDesignationAsync(DesignationRequest request);
        Task UpdateDesignationAsync(int id, DesignationRequest request);
        Task DeleteDesignationAsync(int id);
    }
}
