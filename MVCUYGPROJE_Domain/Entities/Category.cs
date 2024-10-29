

namespace MVCUYGPROJE_Domain.Entities
{
    public class Category:AuditableEntity
    {
        public Category()
        {
            Books = new HashSet<Book>();

        }
        //Navigation Properties
        public virtual IEnumerable<Book> Books { get; set; }
        public string Name { get; set; }
       
    }
}
