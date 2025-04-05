﻿using System.ComponentModel.DataAnnotations;

namespace WebApi.DAL.Entities
{
    public class CategoryEntity : BaseEntity<string>
    {
        public override string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        [MaxLength(255)]
        public required string Name { get; set; }
        [Required]
        [MaxLength(255)]
        public string? NormalizedName { get; set; }
        [MaxLength]
        public string? Description { get; set; }
        [MaxLength(255)]
        public string? Image { get; set; }
        public ICollection<ProductEntity> Products { get; set; } = [];
    }
}
