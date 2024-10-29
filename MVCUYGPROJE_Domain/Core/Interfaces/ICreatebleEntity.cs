using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCUYGPROJE_Domain.Core.Interfaces;

public interface ICreatebleEntity:IEntity
{
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
}
