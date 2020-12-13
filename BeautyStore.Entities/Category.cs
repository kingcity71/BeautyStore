using BeautyStore.Entities.Abstract;
using System;

namespace BeautyStore.Entities
{
    public class Category : Entity
    {
        public string Title { get; set; }
        public Guid? ParentCategoryId { get; set; }
    }
}
