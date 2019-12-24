using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAPI.Extensions
{
    public class ApiUserClaimsSettings
    {
        public string Secret { get; set; }

        public int ExpiresinHrs { get; set; }

        public string Issue { get; set; }

        public string ValidFor { get; set; }
    }
}
