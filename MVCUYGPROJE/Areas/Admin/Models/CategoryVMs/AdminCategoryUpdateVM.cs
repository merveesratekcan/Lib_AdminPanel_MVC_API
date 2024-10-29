using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MVCUYGPROJE.Areas.Admin.Models.CategoryVMs
{
    public class AdminCategoryUpdateVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; } 
    }
}
