namespace ThreadProject;

public class TaskProject
{
    public static void Main()
    {
        var temp = new Template();
        temp.TaskRun();
        Console.ReadKey();
    }
}

public class Template
{
    public void TaskRun()
    {
        Main();
    }
    public async Task Main()
    {
        Console.WriteLine("1");
        await FooAsync();
        Console.WriteLine("2");
    }

    async Task FooAsync()
    {
        Console.WriteLine("3");
        await Task.Delay(2000);
        Console.WriteLine("4");
    }
}