using Microsoft.EntityFrameworkCore;
using MVCUYGPROJE_Domain.Entities;
using MVCUYGPROJE_Infrastructure.AppContext;
using MVCUYGPROJE_Infrastructure.DataAccess.EntityFramework;
using MVCUYGPROJE_Infrastructure.Repositories.CategoryRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCUYGPROJE_Infrastructure.Repositories.ProfileUserRepository;

public class ProfileUserRepository : EFBaseRepository<ProfileUser>, IProfileUserRepository
{
    public ProfileUserRepository(AppDbContext context) : base(context)
    {

    }

    public Task<ProfileUser?> GetByIdentityId(string identityId)
    {
        return _table.FirstOrDefaultAsync(x => x.IdentityId == identityId);
    }
}
