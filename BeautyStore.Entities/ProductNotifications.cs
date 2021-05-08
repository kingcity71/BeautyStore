using BeautyStore.Entities.Abstract;
using System;

namespace BeautyStore.Entities
{
    public class ProductNotifications:Entity
    {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
    }
}
