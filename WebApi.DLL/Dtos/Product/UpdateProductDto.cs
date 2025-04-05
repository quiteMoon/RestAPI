namespace WebApi.BLL.Dtos.Product
{
    public class UpdateProductDto
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }        
        public decimal Price { get; set; }        
        public int Amount { get; set; }
    }
}
