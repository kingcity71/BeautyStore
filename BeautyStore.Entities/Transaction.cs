using System;
using BeautyStore.Entities.Abstract;
using BeautyStore.Entities.Enum;

namespace BeautyStore.Entities
{
    public class Transaction : Entity
    {
        public int Count { get; set; }
        public DateTime DateTime { get; set; }
        public TransactionType Type { get; set; }
        public Guid? UserId { get; set; }
    }
}
