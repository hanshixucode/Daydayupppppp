using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Linq;

namespace ArrayTest
{
    public class ListTemplate
    {
        public static void Main()
        {
            List<Age> ages = new List<Age>()
            {
                new Age() { id = 1 },
                new Age() { id = 5 },
                new Age() { id = 3 },
                new Age() { id = 4 },
                new Age() { id = 4 },
            };
            ages.Sort(new AgeCompare(CompareType.positive));
            foreach (Age age in ages)
            {
                Console.WriteLine(age);
            }
            ages.Sort(new AgeCompare(CompareType.negative));
            foreach (Age age in ages)
            {
                Console.WriteLine(age);
            }
            ages.Reverse();
        }
        public class Age
        {
            public int id { get; set; }
            public override string ToString()
            {
                return id.ToString();
            }
        }
        public class Age2 : IComparable<Age2>
        {
            public int id { get; set; }
            public override string ToString()
            {
                return id.ToString();
            }

            public int CompareTo(Age2? other)
            {
                if (other == null)
                {
                    return 1;
                }

                if (other.id == this.id) return 0;
                return this.id > other.id ? -1 : 1;
            }
        }
        public enum CompareType
        {
            positive,
            negative
        }
        public class AgeCompare : IComparer<Age>
        {
            public CompareType Type { get; set; }
            public AgeCompare(CompareType type)
            {
                Type = type;
            }
            public int Compare(Age? x, Age? y)
            {
                if(x.id == y.id) return 0;
                if (Type == CompareType.positive)
                {
                    return x.id > y.id ? 1 : -1;
                }
                else
                {
                    return x.id > y.id ? -1 : 1;
                }
            }
        }
    }
    public class Program
    {
        public void Main()
        {
            #region old
            //二维数组 很少用
            // int[,] array2d = new int[3, 3];
            int[,] array2d =
            {
                {1,2,3},
                {1,2,3},
                {1,2,3}
            }
            ;
            //锯齿数组
            int[][] jagged = new int[3][];
            jagged[0] = new int[2] { 1, 2 };
            jagged[1] = new int[6] { 1, 2,1,2,1,2 };
            jagged[2] = new int[3] { 1, 2,3 };
            
            
            //Array
            var intarray = Array.CreateInstance(typeof(int), 5);
            for (int i = 0; i < intarray.Length; i++)
            {
                intarray.SetValue(33, i);
            }

            for (int i = 0; i < intarray.Length; i++)
            {
                Console.WriteLine(intarray.GetValue(i));
            }

            int[] array2 = (int[])intarray;
            for (int i = 0; i < array2.Length; i++)
            {
                Console.WriteLine(array2[i]);
            }
            
            var changearray = intarray.Clone() as Array;
            changearray.SetValue(55,4);
            changearray.SetValue(55,0);
            Array.Copy(changearray, intarray, intarray.Length - 2);

            Sort ss = new Sort();
            ss.SortTest();

            Person[] persons =
            {
                new Person() { FirstName = "c", LastName = "1" },
                new Person() { FirstName = "b", LastName = "1" },
                new Person() { FirstName = "a", LastName = "2" },
            };
            DisplayArray(persons);

            foreach (var person in persons)
            {
                
            }

            IEnumerator enumerator = persons.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current as Person;
                //enumerator.Current;
            }

            var p1 = new Person("han", "xu", new DateTime(1997, 2, 25));
            var p2 = new Person("zhang", "xin", new DateTime(1996, 11, 09));
            var col = new PersonCollection(p1, p2);
            Console.WriteLine(col[1]);
            var temp = col[new DateTime(1997, 2, 25)].GetEnumerator();
            if (temp.MoveNext())
            {
                Console.WriteLine(temp.Current);
            }
            #endregion

            var test = new Test();
            test.Test1();
                        
        }

