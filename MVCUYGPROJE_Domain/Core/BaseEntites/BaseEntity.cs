
using MVCUYGPROJE_Domain.Enums;


namespace MVCUYGPROJE_Domain.Core.BaseEntites;

public class BaseEntity : IUpdatebleEntity
{
    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid Id { get; set; }= Guid.NewGuid();
    public Status Status { get; set; }
}
