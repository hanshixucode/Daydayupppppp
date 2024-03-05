namespace ThreadProject;

public class TaskProject
{
    public static void Main()
    {
        var temp = new Template();
        temp.Main();
    }
}

public class Template
{
    public async Task Main()
    {
        Console.WriteLine("1");
        await FooAsync();
        Console.WriteLine("4");
    }

    async Task FooAsync()
    {
        Console.WriteLine("2");
        await Task.Delay(10);
        Console.WriteLine("3");
    }
}