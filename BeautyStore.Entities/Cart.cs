using BeautyStore.Entities.Abstract;
using BeautyStore.Entities.Enum;
using System;

namespace BeautyStore.Entities
{
    public class Cart : Entity
    {
        public Guid UserId { get; set; }
        public Guid BranchId { get; set; }
        public BasketStatus Status { get; set; }
    }
}
