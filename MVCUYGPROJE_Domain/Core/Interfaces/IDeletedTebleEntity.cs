using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCUYGPROJE_Domain.Core.Interfaces;

public interface IDeletedTebleEntity
{
    public string? DeletedBy { get; set; }
    public DateTime? DeletedDate { get; set; }

}
