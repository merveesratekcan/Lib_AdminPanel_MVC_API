
using MVCUYGPROJE_Infrastructure.AppContext;
using MVCUYGPROJE_Infrastructure.DataAccess.EntityFramework;


namespace MVCUYGPROJE_Infrastructure.Repositories.AdminRepository
{
    public class AdminRepository : EFBaseRepository<Admin>, IAdminRepository
    {
        public AdminRepository(AppDbContext context) : base(context)
        {

        }

        public Task<Admin?> GetByIdentityId(string identityId)
        {
            return _table.FirstOrDefaultAsync(x=>x.IdentityId==identityId);
        }
    }
   
}
