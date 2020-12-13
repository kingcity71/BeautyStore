using BeautyStore.Models.Abstract;

namespace BeautyStore.Models
{
    public class CategoryModel : Model
    {
        public string Title { get; set; }
        public CategoryModel Parent { get; set; }
    }
}
