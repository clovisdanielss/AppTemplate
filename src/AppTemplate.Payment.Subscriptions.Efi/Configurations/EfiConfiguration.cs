using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Payment.Subscriptions.Efi.Configurations
{
    public class EfiConfiguration
    {
        public bool IsSandbox { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
