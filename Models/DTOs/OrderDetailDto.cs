namespace HelloApi.Models.DTOs
{
    public class OrderDetailDto
    {
        public int Id { get; set; }
        public required int ItemId { get; set; }
        public required string ItemName { get; set; }
        public required int Quantity { get; set; }
        public required decimal Price { get; set; }
        public required decimal Total { get; set; }
    }
}
