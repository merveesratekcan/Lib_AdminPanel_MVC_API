using MVCUYGPROJE_Domain.Entities;
using MVCUYGPROJE_Infrastructure.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCUYGPROJE_Infrastructure.Repositories.BookRepositories;

public interface IBookRepository : IAsyncRepository, IAsyncInsertable<Book>, IAsyncFindableRepository<Book>,
  IAsyncQueryableRepository<Book>,
  IAsyncUpdatableRepository<Book>,
  IAsyncDeletableRepository<Book>
{
}
