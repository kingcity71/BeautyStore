using BeautyStore.Entities.Abstract;
using System;

namespace BeautyStore.Entities
{
    public class CartProduct : Entity
    {
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public int Count { get; set; }
    }
}
