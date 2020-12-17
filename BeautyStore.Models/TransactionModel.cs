using BeautyStore.Models.Abstract;
using BeautyStore.Models.Enum;
using System;

namespace BeautyStore.Models
{
    public class TransactionModel : Model
    {
        public int Count { get; set; }
        public DateTime DateTime { get; set; }
        public TransactionType Type { get; set; }
        public Guid? UserId { get; set; }
    }
}
