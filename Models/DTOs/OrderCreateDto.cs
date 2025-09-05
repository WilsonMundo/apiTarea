namespace HelloApi.Models.DTOs
{
    public class OrderCreateDto
    {
        public required int PersonId { get; set; }
        public required int Number { get; set; }
        public required int CreatedBy { get; set; }
        public List<OrderDetailCreateDto> Details { get; set; } = new();
    }
}
