using BeautyStore.Entities.Abstract;
using System;

namespace BeautyStore.Entities
{
    public class ProductPhoto : Entity
    {
        public Guid ProductId { get; set; }
        public Guid PhotoId { get; set; }
        public int DisplayOrder { get; set; }
    }
}
