namespace HelloApi.Models.DTOs
{
    public class OrderUpdateDto
    {
        public required int Id { get; set; }
        public required int PersonId { get; set; }
        public required int Number { get; set; }
        public required int UpdatedBy { get; set; }
    }
}
