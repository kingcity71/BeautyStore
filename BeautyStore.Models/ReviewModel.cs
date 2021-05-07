using BeautyStore.Models.Abstract;
using System;

namespace BeautyStore.Models
{
    public class ReviewModel : Model
    {
        public Guid ProductId { get; set; }
        public UserModel User { get; set; }
        public int Stars { get; set; }
        public string Comment { get; set; }
    }
}
