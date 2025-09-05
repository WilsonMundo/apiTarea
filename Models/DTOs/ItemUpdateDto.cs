namespace HelloApi.Models.DTOs
{
    public class ItemUpdateDto
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public required int UpdatedBy { get; set; }
    }
}
