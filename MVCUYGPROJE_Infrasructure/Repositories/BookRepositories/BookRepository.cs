using MVCUYGPROJE_Domain.Entities;
using MVCUYGPROJE_Infrastructure.AppContext;
using MVCUYGPROJE_Infrastructure.DataAccess.EntityFramework;
using MVCUYGPROJE_Infrastructure.Repositories.CategoryRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCUYGPROJE_Infrastructure.Repositories.BookRepositories;

public class BookRepository : EFBaseRepository<Book>, IBookRepository
{
    public BookRepository(AppDbContext context) : base(context)
    {

    }
}



