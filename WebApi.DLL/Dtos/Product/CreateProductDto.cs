﻿using System.ComponentModel.DataAnnotations;

namespace WebApi.BLL.Dtos.Product
{
    public class CreateProductDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }        
        public decimal Price { get; set; }        
        public int Amount { get; set; }
    }
}
