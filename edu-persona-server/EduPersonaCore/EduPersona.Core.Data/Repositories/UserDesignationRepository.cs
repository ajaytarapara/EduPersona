using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduPersona.Core.Data.Entities;
using EduPersona.Core.Data.IRepositories;
using EduPersona.Core.Data.Repositories.EduPersona.Core.Data.Repositories;

namespace EduPersona.Core.Data.Repositories
{
    public class UserDesignationRepository : BaseRepository<UserDesignation>, IUserDesignationRepository
    {
        public UserDesignationRepository(AppDbContext context) : base(context)
        {

        }

    }
}