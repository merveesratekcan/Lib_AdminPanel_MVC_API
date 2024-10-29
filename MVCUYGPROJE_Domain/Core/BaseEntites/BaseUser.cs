

namespace MVCUYGPROJE_Domain.Core.BaseEntites;

public class BaseUser: AuditableEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string IdentityId { get; set; }
}
