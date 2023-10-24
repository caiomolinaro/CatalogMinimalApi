using System.Text.Json.Serialization;

namespace CatalogMinimalApi.Models
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        [JsonIgnore]
        public ICollection<ProductModel> Products { get; set; }
    }
}
