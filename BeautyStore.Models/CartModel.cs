using BeautyStore.Models.Abstract;
using BeautyStore.Models.Enum;
using System;

namespace BeautyStore.Models
{
    public class CartModel : Model
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public BasketStatus Status { get; set; }
    }
}
