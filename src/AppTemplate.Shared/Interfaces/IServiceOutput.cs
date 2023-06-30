namespace AppTemplate.Shared.Interfaces;

public interface IServiceOutput<T> where T : IEntity
{
    T Result { get; set; }
    List<string> Messages { get; set; }
}