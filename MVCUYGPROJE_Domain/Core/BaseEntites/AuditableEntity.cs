

namespace MVCUYGPROJE_Domain.Core.BaseEntites;

public class AuditableEntity: BaseEntity, IDeletedTebleEntity
{

    public string? DeletedBy { get; set; }
    public DateTime? DeletedDate { get; set; }
}

