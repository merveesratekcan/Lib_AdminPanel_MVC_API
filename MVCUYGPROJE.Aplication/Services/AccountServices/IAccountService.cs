using Microsoft.AspNetCore.Identity;
using MVCUYGPROJE_Domain.Entities;
using MVCUYGPROJE_Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVCUYGPROJE.Aplication.Services.AccountServices;

public interface IAccountService
{
    Task<bool>AnyAsync(Expression<Func<IdentityUser, bool>> predicate);
    Task<IdentityUser?> FindByIdAsync(string identityId);
    Task<IdentityResult> CreateUserAsync(IdentityUser user, Roles role);
    Task<IdentityResult> DeleteUserAsync(string identityId);
    Task<Guid> GetUserIdAsync(string identityId,string role);
    Task<IdentityResult> UpdateUserAsync(IdentityUser user);

}
