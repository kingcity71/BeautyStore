using BeautyStore.Entities.Abstract;
using System;

namespace BeautyStore.Entities
{
    public class Product : Entity
    {
        public Guid CategoryId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
    }
}
