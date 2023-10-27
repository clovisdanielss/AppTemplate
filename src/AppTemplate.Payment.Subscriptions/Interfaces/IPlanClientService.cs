
using AppTemplate.Payment.Subscriptions.Models;

namespace AppTemplate.Payment.Subscriptions.Interfaces
{
    public interface IPlanClientService
    {
        public IEnumerable<Plan> GetAll();
        public Plan Get(string id);
        public void Delete(string id);
        public Plan Create(Plan plan);
    }
}