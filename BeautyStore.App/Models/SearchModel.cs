using BeautyStore.Models;
using System;
using System.Collections.Generic;
namespace BeautyStore.App.Models
{
    public class SearchModel
    {
        public Guid? CategoryId { get; set; }
        public string Key { get; set; }
        public int Page { get; set; } = 0;
        public IEnumerable<ProductModel> Products { get; set; }
    }
}