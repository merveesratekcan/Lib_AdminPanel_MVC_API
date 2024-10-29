using MVCUYGPROJE_Domain.Entities;
using MVCUYGPROJE_Infrastructure.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCUYGPROJE_Infrastructure.Repositories.ProfileUserRepository;

public interface IProfileUserRepository : IAsyncRepository, IAsyncInsertable<ProfileUser>, IAsyncFindableRepository<ProfileUser>,
  IAsyncQueryableRepository<ProfileUser>, IAsyncUpdatableRepository<ProfileUser>, IAsyncDeletableRepository<ProfileUser>, IAsyncTransactionRepository
{
    Task<ProfileUser?> GetByIdentityId(string identityId);
}

