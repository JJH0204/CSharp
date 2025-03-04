# Value Type & Reference Type

```csharp
public static void Func_ValueNRefernce()
{
  // 값 형식
  int value1 = 10;
  int value2 = value1;

  value2 = 20;

  Console.WriteLine(value1); // 10
  Console.WriteLine(value2); // 20

  // 참조 형식 (primitive type)
  int[] array1 = { 1, 2, 3 };
  int[] array2 = array1;

  array2[0] = 4;

  Console.WriteLine(array1[0]); // 4
  Console.WriteLine(array2[0]); // 4
}
```

- 값 형식 변수는 stack, 참조 형식 변수는 Heap 에 할당 (메모리 관점)
- Heap은 가비지 컬랙션에서 관리한다. C# 의 특징
- 사용중이면 놔두고 사용중이지 않으면 제거
- 참조 타입은 선언된 지역 외에서도 접근해서 사용할 수 있게 된다.
- 대표적인 레퍼런스 타입은 string, class, array, list 가 있다.
- 특히 string 타입은 값 형식으로 동작하지만 정확하게는 참조형식이다.

# call by value, call by reference

```csharp
public static void CallByValueNCallByReference()
{
  int value = 10;
  Increase(value);
  Console.WriteLine(value); // 10
  Increase(ref value);
  Console.WriteLine(value); // 11
}

public static void Increase(int value)
{
  value++;
}

public static void Increase(ref int value)
{
  value++;
}
```

- value 타입을 reference 타입 형식으로 사용하는 것은 비추, 사용성을 명확하게 하는 것이 좋다.
- 위 예제는 함수 오버로딩을 통해 두 함수를 구현해 서로 다른 방식(CallByValue, CallByReference 로 호출한 방식)


```csharp
...(생략)...
int[] array = { 1, 2, 3 };
Increase(array);
Console.WriteLine(array[0]); // 2
Increase(ref array);
Console.WriteLine(array[0]); // 3
...(생략)...

public static void Increase(int[] value)
{
  value[0]++;
}

public static void Increase(ref int[] value)
{
  value[0]++;
}
```

- 레퍼런스 타입으로 정의된 변수에는 Call by Value 방식을 사용할 수 없다.
- ref 선언이 없어도 Call By Reference 방식으로 동작한다.


```csharp
...(생략)...
int value = 10;
Increase(out int newValue);
Console.WriteLine(newValue); // 11

...(생략)...
public static void Increase(out int value)
{
  value = 11;
}
```

- 이전에 주의를 요했던 Call By Reference 방식에 대해서 Call By Value 방식으로 전환하는 방식 중 하나의 예제이다.
- out 키워드를 통해 반환값을 인자 값으로 사용한 변수에 그대로 저장할 수 있다.