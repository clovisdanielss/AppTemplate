namespace AppTemplate.Shared.Interfaces;

public interface IService<Input, Output> where Input : IEntity where Output : IEntity{
    IServiceOutput<Output> Handle(Input input);
}

public interface IService<Output> where Output : IEntity{
    IServiceOutput<Output> Handle();
}
