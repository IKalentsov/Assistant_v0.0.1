using Assistant.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Assistant.Domain.Entities
{
    public class User : IAuditable
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; } // TODO: delete
        public bool Right { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime LastModifiedOn { get; set; }
    }
}
