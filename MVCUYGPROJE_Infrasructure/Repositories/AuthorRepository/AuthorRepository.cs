using MVCUYGPROJE_Domain.Entities;
using MVCUYGPROJE_Infrastructure.AppContext;
using MVCUYGPROJE_Infrastructure.DataAccess.EntityFramework;
using MVCUYGPROJE_Infrastructure.Repositories.CategoryRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCUYGPROJE_Infrastructure.Repositories.AuthorRepository;

public class AuthorRepository : EFBaseRepository<Author>, IAuthorRepository
{
    public AuthorRepository(AppDbContext context) : base(context)
    {

    }
}

