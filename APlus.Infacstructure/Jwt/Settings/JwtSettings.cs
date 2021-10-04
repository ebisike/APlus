using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APlus.Infacstructure.Jwt.Settings
{
    public class JwtSettings
    {
        public string Site { get; set; }
        public string Secret { get; set; }
        public int ExpirationTime { get; set; }
        public string Audience { get; set; }
    }
}
