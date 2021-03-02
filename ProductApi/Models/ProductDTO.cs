using System.ComponentModel.DataAnnotations;

namespace ProductApi.Models
{
    public class ProductDTO
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        public double Price { get; set; }
    }
}