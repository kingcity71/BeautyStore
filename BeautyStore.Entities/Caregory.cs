using System;

namespace BeautyStore.Entities
{
    public class Caregory : User
    {
        public string Title { get; set; }
        public Guid? ParentCategoryId { get; set; }
    }
}
