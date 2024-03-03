// See https://aka.ms/new-console-template for more information

var th = new Thread((() =>
{
    for (int i = 0; i < 20; i++)
    {
        Thread.Sleep(200);
        Console.WriteLine("Hello, World!");
    }
}));

th.Start();
Console.WriteLine("???");
th.Join();
Console.WriteLine("Done");