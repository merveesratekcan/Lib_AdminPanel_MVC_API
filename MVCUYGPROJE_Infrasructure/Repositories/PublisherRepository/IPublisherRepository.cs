using MVCUYGPROJE_Domain.Entities;
using MVCUYGPROJE_Infrastructure.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCUYGPROJE_Infrastructure.Repositories.PublicherRepository;

public interface IPublisherRepository : IAsyncRepository, IAsyncInsertable<Publisher>, IAsyncFindableRepository<Publisher>,
IAsyncQueryableRepository<Publisher>, IAsyncUpdatableRepository<Publisher>, IAsyncDeletableRepository<Publisher>
{
}
