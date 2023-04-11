using System.ComponentModel.DataAnnotations;

namespace Assistant.Infrastructure.Models
{
    public class UserModel
    {
        [Key]
        public Guid? UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public bool Right { get; set; }
    }
}
