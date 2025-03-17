using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace CSharp_Basic.Assets
{
    public static class ETC
    {
        /// <summary>
        /// C# 의 고급 개념들 중 기타에 해당하는 개념을 다룹니다.
        /// 1. object, dictionary, enum컨트롤, struct, consolas
        /// 2. 상속(new, sealed, override), goto, 정렬, 다타원배열, 가변배열
        /// 3. Class (종료자 (Finalizer), this() 생성자, 분활 클래스, 프로퍼티, 튜플)
        /// 4. using dispose, 인덱서, try catch 추가, 확장메서드
        /// </summary>
        public static void ObjectMain()
        {
            // object
            int intVal = 42;
            object obj = intVal;    // 모든 자료형은 오브젝트를 최상위로 상속을 받음 //boxing이 일어남
            int intVal2 = (int)obj; // unboxing

            // boxing(또는 unboxing)이 일어나는 작업은 주의, 성능하락

            Character character = new Character("jaeho", 100, 200);
            object obj2 = character;

            SetValue<int>(intVal);
            SetValue<Character>(character);
        }

        static void SetValue<T>(T obj)  // 함수를 만들어 값을 관리하는 것이 더 안전함
        {
            Console.WriteLine(obj);
        }

        // Key 값과 Value값으로 구성된 자료형
        private static Dictionary<int, string> dic = new Dictionary<int, string>();

        public static void DictionaryMain()
        {
            // dictionary
            dic.Add(100, "나무검");
            dic.Add(101, "청동검");
            dic.Add(102, "강철검");

            // 키 값으로 데이터를 구분하기 때문에 리스트 보다 값을 참조하는데 빠르고 효율적인다.
            // 키 값이 중복되면 안됨 그래서 함수를 만들어서 관리하거나 값의 존재를 확인하는 구문을 넣어야 한다.

            if (!dic.ContainsKey(100))  // 값이 있는지 없는지 검사 (없으면 참)
                dic.Add(100, "나무검");
            if (!dic.ContainsKey(101))
                dic.Add(101, "청동검");
            if (!dic.ContainsKey(102))
                dic.Add(102, "강철검");
            if (!dic.ContainsKey(103))
                dic.Add(103, "무형검");

            string value = string.Empty;
            if (dic.ContainsKey(103))   // 값이 있으면 참
                value = dic[103];

            Console.WriteLine(value);

            if (dic.ContainsKey(104))
                value = dic[104];

            Console.WriteLine(value);

            // for 문을 돌릴 수도 있다.
            foreach (KeyValuePair<int, string> item in dic)
            {
                Console.WriteLine($"ItemKey: {item.Key}, ItemValue: {item.Value}");
                // 다만 저장 순서를 보장하지 않음으로 주의(List는 순서를 보장)
            }
        }

        // Enum 컨트롤

        private enum ITEM_TYPE
        {
            none = 0,
            normal = 1,
            rare = 2,
            unique = 3
        }

        public static void EnumControllMain()
        {
            ITEM_TYPE iTEM_TYPE = (ITEM_TYPE)3;
            int item_type = (int)ITEM_TYPE.normal;

            Console.WriteLine($"Int2Enum: {iTEM_TYPE}, Enum2Int: {item_type}");

            if (Enum.IsDefined(typeof(ITEM_TYPE), 3)) // 3 번에 해당하는 Enum 타입이 정의되었는지 검사
                Console.WriteLine("isContains");
            else
                Console.WriteLine("isNotContains");

            // for 순회가 가능
            foreach (var item in Enum.GetValues(typeof(ITEM_TYPE)))
            {
                Console.WriteLine(item);
            }

            // 주의: 키 값으로 string 타입을 사용하는 것은 가급적 사양
            // 사람에 의한 에러를 방지하기 위해
        }

        // struct
        struct ITEM_INFO
        {
            public int ID;
            public string NAME;

            public ITEM_INFO(int id, string name)
            {
                ID = id;
                NAME = name;
            }
        }

        public static void StructMain()
        {
            ITEM_INFO iTEM_INFO = new ITEM_INFO(100, "나무검");     // value 타입
            ITEM_INFO iTEM_INFO1 = iTEM_INFO;                     // 값 복사가 일어난다. (인스턴스 형태가 아님)
            // int, double 같은 value 타입 자료형들은 사실 struct(구조체)이다.
        }

        // 상속
        // internal sealed class 부모클래스
        internal class 부모클래스
        {
            public virtual int GetValues()
            {
                return 0;
            }

            // sealed 키워드로 해당 함수가 상속되는 것을 막을 수 있다.
        }

        internal class 자식클래스 : 부모클래스
        {
            public override int GetValues()
            {
                return base.GetValues();
            }

            // public new int GetValues()
            // {
            //     return 0;
            // }
            // new 키워드로 부모의 함수를 override 하지 않는 같은 이름의 다른 함수를 정의할 수 있다.
        }

        internal class 손자클래스 : 자식클래스
        {
            public override int GetValues()
            {
                return base.GetValues();
                // 자식이 상속 받은 함수는 손자클래스에 override 할 수 있다.
                // 이때 base = 자식클래스(손자의 부모)
            }
        }

        // goto
        public static void GotoMain()
        {
            Console.WriteLine("GOTO Main");
            int i = 0;
        Start:
            Console.WriteLine($"i: {i}");
            i++;
            if (i < 10)
                goto Start;
            else
                return;
            // GOTO Main
            // i: 0
            // i: 1
            // i: 2
            // i: 3
            // i: 4
            // i: 5
            // i: 6
            // i: 7
            // i: 8
            // i: 9

            //코드의 가독성을 매우 해치기 때문에 사용하지 않는 것을 권장
        }

        // sort
        public static void SortMain()
        {
            Console.WriteLine("Sort Main");

            List<int> ints = new List<int>();
            Random random = new Random();

            for (int i = 0; i < 10; i++)
                ints.Add(random.Next(1, 100));

            Console.WriteLine("Before Sort");

            foreach (int item in ints)
                Console.WriteLine(item);

            ints.Sort();
            Console.WriteLine("After Sort");
            foreach (int item in ints)
                Console.WriteLine(item);

            // Class의 정렬 예제
            List<ITEMDATA> iTEMDATAs = new List<ITEMDATA>();

            for (int i = 0; i < 10; i++)
            {
                int j = random.Next(1, 100);
                iTEMDATAs.Add(new ITEMDATA(j, $"ITEM{j}"));
            }

            Console.WriteLine("Before Sort");
            foreach (ITEMDATA item in iTEMDATAs)
                Console.WriteLine(item.ID + ", " + item.Name);

            // 클래스의 경우 Sort() 함수로 정렬이 힘들가(가능은 할까?)
            // 별도의 인터페이스를 구현해 정렬 기능을 정의해야 한다.
            iTEMDATAs.Sort(new ITEMDATAComparer());

            Console.WriteLine("After Sort");
            foreach (ITEMDATA item in iTEMDATAs)
                Console.WriteLine(item.ID + ", " + item.Name);
        }

        class ITEMDATA
        {
            public readonly int ID;
            public readonly string Name;

            public ITEMDATA(int iD, string name)
            {
                ID = iD;
                Name = name;
            }
        }

        // 인터페이스 선언
        class ITEMDATAComparer : IComparer<ITEMDATA>
        {
            public int Compare(ITEMDATA? x, ITEMDATA? y)
            {
                return x.ID.CompareTo(y.ID);
            }
        }

        public static void ArrayMain()
        {
            Console.WriteLine("Array Main");
            // int[,] arrDemension = new int[3, 3];
            int[,] arrDemension =
            {
                {1, 2, 3},
                {4, 5, 6},
                {7, 8, 9}
            };
            Console.WriteLine(arrDemension[1, 2]);

            // 가변 배열
            int[][] arrD = new int[3][];
            arrD[0] = new int[3] { 1, 2, 3 };
            arrD[1] = new int[4] { 1, 2, 3, 4 };
            arrD[2] = new int[] { 1, 2 };

            Console.WriteLine(arrDemension.Length);         // 배열 전체 요소 개수
            Console.WriteLine(arrDemension.GetLength(0));   // 행 개수 (3)
            Console.WriteLine(arrDemension.GetLength(1));   // 열 개수 (3)
        }
    }

    public partial class ITEMINFO
    {
        public int itemId;
        public ITEMINFO()
        {
            Console.WriteLine("기본 생성자");
        }

        public ITEMINFO(int itemId) : this()    // 클래스의 상속에서 보던 패턴 // 사용시 기존 생성자 호출
        {
            this.itemId = itemId;
            Console.WriteLine("아이템 아이디 받는 생성자");
        }

        ~ITEMINFO()
        {
            Console.WriteLine("소멸자 호출");
        }


    }

    public partial class ITEMINFO   // 부분(분할) 클래스 
    {
        public void PrintItem()
        {
            // 컴파일러가 컴파일하면서 하나의 클래스로 합쳐준다.
        }
    }

    public class CharacterInfo
    {
        // public int CharID { get; private set; }
        public required int CharID { get; init; }   // required: 초기화 강제  // init: 초기화 후 수정 불가능

        // public readonly CharID { get; set;}

        // public void SetCharID(int charId)
        // {
        //     CharID = charId;
        // }

        // 우선순위: private readonly > public required init > public readonly > private > public ()

        // 튜플 예제
        public static void PrintCharMain()
        {
            var peopleInfo = (a: 1, b: 2);

            Console.WriteLine(peopleInfo.a);
            Console.WriteLine(peopleInfo.b);

            (int a, int b) value = Init();
            Console.WriteLine(value.a);
            Console.WriteLine(value.b);
        }

        static (int a, int b) Init()
        {
            return (1, 2);
        }
        // 생명주기가 짧아 간단하게 사용하기 좋다. 넘길 인자가 많을 경우 간략하게 표현할 수 있어 유용
    }

    public class ResourcesLoad : IDisposable
    {
        public ResourcesLoad()
        {
            Console.WriteLine("생성자");
        }

        public void Connect()
        {
            Console.WriteLine("리소스 연결");
        }

        public void Disconnect()
        {
            Console.WriteLine("리소스 연결 해제");
        }

        public void Dispose()
        {
            Disconnect();
        }
    }

    public static class EXDispose
    {
        public static void DisposeMain()
        {
            Console.WriteLine("Dispose Main");

            using (ResourcesLoad resourcesLoad = new ResourcesLoad())
            {
                resourcesLoad.Connect();
                Console.WriteLine("리소스 사용");
                // 직접 Dispose를 사용할 필요없이 사용이 끝나면 호출된다.
            }

            int[] ints = new int[] { 1, 2, 3, 4, 5 };
            ITEMINFO iTEMINFO = new ITEMINFO(ints);
            Console.WriteLine(iTEMINFO[3]);     // 인덱서를 이용해 클래스를 배열처럼 사용할 수 있게 된다.

            //try catch
            try
            {
                PrintM();
            }
            catch (Exception e)
            {
                Console.WriteLine("오류 처리 완료");
                Console.WriteLine(e.Message);
            }

            // 확장 메서드
            int a = 1;
            Console.WriteLine(a.Power());
            Console.WriteLine(100.Power(3));
        }

        static void PrintM()
        {
            try
            {
                PrintMItem();   // 에러를 검출할 수 있다.
            }
            catch (Exception e)
            {
                Console.WriteLine("오류 처리");
                throw;      // throw를 이용해 오류를 상위 함수로 보낼 수 있다.
            }
        }

        static void PrintMItem()
        {
            throw new Exception();
        }
    }

    public partial class ITEMINFO
    {
        public int[] ItemIDs { get; init; }

        public ITEMINFO(int[] itemIds)
        {
            ItemIDs = itemIds;
        }

        public int this[int index]
        {
            get
            {
                return ItemIDs[index];
            }

            set
            {
                ItemIDs[index] = value;
            }
        }
    }

    // 확장 메서드 예제
    public static class IntegerExtension
    {
        public static int Power(this int myInt, int v)
        {
            return myInt * 2;
        }
    }
}