

namespace MVCUYGPROJE_Domain.Entities;

public class Publisher:AuditableEntity
{
    public Publisher()
    {
        Books = new HashSet<Book>();

    }
    //Navigation Properties
    public virtual IEnumerable<Book> Books { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
   
}
