using BeautyStore.Models.Abstract;

namespace BeautyStore.Models
{
    public class UserModel : Model
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
