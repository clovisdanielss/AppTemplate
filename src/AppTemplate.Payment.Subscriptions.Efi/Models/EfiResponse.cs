using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Payment.Subscriptions.Efi.Models
{
    public class EfiResponse<T>
    {
        public int Code { get; set; }
        public T Data { get; set; }
        public string Error { get; set; }
        [JsonProperty("error_description")]
        public dynamic ErrorDescription { get; set; }
    }

}
