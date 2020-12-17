using BeautyStore.Models.Abstract;
using System;

namespace BeautyStore.Models
{
    public class StoreModel : Model
    {
        public Guid ProductId { get; set; }
        public int Count { get; set; }
    }
}
