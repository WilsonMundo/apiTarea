namespace HelloApi.Models.DTOs
{
    public class OrderDetailCreateDto
    {
        public required int ItemId { get; set; }
        public required int Quantity { get; set; }        
        public decimal? Price { get; set; }
    }
}
