using MVCUYGPROJE_Domain.Entities;
using MVCUYGPROJE_Infrastructure.AppContext;
using MVCUYGPROJE_Infrastructure.DataAccess.EntityFramework;
using MVCUYGPROJE_Infrastructure.DataAccess.Interfaces;
using MVCUYGPROJE_Infrastructure.Repositories.PublicherRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVCUYGPROJE_Infrastructure.Repositories.PublisherRepository;

public class PublisherRepository:EFBaseRepository<Publisher>,IPublisherRepository
{
   public PublisherRepository(AppDbContext context) : base(context)
    {

    }

   
}
