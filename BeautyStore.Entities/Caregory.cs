using BeautyStore.Entities.Abstract;
using System;

namespace BeautyStore.Entities
{
    public class Caregory : Entity
    {
        public string Title { get; set; }
        public Guid? ParentCategoryId { get; set; }
    }
}
