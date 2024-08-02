using System.ComponentModel.DataAnnotations;

namespace WebApp.Services.CategoryService.Dto
{
    public class CategoryDto
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;
        public string CategoryType { get; set; } = null!;
    }
}
