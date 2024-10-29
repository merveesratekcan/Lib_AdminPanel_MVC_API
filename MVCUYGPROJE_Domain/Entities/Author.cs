

namespace MVCUYGPROJE_Domain.Entities;

public class Author: AuditableEntity
{
    public Author()
    {
        Books = new HashSet<Book>();

    }
    //Navigation Properties
    public virtual IEnumerable<Book> Books { get; set; }
    public string Name { get; set; }    
    public DateTime BirthDate { get; set; }
    
}
