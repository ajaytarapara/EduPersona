using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduPersona.Core.Data.Entities;
using EduPersona.Core.Data.IRepositories;
using EduPersona.Core.Data.Repositories.EduPersona.Core.Data.Repositories;

namespace EduPersona.Core.Data.Repositories
{
    public class SkillRepository : BaseRepository<Skill>, ISkillRepository
    {
        public SkillRepository(AppDbContext context) : base(context)
        {

        }

    }
}