using MVCUYGPROJE.Areas.ProfileUser.Controllers;

namespace MVCUYGPROJE.Areas.ProfileUser.Models.BookListVMs
{
    public class BookListVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfPublish { get; set; }
        public bool IsAvailable { get; set; }
        public string AuthorName { get; set; }
        public string PublisherName { get; set; }
        public string CategoryName { get; set; }
    }
}
