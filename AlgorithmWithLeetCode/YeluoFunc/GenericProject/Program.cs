using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Generic
{
    public class Program
    {
        public static void Main()
        {
            var list1 = new LinkedList<int>();
            list1.AddLast(1);
            list1.AddLast(2);
            list1.AddLast(3);

            var manager = new DocumentManager<Document>();
            var doc = new Document("hsx", "123");
            manager.AddDocment(doc);
            if(manager.IsDocmentAvailable) manager.DisplayAllDocuments();

            var han = new Han<Han2<int>, Han3>();
            
            
            IIndex<Rectangle> rect = RectangleCollection.GetRectangleCollection();
            IIndex<Shape> shapex = rect;

            for (int i = 0; i < shapex.Count; i++)
            {
                Console.WriteLine(shapex[i]);
            }
        }
    }

    #region 链表
    public class LinkedListNode<T>
    {
        public LinkedListNode(T value)
        {
            Value = value;
        }
        public T Value { get; private set; }
        public LinkedListNode<T> Next { get; internal set; }
        public LinkedListNode<T> Prev { get; internal set; }
    }

    public class LinkedList<T> : IEnumerable<T>
    {
        public LinkedListNode<T> First { get; private set; }
        public LinkedListNode<T> Last { get; private set; }

        public LinkedListNode<T> AddLast(T node)
        {
            var newNode = new LinkedListNode<T>(node);
            if (First == null)
            {
                First = newNode;
                Last = First;
            }
            else
            {
                LinkedListNode<T> previous = Last;
                Last.Next = newNode;
                Last = newNode;
                Last.Prev = previous;
            }

            return newNode;
        }
        public IEnumerator<T> GetEnumerator()
        {
            LinkedListNode<T> current = First;

            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    #endregion

    #region 泛型类

    public interface IDocument
    {
        string Title { get; set; }
        string Content { get; set; }
    }

    public class Document : IDocument
    {
        public Document()
        {
            
        }

        public Document(string title, string conten)
        {
            Title = title;
            Content = conten;
        }
        public string Title { get; set; }
        public string Content { get; set; }
    }
    public class DocumentManager<T> where T: IDocument
    {
        private readonly Queue<T> docmentQueue = new Queue<T>();

        public void AddDocment(T doc)
        {
            lock (this)
            {
                docmentQueue.Enqueue(doc);
            }
        }

        public T GetDocment()
        {
            T doc = default(T);
            lock (this)
            {
                doc = docmentQueue.Dequeue();
            }

            return doc;
        }

        public void DisplayAllDocuments()
        {
            foreach (T doc in docmentQueue)
            {
                Console.WriteLine(doc.Title);
            }
        }

        public bool IsDocmentAvailable => docmentQueue.Count > 0;
    }

    public interface HanBase<in T>
    {
        int Test(T obj);
    }
    public abstract class Han1<T1, T2> : HanBase<T1>
    {
        public abstract T1 Add();
        public abstract int Test(T1 obj);
    }

    public abstract class Han2<T2> : Han1<string, T2>
    {
        public abstract T2 Sub();
    }

    public class Han3 : Han2<int>
    {
        public Han3()
        {
            
        }
        public override string Add()
        {
            return null;
        }

        public override int Test(string obj)
        {
            return 0;
        }

        public override int Sub()
        {
            return -1;
        }
    }

    public class Han<T1, T2> where T1 : HanBase<string>  where T2 : T1 , new()
    {

    }

    #endregion

    #region 泛型接口的协变

    public class Shape
    {
        public double width { get; set; }
        public double height { get; set; }
        public override string ToString() => $"{width} + {height}";
    }

    public class Rectangle : Shape
    {
        
    }
    public interface IIndex<out T>
    {
        T this[int index] { get; }
        int Count { get; }
    }

    public class RectangleCollection : IIndex<Rectangle>
    {
        public Rectangle[] data = new Rectangle[3]
        {
            new Rectangle() { height = 2, width = 4 },
            new Rectangle() { height = 3, width = 6 },
            new Rectangle() { height = 4, width = 8 },
        };

        public static RectangleCollection _coll;

        public static RectangleCollection GetRectangleCollection()
        {
            if (_coll == null)
            {
                _coll = new RectangleCollection();
            }

            return _coll;
        }
        public Rectangle this[int index]
        {
            get
            {
                if (index < 0 || index > data.Length)
                    throw new ArgumentOutOfRangeException("index");
                return data[index];
            }
        }

        public int Count
        {
            get
            {
                return data.Length;
            }
        }
    }

    #endregion

    #region 泛型结构

    //todo 用的少？
    public struct Nullable<T> where T : struct
    {
        public int this[int index] => throw new NotImplementedException();

        public int Count { get; }
    }

    #endregion
    
    #region 泛型方法

    public class Account
    {
        public string Name
        {
            get;
        }
        public decimal Balance { get; private set; }

        public Account(string name, decimal balance)
        {
            Name = name;
            Balance = balance;
        }
    }

    #endregion
}