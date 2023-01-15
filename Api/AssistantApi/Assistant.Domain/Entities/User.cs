using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assistant.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public string Login { get; set; }
        public string Password { get; set; } // TODO: delete
        public bool Right { get; set; }
    }
}
