namespace HelloApi.Models.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public required int PersonId { get; set; }
        public required int Number { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<OrderDetailDto> Details { get; set; } = new();
    }
}
