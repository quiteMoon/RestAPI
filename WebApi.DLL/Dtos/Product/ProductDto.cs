﻿namespace WebApi.BLL.Dtos.Product
{
    public class ProductDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
    }
}
