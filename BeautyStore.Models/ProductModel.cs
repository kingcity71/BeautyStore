using BeautyStore.Models.Abstract;
using System.Collections.Generic;

namespace BeautyStore.Models
{
    public class ProductModel : Model
    {
        public CategoryModel Category { get; set; }
        public string Description { get; set; }
        public SortedList<int, PhotoModel> Photos { get; set; } = new SortedList<int, PhotoModel>();
        public decimal Price { get; set; }
        public string Title { get; set; }
        public Dictionary<BranchModel, int> BranchCounts{ get; set; }
        public IList<ReviewModel> Reviews { get; set; } = new List<ReviewModel>();
    }
}
