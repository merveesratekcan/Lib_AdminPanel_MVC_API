using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCUYGPROJE_Domain.Core.Interfaces;

public interface IUpdatebleEntity:ICreatebleEntity
{
    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
}
