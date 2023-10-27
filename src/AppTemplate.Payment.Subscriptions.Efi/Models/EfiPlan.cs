using AppTemplate.Payment.Subscriptions.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Payment.Subscriptions.Efi.Models
{
    public class EfiPlan
    {
        [JsonProperty("plan_id")]
        public int PlanId { get; set; }
        public int Interval { get; set; }
        public int Repeats { get; set; }
        public string Name { get; set; }
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
        public EfiPlan()
        {

        }
        public EfiPlan(Plan plan)
        {
            PlanId = int.Parse(plan.Id);
            Interval = plan.Interval;
            Repeats = plan.Repeats;
            Name = plan.Name;   
            CreatedAt = plan.CreatedAt;
        }

        public Plan ConvertToPlan()
        {
            return new Plan
            {
                Id = this.PlanId.ToString(),
                Repeats = this.Repeats,
                CreatedAt = this.CreatedAt,
                Interval = this.Interval,
                Name = this.Name
            };
        }
    }
}
