using System.ComponentModel.DataAnnotations.Schema;

namespace E_commerce.models
{
    public class IsLike
    {
        public int Id { get; set; }
        public Users? Users { get; set; }
        [ForeignKey("UsersId")]
        public string? UsersId { get; set; }
        public ICollection<Products> Products { get; set; }
    }
}
