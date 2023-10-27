using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Payment.Subscriptions.Models
{
    public class Plan
    {
        public string Id { get; set; }
        [Required]
        public int Interval { get; set; }
        [Required]
        public int Repeats { get; set; }
        [Required]
        public string Name { get; set; }
        public string CreatedAt { get; set; }

    }
}
