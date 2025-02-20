# Hello World & Class

## Hello World

~~~csharp
using System;     // C/Cpp의 해더파일 포함(include) 개념과 유사

namespace CSharp
{
  class Program
  {
    static void Main()
    {
      Console.WriteLine("Hello World!");  // console 창에 원하는 문자열 출력 후 줄바꿈
    }
  }
}
~~~

---

## Class

~~~csharp
using System;

namespace CSharp
{
  // Box 객체 정의
  class Box
  {
    public string Name;
    public int Money;
  }

  class Program
  {
    static void Main(string[] args)
    {
      // (1) 인스턴스 생성 방법
      new Box()
      {
        Name = "Box",
        Money = 100
      };

      // (2) 인스턴스 생성 방법
      Box box1 = new Box();
      box1.Name = "Box1";
      box1.Money = 100;

      Box box2 = new Box();
      box2.Name = "Box2";
      box2.Money = 200;

      Console.WriteLine("Box1 Name: " + box1.Name + " Money: " + box1.Money);
      Console.WriteLine("Box2 Name: " + box2.Name + " Money: " + box2.Money);
    }
  }
}
~~~

### Box 객체 정의

~~~csharp
// Box 객체 정의
class Box
{
  public string Name;
  public int Money;
}
~~~

- class 객체이름 {객체속성1; 객체속성2;} 형식으로 원하는 객체를 정의할 수 있다.

### 인스턴스 생성

~~~csharp
static void Main(string[] args)
{
  // (1) 인스턴스 생성 방법
  new Box()
  {
    Name = "Box",
    Money = 100
  };
  // (생략)
}
~~~

- 원하는 객체를 **new** 키워드를 사용해 생성할 수 있다.
- 이것을 인스턴스 생성이라고 한다.
- 위 코드는 인스턴스를 생성하며 객체의 속성 값을 원하는 값으로 정의하고 있다.
- 문제는 위 방법으로 정의한 인스턴스를 활용하기 위해서는 시스템이 이해할 수 있는 이름을 지어주어야 한다.
- 이 행위를 변수에 저장한다고 하는데 (맞는 표현인지는 모르겠다. 나는 이렇게 이해했다.)
- 변수로 인스턴스를 관리하는 방법은 아래와 같다.

~~~csharp
static void Main(string[] args)
{
  // (생략)
  // (2) 인스턴스 생성 방법
  Box box1 = new Box()
  {
    Name= "Box1",
    Money = 100
  };
  // (생략)
}
~~~

- 생성한 인스턴스를 해당하는 클래스 자료형의 변수에 저장한다.
- 생성 방식은 (1)과 같아서 별 차이는 없다.
- 또 다른 방법이 있다. 아래 방법은 소스코드가 보다 직관적이라 이해하기 쉬울 것이다.

~~~csharp
static void Main(string[] args)
{
  // (생략)
  // (3) 인스턴스 생성 방법
  Box box2 = new Box();
  box2.Name = "Box2";
  box2.Money = 200;
  // (생략)
}
~~~

- 위 방법은 객체를 정의할 당시 속성의 접근 제어 설정을 public(공용)으로 하였기 때문에 사용할 수 있다.
- public으로 사용하면 클래스 내부가 아닌 외부에서도 자유롭게 값을 참조할 수 있기 때문에 유용하지만, OOP(객체지향) 프로그래밍에서는 이런 상황을 좋아하지 않는다.

### WriteLine()

~~~csharp
Console.WriteLine("Box1 Name: " + box1.Name + " Money: " + box1.Money);
~~~

- WriteLine()의 특수한 기능 중에 + 연산으로 문자열을 이어 붙여 출력이 가능하다.
- C 언어에서 사용하는 %d, %s, %c 같은 기호를 사용할 필요가 없어 유용할 것 같다.
