using AppTemplate.Shared.Interfaces;

namespace AppTemplate.Shared.Models;

public class ServiceOutput<T> : IServiceOutput<T> where T : IEntity
{
    public ServiceOutput(T result)
    {
        Messages = new List<string>();
        Result = result;
    }

    public ServiceOutput(string error){
        Messages = new List<string>();
        Messages.Add(error);
    }
    public T Result { get; set; }
    public List<string> Messages { get; set; }
}