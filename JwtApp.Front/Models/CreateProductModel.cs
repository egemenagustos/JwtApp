using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace JwtApp.Front.Models
{
    public class CreateProductModel
    {
        [Required(ErrorMessage = "ürün adı boş geçilemez")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "stok boş geçilemez")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "fiyat boş geçilemez")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "kategori seçimi yapmalısınız")]
        public int CategoryId { get; set; }

        public SelectList? Categories { get; set; }
    }
}
