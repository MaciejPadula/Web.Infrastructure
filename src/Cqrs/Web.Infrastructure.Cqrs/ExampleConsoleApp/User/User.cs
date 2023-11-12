namespace ExampleConsoleApp.User;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }

    public override string ToString()
    {
        return $"{Id}_{Name}";
    }
}
