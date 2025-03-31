using System.ComponentModel.DataAnnotations;

namespace WebApi.DAL.Entities
{
    public class CategoryEntity
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [MaxLength(255)]
        [Required]
        public string Name { get; set; } = string.Empty;
        public ICollection<ProductEntity> Products { get; set; } = [];
    }
}
