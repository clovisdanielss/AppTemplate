namespace AppTemplate.Shared.Interfaces;

public interface IProcedure<Input>
{
    Task HandleAsync(Input input);
}
public interface IProcedure
{
    Task HandleAsync();
}
