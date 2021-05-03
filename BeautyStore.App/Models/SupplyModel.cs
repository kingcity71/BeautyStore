using BeautyStore.Entities;
using BeautyStore.Models;
using System;
using System.Collections.Generic;

namespace BeautyStore.App.Models
{
    public class SupplyModel
    {
        public Guid BranchId { get; set; }
        public IEnumerable<Branch> Branchs { get; set; }
        public ProductModel Product { get; set; }
        public int Count { get; set; }
    }
}
