using BeautyStore.Entities.Abstract;

namespace BeautyStore.Entities
{
    public class User : Entity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}