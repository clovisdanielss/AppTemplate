namespace AppTemplate.Application.Interfaces;

public interface IUser<TKey>
{
    TKey Id { get; set; }
    string UserName { get; set; }
}