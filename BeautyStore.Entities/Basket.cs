using BeautyStore.Entities.Abstract;
using BeautyStore.Entities.Enum;
using System;

namespace BeautyStore.Entities
{
    public class Basket : Entity
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public BasketStatus Status { get; set; }
    }
}
