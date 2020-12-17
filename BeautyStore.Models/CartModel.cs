using BeautyStore.Models.Abstract;
using BeautyStore.Models.Enum;
using System;

namespace BeautyStore.Models
{
    public class CartModel : Model
    {
        public ProductModel Product { get; set; }
        public Guid ProductId { get; set; }
        public BasketStatus Status { get; set; }
        public Guid UserId { get; set; }
    }
}
