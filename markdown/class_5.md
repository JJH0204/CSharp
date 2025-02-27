# 데이터타입

## 데이터타입

char: 문자
string: 문자열 (문자의 배열 형태)

~~~csharp
// string의 배열 특성을 활용한 예제
foreach (var c in stringValue)
{
  Console.WriteLine(c);
}
~~~

~~~
H
e
l
l
o
,
 
W
o
r
l
d
!
~~~

int: 4byte 정수
long: 8byte 정수
 - 길어질 것 같으면 long

float: 4byte 실수 (정밀도 낮음)
  - float FloatValue = 100.0f;
double: 8byte 실수 (정밀도 높음, 권장)

서버와 클라이언트 통신에서는 float를 잘 사용하지 않음 (실수를 통신에 사용하는 일이 잘 없음)

bool: 논리형 데이터 (true, false)

그 외 자료형은 아래와 같이 사용할 수 있다.

~~~csharp
private static void DataTypes()
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
}
~~~

## 열거형

```csharp
// 열거형 정의
public enum Category
{
    Food = 10,
    Clothes = 20,
    Electronics = 30
}
```

```csharp
// 열거형 사용 예제 1
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

...(생략)...

public static void Func_Enum()
{
  Item item1 = new Item("Apple", 1000, Category.Food);
  Item item2 = new Item("T-shirt", 30000, Category.Clothes);
  Item item3 = new Item("Smartphone", 1000000, Category.Electronics);

  Console.WriteLine(item1);
  Console.WriteLine(item2);
  Console.WriteLine(item3);
}
```

왜 열거형을 사용할까?

- 타입을 정의하거나 약속된 규칙에 맞게 로직을 설계할때 언제나 사람은 실수를 한다. (오타 같은 작은 실수가 버그, 에러를 만든다.)
- 약속을 정의하고 관리할 수 있는 수단으로 열거형을 많이 사용한다.
- 위 예제는 아이템 타입의 3종류를 미리 약속해 정의한 후 사용한다고 볼 수 있다.

~~~
Name: Apple, Price: 1000, Type: Food
Name: T-shirt, Price: 30000, Type: Clothes
Name: Smartphone, Price: 1000000, Type: Electronics
~~~

## 형변환

```csharp
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
```

- 거의 모든 형 변환 방식에 대해서 정리했다.
- 형 변환 문제는 개발 과정에서 에러 발생율이 잦은 부분에 속한다.
- 과정을 이해하고 숙지해 놓는 것이 좋다.