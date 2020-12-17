using BeautyStore.Models;

namespace BeautyStore.App.Models
{
    public class SupplyModel
    {
        public ProductModel Product { get; set; }
        public int Count { get; set; }
        public int Balance { get; set; }
    }
}
