namespace CrwnClothing.BLL.DTOs
{
    public class CategoryDTO
    {

        public int? Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CategoryId { get; set; }
        public List<ProductDTO> Products { get; set; } = new List<ProductDTO>();
    }
}
