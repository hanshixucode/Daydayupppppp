using System;
using System.Collections;

namespace ArrayTest
{
    public class Program
    {
        public static void Main()
        {
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
        }
    }

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
    public class Person : IComparable<Person>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
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
    }

}