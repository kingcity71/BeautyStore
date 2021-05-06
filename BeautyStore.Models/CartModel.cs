using BeautyStore.Models.Abstract;
using BeautyStore.Models.Enum;
using System;
using System.Collections.Generic;

namespace BeautyStore.Models
{
    public class CartModel : Model
    {
        public Guid BranchId { get; set; }
        public string BranchTitle { get; set; }
        public Dictionary<ProductModel, int> ProductCounts { get; set; }
        public BasketStatus Status { get; set; }
        public Guid UserId { get; set; }
    }
}
