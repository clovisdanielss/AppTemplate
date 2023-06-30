namespace AppTemplate.Shared.Interfaces;

public interface IService<Input, Output>
{
    Task<Output> HandleAsync(Input input);
}

public interface IService<Output>
{
    Task<Output> HandleAsync();
}
