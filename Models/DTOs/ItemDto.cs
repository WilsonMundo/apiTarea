namespace HelloApi.Models.DTOs
{
    public class ItemDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
