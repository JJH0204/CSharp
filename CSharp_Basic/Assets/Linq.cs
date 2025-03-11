using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace CSharp_Basic.Assets
{
    // class Linq
    class Info
    {
        public string Name { get; private set; }
        public int Age { get; private set; }

        public Info(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }

    class ItemInfo
    {
        public string Name { get; private set; }

        public ItemInfo(string name)
        {
            Name = name;
        }
    }

    public class Linq
    {
        public static void LinqMain()
        {
            Console.WriteLine("LinqMain");

            #region Linq_case1
            // 1 - 100 까지의 리스트 만들기 
            List<int> intList = new List<int>();
            for (int i = 0; i < 100; i++)
                intList.Add(i);

            // 짝수 리스트 만들기
            List<int> evenList = new List<int>();
            foreach (int i in intList)
            {
                if (i % 2 == 0)
                    evenList.Add(i);
            }

            // Delegate 사용
            IEnumerable<int> enumerableEven = intList.Where(CheckIsEven);

            // Lambda 사용
            Func<int, bool> isEven = i => i % 2 == 0;
            enumerableEven = intList.Where(isEven);              // case 1
            enumerableEven = intList.Where(i => i % 2 == 0);     // case 2

            // Enumerable 출력
            foreach (int i in enumerableEven)
                Console.WriteLine(i);

            List<int> resultList = enumerableEven.ToList();     // Enumerable > List
            int[] resultArray = enumerableEven.ToArray();       // Enumerable > Array
            int resultValue = intList.Where(i => i % 2 != 0).FirstOrDefault(); // Enumerable > Value
            #endregion

            #region Linq_case2
            List<Info> infoList = new List<Info>();
            for (int i = 0; i < 5; i++)
                infoList.Add(new Info($"Name{i}", i));

            List<Info> classResult = infoList.Where((Info info) => info.Age > 2).ToList();

            foreach (Info info in classResult)
                Console.WriteLine($"Name: {info.Name}, Age: {info.Age}");

            Info info1 = infoList.Where((Info info) => info.Age == 2).FirstOrDefault(); // 없는 값이면 null 이 반환된다.
            if (info1 != null)
                Console.WriteLine($"Name: {info1?.Name}, Age: {info1?.Age}");
            #endregion

            #region Linq_case3
            // 정렬 OrderBy, First, Max, Select
            List<Info> RandomList = new List<Info>();
            Random random = new Random();
            for (int i = 0; i < 10; i++)
                RandomList.Add(new Info($"Name{i}", random.Next(1, 100)));

            Console.WriteLine("RandomList");
            foreach (Info i in RandomList)
                Console.WriteLine($"name: {i.Name}, age: {i.Age}");

            // 오름차순 정렬
            List<Info> ResultList = RandomList.OrderBy(v => v.Age).ToList();

            Console.WriteLine("ResultList");
            foreach (Info i in ResultList)
                Console.WriteLine($"name: {i.Name}, age: {i.Age}");

            // 내림차순 정렬
            ResultList = RandomList.OrderByDescending(v => v.Age).ToList();

            Console.WriteLine("ResultList");
            foreach (Info i in ResultList)
                Console.WriteLine($"name: {i.Name}, age: {i.Age}");

            // 가장 큰 값
            int MaxValue = RandomList.Max(ValueTask => ValueTask.Age);
            Console.WriteLine($"MaxValue: {MaxValue}");

            // 가장 작은 값
            int MinValue = RandomList.Min(ValueTask => ValueTask.Age);
            Console.WriteLine($"MinValue: {MinValue}");

            // Select
            ItemInfo SelectList = RandomList.Select(v => new ItemInfo(v.Name)).FirstOrDefault();    // Class를 활용하는 방법
            Console.WriteLine($"SelectList[{SelectList.Name}]");
            #endregion
        }

        private static bool CheckIsEven(int i)
        {
            return i % 2 == 0;
        }
    }
}
