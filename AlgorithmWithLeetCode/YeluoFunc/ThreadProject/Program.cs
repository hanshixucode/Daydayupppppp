// See https://aka.ms/new-console-template for more information

// var th = new Thread((() =>
// {
//     try
//     {
//         while (true)
//         {
//             Thread.Sleep(1);
//         }
//         // for (int i = 0; i < 20; i++)
//         // {
//         //     Thread.Sleep(200);
//         //     Console.WriteLine("Hello, World!");
//         // }
//     }
//     catch(ThreadInterruptedException)
//     {
//         Console.WriteLine("thread over");
//     }
//     finally
//     {
//         Console.WriteLine("Finish");
//     }
// }));
// th.Start();
// Console.WriteLine("???");
// Thread.Sleep(1000);
// th.Interrupt();
// th.Join();
// Console.WriteLine("Done");

using System.Collections.Concurrent;

//
// var queue = new ConcurrentQueue<int>();
//
// var producer = new Thread(AddNumber);
// var consumer1 = new Thread(ReadNumber);
// var consumer2 = new Thread(ReadNumber);
//
// producer.Start();
// consumer1.Start();
// consumer2.Start();
//
// producer.Join();
// consumer1.Interrupt();
// consumer2.Interrupt();
// consumer1.Join();
// consumer2.Join();
//
//
// void AddNumber()
// {
//     for (int i = 0; i < 20; i++)
//     {
//         Thread.Sleep(20);
//         queue.Enqueue(i);
//     }
// }
//
// void ReadNumber()
// {
//     try
//     {
//         while (true)
//         {
//             if (queue.TryDequeue(out var result))
//             {
//                 Console.WriteLine(result);
//             }
//             Thread.Sleep(1);
//         }
//     }
//     catch (ThreadInterruptedException)
//     {
//         Console.WriteLine("interrupted");
//     }
// }
// var queue = new Queue<int>();
// var producer = new Thread(AddNum);
// var customer1 = new Thread(ReadNum);
// var customer2 = new Thread(ReadNum);
//
// producer.Start();
// customer1.Start();
// customer2.Start();
//
// producer.Join();
// customer1.Interrupt();
// customer2.Interrupt();
// customer1.Join();
// customer2.Join();
//
// void AddNum()
// {
//     for (int i = 0; i < 10; i++)
//     {
//         Thread.Sleep(20);
//         queue.Enqueue(i);
//     }
// }
//
// void ReadNum()
// {
//     //lock (producer)
//     {
//         while (true)
//         {
//             if (queue.TryDequeue(out var result))
//             {
//                 Console.WriteLine(result);
//             }
//         }
//     }
// }