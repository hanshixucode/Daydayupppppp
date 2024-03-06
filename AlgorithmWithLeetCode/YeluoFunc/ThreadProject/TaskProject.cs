namespace ThreadProject;

public interface IFoo
{
    Task FooAsync();
}
public class TaskProject
{
    public static void Main()
    {
        var test = new Test<Template>(new Template());
        test.TestAsync();
        Console.ReadKey();
    }
}

public class Test<T> where T : IFoo
{
    public T _foo { get; private set; }
    public Test(T foo)
    {
        this._foo = foo;
    }

    public async Task TestAsync()
    {
        await _foo.FooAsync();
    }
}

public class Template : IFoo
{
    public void TaskRun()
    {
        MainAsync();
    }

    public Task TestTask()
    {
        return null;
    }
    public async Task MainAsync()
    {
        Console.WriteLine("1");
        await FooAsync();
        Console.WriteLine("2");
    }

    public async Task FooAsync()
    {
        Console.WriteLine("3");
        await Task.Delay(2000);
        Console.WriteLine("4");
    }
}