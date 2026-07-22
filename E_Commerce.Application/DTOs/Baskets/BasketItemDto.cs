using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Application.DTOs.Baskets
{
    public class BasketItemDto
    {
        [Required(ErrorMessage ="Product Id Is Required")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Product Name Is Required")]
        public string Name { get; set; } = default!;
        public string PictureUrl { get; set; } = default!;
        [Range(0,double.MaxValue)]
        public decimal Price { get; set; }
        [Range(1,50)]
        public int Quantity { get; set; }
    }
}