        /// <summary>
        /// 数组支持协变（父类形定义传入子类实现）
        /// </summary>
        /// <param name="dtat"></param>
        public static void DisplayArray(Person[] dtat)
        {
            //do something
        }
    }
    #region old
    public class Sort
    {
        public void SortTest()
        {
            Person[] persons =
            {
                new Person() { FirstName = "c", LastName = "1" },
                new Person() { FirstName = "b", LastName = "1" },
                new Person() { FirstName = "a", LastName = "2" },
            };
            Array.Sort(persons);
            foreach (Person person in persons)
            {
                Console.WriteLine($"one {person.FirstName}, two {person.LastName}");
            }
            
            Array.Sort(persons, new PersonCompare(PersonNameType.FirstName));
            foreach (Person person in persons)
            {
                Console.WriteLine($"one {person.FirstName}, two {person.LastName}");
            }
        }

        public void EnumTest()
        {
            Object obj = new HelloCollection();
            var helloworld = new HelloCollection();
            foreach (var s in helloworld)
            {
                Console.WriteLine(s);
            }
        }
    }

    public enum PersonNameType
    {
        FirstName,
        LastName
    }

    public class PersonCompare : IComparer<Person>
    {
        public PersonNameType _personNameType;
        public PersonCompare(PersonNameType personNameType)
        {
            _personNameType = personNameType;
        }
        public int Compare(Person x, Person y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (ReferenceEquals(null, y)) return 1;
            if (ReferenceEquals(null, x)) return -1;
            switch (_personNameType)
            {
                case PersonNameType.FirstName :
                    return string.Compare(x.FirstName, y.FirstName);
                case PersonNameType.LastName :
                    return string.Compare(x.LastName, y.LastName);
                default:
                    throw new ArgumentException("unexpected compare type");
            }
        }
    }

    public abstract class PersonBase
    {
        
    }
    //自定义索引运算符
    public class Person : PersonBase, IComparable<Person>
    {
        public DateTime Birthday { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Person()
        {
            
        }
        public Person(string firstName, string lastName, DateTime birthDay)
        {
            FirstName = firstName;
            LastName = lastName;
            Birthday = birthDay;
        }
        public int CompareTo(Person? other)
        {
            if (other == null) return 1;
            int result = string.Compare(this.LastName, other.LastName);
            if (result == 0)
            {
                result = String.Compare(this.FirstName, other.FirstName);
            }

            return result;
        }

        public override string ToString() => $"{FirstName} {LastName}";
    }

    /// <summary>
    /// 索引器应用
    /// </summary>
    public class PersonCollection
    {
        private Person[] _people;

        public Person this[int index]
        {
            get { return _people[index]; }
            set { _people[index] = value; }
        }

        public IEnumerable<Person> this[DateTime dateTime] => _people.Where(p => p.Birthday == dateTime);

        public IEnumerable<Person> FindBirthDay(DateTime dateTime) => _people.Where(p => p.Birthday == dateTime);
        
        public PersonCollection(params Person[] people)
        {
            _people = people.ToArray();
        }
    }

    public class HelloCollection
    {
        public IEnumerator<string> GetEnumerator()
        {
            yield return "Hello";
            yield return "world";
        }
    }

    public class TestEnum
    {
        public string[] txt = new[] { "a", "b", "c", "d" };

        public IEnumerator<string> GetEnumerator()
        {
            for (int i = 0; i < 4; i++)
            {
                yield return txt[i];
            }
        }

        /// <summary>
        /// 所以 元组用来干啥？
        /// </summary>
        /// <param name="name"></param>
        /// <param name="number"></param>
        public void OutTest(out string name,out int number)
        {
            name = "han";
            number = 1;
        }
    }
    #endregion

    ///自定义类型强制转换
    public struct Currency
    {
        public uint Dollars { get; }
        public ushort Cents { get; }

        public Currency(uint dollars, ushort cents)
        {
            Dollars = dollars;
            Cents = cents;
        }

        public override string ToString()
        {
            return $"${Dollars}.{Cents,-2:00}";
        }
        //自定义的强制转换
        public static implicit operator float(Currency value) => value.Dollars + (value.Cents / 100.0f);

        public static implicit operator Currency(float value)
        {
            uint dollars = (uint)value;
            ushort cents = (ushort)((value - dollars) * 100);
            return new Currency(dollars, cents);
        }
    }

    public class Test
    {
        public void Test1()
        {
            object o = new Currency(50, 35);
            Currency c = (Currency)o;
            try
            {
                var balnce = new Currency(50, 35);
                Console.WriteLine(balnce);
                Console.WriteLine($"balance is {balnce}");
                float balance2 = balnce;
                Console.WriteLine(balance2);

                balnce = (Currency)balance2;
                Console.WriteLine(balnce);
                checked
                {
                    balnce = (Currency)(-50.50);
                    Console.WriteLine(balnce);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
    //queue
    public class Documet
    {
        public string Title { get; private set; }
        public string Content { get; private set; }

        public Documet(string title, string content)
        {
            Title = title;
            Content = content;
        }
    }

    public class DocumentManager
    {
        private readonly Queue<Documet> _documets = new Queue<Documet>();

        public void AddDocument(Documet doc)
        {
            lock (this)
            {
                _documets.Enqueue(doc);
            }
        }

        public Documet GetDocument()
        {
            Documet doc = null;
            lock (this)
            {
                doc = _documets.Dequeue();
            }

            return doc;
        }

        public bool IsDocumentAvailable => _documets.Count > 0;
    }

}