using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSharp_Basic.Assets
{
    public class Delegate_CS
    {
        public void DelegateMain()
        {
            DelegateExample1();
            DelegateExample2();
            DelegateExample3();
            DelegateExample4();
            DelegateExample5();
            DelegateExample6();
            DelegateExample7(); 
        }

        #region Delegate-Example1
        public delegate void Print(int value);
        public static void PrintLow(int value)
        {
            Console.WriteLine(value);
        }
        public static void PrintHigh(int value)
        {
            Console.WriteLine(value + 100);
        }
        public static void DelegateExample1()
        {
            Print printDel = PrintLow;
            printDel(100);
            printDel = PrintHigh;
            printDel(100);
        }
        #endregion

        #region Delegate-Example2
        public delegate void Print2();

        public static void DelegateExample2()
        {
            Print2 printDel = delegate () { Console.WriteLine("Print 1"); };
            printDel();
        }
        #endregion

        #region Delegate-Example3
        delegate int SquareDelegate(int num);

        public static void DelegateExample3()
        {
            SquareDelegate square = num => num * num;
            Console.WriteLine(square(5));
        }
        #endregion

        #region Delegate-Example4
        public static void DelegateExample4()
        {
            Print printDel = PrintLow;
            printDel(100);
            printDel += PrintHigh;
            printDel(100);
        }
        #endregion

        #region Delegate-Example5
        public static void DelegateExample5()
        {
            Func<int, int> square = num => num * num;
            Console.WriteLine(square(5));
        }
        #endregion

        #region Delegate-Example6
        public static void DelegateExample6()
        {
            Action<int> print = num => Console.WriteLine(num);
            print(5);
        }
        #endregion

        #region Delegate-Example7
        public static event Action<string> OnMessageReceived;

        public static void DelegateExample7()
        {
            // 이벤트에 메서드 추가
            OnMessageReceived += msg => Console.WriteLine("Event triggered: " + msg);

            // 이벤트 호출
            OnMessageReceived?.Invoke("Hello, Events!");
        }
        #endregion
    }
}