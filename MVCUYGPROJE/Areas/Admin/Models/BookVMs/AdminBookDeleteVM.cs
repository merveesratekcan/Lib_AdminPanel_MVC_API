namespace MVCUYGPROJE.Areas.Admin.Models.BookVMs
{
    public class AdminBookDeleteVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfPublish { get; set; }
        public bool IsAvailable { get; set; }
        public Guid AuthorId { get; set; }
        public Guid PublisherId { get; set; }
        public Guid CategoryId { get; set; }
    }
}
