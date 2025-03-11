using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace CSharp_Basic
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

        public static void Func_ValueNRefernce()
        {
            // 값 형식
            int value1 = 10;
            int value2 = value1;

            value2 = 20;

            Console.WriteLine(value1); // 10
            Console.WriteLine(value2); // 20

            // 참조 형식
            int[] array1 = { 1, 2, 3 };
            int[] array2 = array1;

            array2[0] = 4;

            Console.WriteLine(array1[0]); // 4
            Console.WriteLine(array2[0]); // 4
        }

        // call by value, call by reference
        public static void CallByValueNCallByReference()
        {
            // int value = 10;
            // Increase(value);
            // Console.WriteLine(value); // 10
            // Increase(ref value);
            // Console.WriteLine(value); // 11

            // int[] array = { 1, 2, 3 };
            // Increase(array);
            // Console.WriteLine(array[0]); // 2
            // Increase(ref array);
            // Console.WriteLine(array[0]); // 3

            int value = 10;
            Increase(out int newValue);
            Console.WriteLine(newValue); // 11
        }

        public static void Increase(int[] value)
        {
            value[0]++;
        }

        public static void Increase(ref int[] value)
        {
            value[0]++;
        }

        public static void Increase(out int value)
        {
            value = 11;
        }

        // 문자열 가공
        public static void StringFormat()
        {
            string name = "홍길동";
            int age = 25;
            float height = 175.5f;

            // 문자열 만들기
            {
                // stirng.Format() 메서드
                string str = string.Format("이름: {0}, 나이: {1}, 키: {2}", name, age, height);
                Console.WriteLine(str);

                // 보간 문자열 $ 사용
                string str2 = $"이름: {name}, 나이: {age}, 키: {height}";
                Console.WriteLine(str2);

                // 문자열 연결 연산자 + 사용
                string str3 = "이름: " + name + ", 나이: " + age + ", 키: " + height;
                Console.WriteLine(str3);

                // ToString() 메서드 사용
                string str4 = "이름: " + name + ", 나이: " + age.ToString() + ", 키: " + height.ToString();
                Console.WriteLine(str4);

                // StringBuilder 클래스 사용 (문자열의 + 연산의 단점 개선, + 연산마다 string 객체를 생성하여 메모리 낭비 x)
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("이름: ");
                sb.Append(name);
                sb.Append(", 나이: ");
                sb.Append(age);
                sb.Append(", 키: ");
                sb.Append(height);
                string str5 = sb.ToString();
                Console.WriteLine(str5);
            }
            // 문자열 가공
            {
                string str = string.Format("이름: {0}, 나이: {1}, 키: {2}", name, age, height);

                // 찾기
                bool isContains = str.Contains("홍길동");
                Console.WriteLine(isContains);

                // 수정
                string str2 = str.Replace("홍길동", "임꺽정");
                Console.WriteLine(str2);

                // 앞뒤 공백 제거
                string str4 = "   홍길동   ";
                string str5 = str4.Trim();
                Console.WriteLine(str5);

                // 대소문자 변경
                string str6 = "Hi, Hello, World!";
                string toLower = str6.ToLower();
                Console.WriteLine(toLower);
                string toUpper = str6.ToUpper();
                Console.WriteLine(toUpper);

                // 추출
                // string str7 = str.Substring(0, 3); // 0번째부터 3개 (범위 연산자로 대체 가능)
                string str7 = str[4..7];
                Console.WriteLine(str7);

                // 위치 찾기
                int index = str.IndexOf("홍길동");
                Console.WriteLine(index);

                // 분할
                string[] strArray = str6.Split(',');
                foreach (var s in strArray)
                {
                    Console.WriteLine(s);
                }
            }

            {
                // 실수형 올림, 내림, 반올림
                float value = 3.141592f;

                // Math 클래스 사용
                Console.WriteLine(Math.Ceiling(value)); // 올림
                Console.WriteLine(Math.Floor(value)); // 내림
                Console.WriteLine(Math.Round(value)); // 반올림

                // 반올림 자리수 지정
                Console.WriteLine(Math.Round(value, 2)); // 소수점 둘째 자리까지 반올림

                // 소수점 자리수 지정
                Console.WriteLine(value.ToString("0.00")); // 소수점 둘째 자리까지 출력
                Console.WriteLine(string.Format("{0:0.00}", value)); // 소수점 둘째 자리까지 출력

                // 통화 표시
                Console.WriteLine(value.ToString("C"));
                Console.WriteLine(string.Format("{0:C}", value));
                string str = string.Format("{0:C}", value);
                Console.WriteLine(str.Replace(str.Substring(0, 1), str.Substring(0, 1) + " ")); // 통화 단위와 금액 사이에 공백 추가

                Console.WriteLine(string.Format("{0:D3}", 3));  // 3을 세 자리로 표시
                Console.WriteLine(string.Format("{0:N0}", 12347892873));    // 숫자를 1000단위로 구분하여 표시
                Console.WriteLine(Math.Abs(-23));   // 절대값
                Console.WriteLine((int)-23.234);    // 소수점 이하 버림
            }

            // 날짜 시간 표현
            {
                // local
                DateTime now = DateTime.Now;
                Console.WriteLine(now);

                // UTC
                DateTime utcNow = DateTime.UtcNow;
                Console.WriteLine(utcNow);

                // toLocalTime
                DateTime localNow = utcNow.ToLocalTime();
                Console.WriteLine(localNow);

                // toUniversalTime
                DateTime utcNow2 = localNow.ToUniversalTime();
                Console.WriteLine(utcNow2);

                // toTimeStamp
                DateTimeOffset now2 = DateTimeOffset.Now;
                Console.WriteLine(now2.ToUnixTimeSeconds());

                // toDateTime
                DateTimeOffset now3 = DateTimeOffset.FromUnixTimeSeconds(1625790000);
                Console.WriteLine(now3);

                // 날짜 시간 출력
                Console.WriteLine(now.ToString("yyyy-MM-dd HH:mm:ss z"));
                Console.WriteLine(string.Format("{0:yyyy-MM-dd HH:mm:ss}", now));

                // 날짜 시간 연산
                Console.WriteLine(now.AddDays(1));
                Console.WriteLine(now.AddHours(1));
                Console.WriteLine(now.AddMinutes(1));
                Console.WriteLine(now.AddSeconds(1));

                // 날짜 시간 비교
                DateTime yesterday = now.AddDays(-1);
                Console.WriteLine(now > yesterday);

                // 날짜 시간 차이
                TimeSpan diff = now - yesterday;
                Console.WriteLine(diff);
                Console.WriteLine(diff.Days);
                Console.WriteLine(diff.Hours);
                Console.WriteLine(diff.Minutes);
                Console.WriteLine(diff.Seconds);
            }
        }

        // Null 검사
        public static void NullCheck()
        {
            string str = null;

            {
                // null 체크
                // if (str == null)
                // {
                //     Console.WriteLine("null");
                //     return;
                // }

                // null 또는 빈 문자열 체크
                if (string.IsNullOrEmpty(str))
                {
                    Console.WriteLine("null or empty");
                    return;
                }
            }

            {
                // 병합 관리 연산자 (null 조건 연산자)
                var length = str?.Length; // null이 아닐 때만 접근
                Console.WriteLine(length);

                string str4 = str ?? "default"; // null이면 default 값으로 대체
                Console.WriteLine(str4);

                string str5 = "Hello, World!";
                string str6 = str5 ?? "default";
                Console.WriteLine(str6);
            }

            {
                // nullable 형식
                int? value = null;
                Console.WriteLine(value.HasValue); // null인지 확인

                // null을 허용하는 형식 (에러가 발생하기 쉬워서 사용을 권장하지 않음)
            }
        }

        public static void TryCatch()
        {
            try
            {
                // int[] array = { 1, 2, 3 };
                int[] array = null;
                Console.WriteLine(array[2]);
            }
            // 널 참조 예외
            catch (NullReferenceException e)
            {
                Console.WriteLine(e.Message);
            }
            // 배열 길이 검사를 먼저 해야함
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }
            // 모든 예외 처리
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                // 예외 발생 여부와 상관없이 실행
                Console.WriteLine("예외 발생 여부와 상관없이 실행");
            }
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