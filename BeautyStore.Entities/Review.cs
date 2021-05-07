using BeautyStore.Entities.Abstract;
using System;

namespace BeautyStore.Entities
{
    public class Review : Entity
    {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public int Stars { get; set; }
        public string Comment { get; set; }
    }
}
