using MVCUYGPROJE_Domain.Entities;
using MVCUYGPROJE_Infrastructure.AppContext;
using MVCUYGPROJE_Infrastructure.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCUYGPROJE_Infrastructure.Repositories.CategoryRepositories;

public class CategoryRepository:EFBaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context): base(context)
    {
        
    }
}
