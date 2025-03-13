using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}