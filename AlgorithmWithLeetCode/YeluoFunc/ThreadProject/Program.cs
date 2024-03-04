// See https://aka.ms/new-console-template for more information

var th = new Thread((() =>
{
    try
    {
        while (true)
        {
            Thread.Sleep(1);
        }
        // for (int i = 0; i < 20; i++)
        // {
        //     Thread.Sleep(200);
        //     Console.WriteLine("Hello, World!");
        // }
    }
    catch(ThreadInterruptedException)
    {
        Console.WriteLine("thread over");
    }
    finally
    {
        Console.WriteLine("Finish");
    }
}));

th.Start();
Console.WriteLine("???");
Thread.Sleep(1000);
th.Interrupt();
th.Join();
Console.WriteLine("Done");