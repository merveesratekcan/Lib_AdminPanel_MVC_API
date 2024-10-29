using MVCUYGPROJE_Domain.Entities;
using MVCUYGPROJE_Infrastructure.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCUYGPROJE_Infrastructure.Repositories.CategoryRepositories;

public interface ICategoryRepository:IAsyncRepository,IAsyncInsertable<Category>,IAsyncFindableRepository<Category>, 
  IAsyncQueryableRepository<Category>, IAsyncUpdatableRepository<Category>, IAsyncDeletableRepository<Category>
{
}

