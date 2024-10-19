using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assistant.Domain.Models
{
    public class SecuritySettings
    {
        public const string Path = "SecuritySettings";

        public string JWTSecretKey { get; set; }

        public double JWTExpiriesInHours { get; set; }
    }
}
