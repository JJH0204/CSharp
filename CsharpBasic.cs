using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSharp
{
    public class CsharpBasic
    {
        public static void ArrayNList()
        {
            // 각 학생 별 시험 점수(국, 영, 수)
            int[] studentA = { 80, 90, 85 };
            int[] studentB = { 90, 95, 90 };
            int[] studentC = { 85, 80, 80 };

            // 전학생 D
            int[] studentD = new int[3];

            // 학생 D의 시험 점수 입력
            studentD[0] = 80;
            studentD[1] = 85;
            studentD[2] = 90;
            // studentD[3] = 95; // 오류 발생

            // 학생들의 시험 점수를 저장할 리스트 생성
            List<int[]> students = new List<int[]>();

            // 학생들의 시험 점수를 리스트에 추가
            students.Add(studentA);
            students.Add(studentB);
            students.Add(studentC);
            students.Add(studentD);

            // 학생들의 시험 점수를 리스트에 추가(배열을 직접 생성하여 추가)
            students.Add(new int[] { 90, 85, 80 });
            students.Add(new int[] { 85, 90, 80 });

            Console.WriteLine("학생들의 시험 점수");
            // foreach문 예제
            foreach (var student in students)
            {
                Console.WriteLine($"{student[0]}, {student[1]}, {student[2]}");
            }

            // 반복문 예제
            for (int i = 0; i < students.Count; i++)
            {
                Console.WriteLine($"{students[i][0]}, {students[i][1]}, {students[i][2]}");
            }

            // 학생들의 시험 점수를 리스트에서 삭제
            students.RemoveAt(3); // studentD 삭제 (4번째 요소)
            students.RemoveAt(3); // { 90, 85, 80 } 삭제 (4번째 요소)

            Console.WriteLine("학생들의 시험 점수");
            foreach (int[] student in students)
            {
                Console.WriteLine($"{student[0]}, {student[1]}, {student[2]}");
            }

            //특정 학생의 시험 점수 수정
            students[0][0] = 95; // studentA의 국어 점수 수정
            students[0][1] = 100; // studentA의 영어 점수 수정
            students[0][2] = 95; // studentA의 수학 점수 수정

            Console.WriteLine("학생들의 시험 점수");
            foreach (int[] student in students)
            {
                Console.WriteLine($"{student[0]}, {student[1]}, {student[2]}");
            }

            // 조건에 맞는 학생의 시험 성적 열람
            int[] studentE = students.Find(s => s[0] == 90 && s[1] == 95 && s[2] == 90);
            if (studentE != null)
            {
                Console.WriteLine("학생 E의 시험 점수");
                Console.WriteLine($"{studentE[0]}, {studentE[1]}, {studentE[2]}");
            }
            else
            {
                Console.WriteLine("학생 E의 시험 점수가 없습니다.");
            }

            // 리스트 초기화
            students.Clear();
            Console.WriteLine("학생들의 시험 점수");  // 아무것도 출력되지 않음
            foreach (int[] student in students)
            {
                Console.WriteLine($"{student[0]}, {student[1]}, {student[2]}");
            }
        }

        public static void DataTypes()
        {
            // 정수형 데이터 타입
            sbyte sbyteValue = 127;
            byte byteValue = 255;
            short shortValue = 32767;
            ushort ushortValue = 65535;
            int intValue = 2147483647;
            uint uintValue = 4294967295;
            long longValue = 9223372036854775807;
            ulong ulongValue = 18446744073709551615;

            // 실수형 데이터 타입
            float floatValue = 3.40282347E+38F;
            double doubleValue = 1.79769313486231570E+308;
            decimal decimalValue = 79228162514264337593543950335M;

            // 문자형 데이터 타입
            char charValue = 'A';

            // 논리형 데이터 타입
            bool boolValue = true;

            // 문자열 데이터 타입
            string stringValue = "Hello, World!";

            Console.WriteLine($"sbyte: {sbyteValue}");
            Console.WriteLine($"byte: {byteValue}");
            Console.WriteLine($"short: {shortValue}");
            Console.WriteLine($"ushort: {ushortValue}");
            Console.WriteLine($"int: {intValue}");
            Console.WriteLine($"uint: {uintValue}");
            Console.WriteLine($"long: {longValue}");
            Console.WriteLine($"ulong: {ulongValue}");
            Console.WriteLine($"float: {floatValue}");
            Console.WriteLine($"double: {doubleValue}");
            Console.WriteLine($"decimal: {decimalValue}");
            Console.WriteLine($"char: {charValue}");
            Console.WriteLine($"bool: {boolValue}");
            Console.WriteLine($"string: {stringValue}");

            foreach (var c in stringValue)
            {
                Console.WriteLine(c);
            }
        }

        public static void Func_Enum()
        {
            Item item1 = new Item("Apple", 1000, Category.Food);
            Item item2 = new Item("T-shirt", 30000, Category.Clothes);
            Item item3 = new Item("Smartphone", 1000000, Category.Electronics);

            Console.WriteLine(item1);
            Console.WriteLine(item2);
            Console.WriteLine(item3);
        }

        // 형 변환 예제
        public static void TypeConversion()
        {
            // 암시적 형 변환
            int intValue = 10;
            long longValue = intValue;

            // 명시적 형 변환(강재 형변환)
            long longValue2 = 100;
            int intValue2 = (int)longValue2;

            // ToString() 메서드를 이용한 형 변환
            string stringValue = intValue2.ToString();
            // string stringValue = intValue2 + ""; // 동일한 결과

            // Parse() 메서드를 이용한 형 변환
            int intValue3 = int.Parse(stringValue);

            // TryParse() 메서드를 이용한 형 변환
            string stringValue2 = "100";
            int intValue4;
            if (int.TryParse(stringValue2, out intValue4))
            {
                Console.WriteLine(intValue4);
            }
            else
            {
                Console.WriteLine("변환 실패");
            }

            // Convert 클래스를 이용한 형 변환
            string stringValue3 = "100";
            int intValue5 = Convert.ToInt32(stringValue3);

            // Boxing과 Unboxing
            int intValue6 = 100;
            object objValue = intValue6; // Boxing
            int intValue7 = (int)objValue; // Unboxing
        }

        
    }

    public enum Category
    {
        Food = 10,
        Clothes = 20,
        Electronics = 30
    }

    class Item
    {
        public string Name { get; set; }
        public int Price { get; set; }

        public Category Type { get; set; }

        public Item(string name, int price, Category type)
        {
            Name = name;
            Price = price;
            Type = type;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Price: {Price}, Type: {Type}";
        }
    }
}