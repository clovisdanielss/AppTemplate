using AppTemplate.Payment.Subscriptions.Efi.Configurations;
using AppTemplate.Payment.Subscriptions.Efi.Models;
using AppTemplate.Payment.Subscriptions.Interfaces;
using AppTemplate.Payment.Subscriptions.Models;
using AppTemplate.Shared.AbstractClasses;
using AppTemplate.Shared.Interfaces;
using Efipay;
using Newtonsoft.Json;

namespace AppTemplate.Payment.Subscriptions.Efi.Services
{
    public class EfiPlanClientService : AbstractService, IPlanClientService
    {
        private dynamic EfiClient { get; set; }
        public EfiPlanClientService(EfiConfiguration configuration, INotifier notifier) : base(notifier)
        {
            EfiClient = new EfiPay(configuration.ClientId, configuration.ClientSecret, configuration.IsSandbox, null);
        }
        public Plan Create(Plan plan)
        {
            var body = ToDynamicBody(plan);
            try
            {
                var rawResponse = EfiClient.CreatePlan(null, body);
                var response = JsonConvert.DeserializeObject<EfiResponse<EfiPlan>>(rawResponse);
                if(response.Error != null)
                {
                    Notify($"Error: {response.ErrorDescription}");
                    return new Plan { Id = null };
                }
                return ToPlan(response.Data);
            }
            catch(EfiException ex)
            {
                Notify(ex.Message);
                return new Plan { Id = null };
            }
        }

        public void Delete(string id)
        {
            var param = new
            {
                id = int.Parse(id)
            };

            try
            {
                var rawResponse = EfiClient.DeletePlan(param);
                var response = JsonConvert.DeserializeObject<EfiResponse<EfiPlan>>(rawResponse);
                if (response.Error != null)
                {
                    Notify($"Error: {response.ErrorDescription}");
                }
            }
            catch (EfiException ex)
            {
                Notify(ex.Message);
            }
        }

        public Plan Get(string id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Plan> GetAll()
        {
            try
            {
                var rawResponse = EfiClient.ListPlans();
                var response = JsonConvert.DeserializeObject<EfiResponse<IEnumerable<EfiPlan>>>((string)rawResponse);
                if (response.Error != null)
                {
                    Notify($"Error: {response.ErrorDescription}");
                    return new List<Plan>();
                }
                return response.Data.Select(ToPlan);
            }
            catch(EfiException ex)
            {
                Notify(ex.Message);
                return new List<Plan>();
            }
        }
        
        private Plan ToPlan(EfiPlan plan)
        {
            return plan.ConvertToPlan();
        }
        
        private dynamic ToDynamicBody(Plan plan)
        {
            return new
            {
                interval = plan.Interval,
                repeats = plan.Repeats,
                name = plan.Name
            };
        }
    }
}
