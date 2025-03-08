# 대리자(Delegate)

메서드를 참조하는 포인터 역할

특정 메서드를 가리키고, 필요할 때 해당 메서드를 실행할 수 있도록 한다.

특정 조건에서 다양한 메서드를 실행할 수 있다.

이벤트(Event)와 밀접한 관련이 있다.

함수를 변수처럼 다룰 수 있다.

## 대리자 선언과 사용

1. 대리자 선언

- 대리자는 특정한 메서드의 형식을 정의해야 한다.
- 대리자는 반환형과 매개변수가 일치하는 메서드만 참조할 수 있다.

```csharp
delegate void MyDelegate(int num);
// 반환형이 void이고 int 형 데이터를 매개변수로 받는 메서드를 참조할 수 있는 대리자
```

2. 대리자에 메서드 연결

```csharp
using System;

class Program
{
    // 대리자 선언
    delegate void MyDelegate(int num);

    // 대리자가 참조할 수 있는 메서드
    static void PrintNumber(int num)
    {
        Console.WriteLine("Number: " + num);
    }

    static void Main()
    {
        // 대리자 인스턴스 생성 및 메서드 연결
        MyDelegate del = new MyDelegate(PrintNumber);

        // 대리자 실행 (메서드 호출과 동일)
        del(10);
    }
}
```

```
Number: 10
```

## 대리자의 주요 기능

1) 익명 메서드 사용

- 대리자는 익명 메서드(Anonymous Method)와 함께 사용할 수 있다.

```csharp
using System;

class Program
{
    delegate void MyDelegate(int num);

    static void Main()
    {
        // 익명 메서드 할당
        MyDelegate del = delegate (int num)
        {
            Console.WriteLine("Anonymous Method: " + num);
        };

        del(20);
    }
}
```

```
Anonymous Method: 20
```

2) 람다식 사용

```csharp
using System;

class Program
{
    delegate int SquareDelegate(int num);

    static void Main()
    {
        // 람다식 사용
        SquareDelegate square = num => num * num;

        Console.WriteLine(square(5)); // 25
    }
}
```

3) 멀티캐스트 대리자 (여러 개의 메서드 호출)

- 한 번에 여러 개의 메서드를 등록할 수도 있다.

```csharp
using System;

class Program
{
    delegate void MyDelegate(string message);

    static void PrintMessage1(string msg)
    {
        Console.WriteLine("1: " + msg);
    }

    static void PrintMessage2(string msg)
    {
        Console.WriteLine("2: " + msg);
    }

    static void Main()
    {
        MyDelegate del = PrintMessage1;
        del += PrintMessage2; // 두 번째 메서드 추가

        del("Hello");

        // 특정 메서드 제거
        del -= PrintMessage1;
        del("World");
    }
}
```

```
1: Hello
2: Hello
2: World
```

## Func<> 및 Action<> 사용

- 대리자를 좀 더 편리하게 사용할 수 있도록 Func<>, Action<> 제네릭 대리자를 제공한다.

1) 반환값이 있는 경우: Func<>

```csharp
Func<int, int> square = x => x * x;
Console.WriteLine(square(4)); // 16
```

2) 반환값이 없는 경우: Action<>

```csharp
Action<string> printMessage = msg => Console.WriteLine(msg);
printMessage("Hello Action!"); // Hello Action!
```

## 대리자와 이벤트(Event)와의 관계

```csharp
using System;

class Program
{
    // 이벤트 정의
    public static event Action<string> OnMessageReceived;

    static void Main()
    {
        // 이벤트에 메서드 추가
        OnMessageReceived += msg => Console.WriteLine("Event triggered: " + msg);

        // 이벤트 호출
        OnMessageReceived?.Invoke("Hello, Events!");
    }
}
```

```
Event triggered: Hello, Events!
```

- 이벤트를 사용하면 특정한 일이 발생할 때 실행할 메서드를 등록할 수 있다.