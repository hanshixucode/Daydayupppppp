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
            manager.DisplayAllDocuments();
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
    

    #endregion
}