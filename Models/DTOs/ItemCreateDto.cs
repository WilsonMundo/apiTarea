namespace HelloApi.Models.DTOs
{
    public class ItemCreateDto
    {
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public required int CreatedBy { get; set; }
    }

}
