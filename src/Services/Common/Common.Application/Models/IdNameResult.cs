namespace Common.Application.Models;
public class IdNameResult
{
    public int Id { get; }
    public string Name { get; }
    public IdNameResult(int id, string name)
    {
        Id = id;
        Name = name;
    }
}