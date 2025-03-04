# 인스턴스 변수의 메모리 참조 & 메서드

## 인스턴스 변수의 메모리 참조

하나 궁금증이 생겼다.

~~~csharp
using System;

namespace CSharp
{
  class Box
  {
    public string Name;
    public int Money;
  }
  class Program
  {
    static void Main(string[] args)
    {
      Box box1 = new Box();
      box1.Name = "Box1";
      box1.Money = 100;

      Box box2 = box1;
      box2.Name = "Box2";
      box2.Money = 200;

      Console.WriteLine("Box1 Name: " + box1.Name + " Money: " + box1.Money);
      Console.WriteLine("Box2 Name: " + box2.Name + " Money: " + box2.Money);

      box2 = new Box();
      box2.Name = "Box3";
      box2.Money = 300;

      Console.WriteLine("Box1 Name: " + box1.Name + " Money: " + box1.Money);
      Console.WriteLine("Box2 Name: " + box2.Name + " Money: " + box2.Money);
    }
  }
}
~~~

위 예제 코드의 실행 결과는 어떻게 될까?

~~~
Box1 Name: Box2 Money: 200
Box2 Name: Box2 Money: 200
Box1 Name: Box2 Money: 200
Box2 Name: Box3 Money: 300
~~~

의외의 결과가 나타났다.

- C언어처럼 ```box2 = box1;``` 코드를 실행하면 box1의 내용이 box2에 복사될 것이라 새각했다. 
- 의도대로 복사가 되었다면 box1과 box2는 서로 영향을 주어서는 안된다.
- 허나 box2에서 값을 수정하면 box1에서 값을 확인했을 때 값이 변해 있는 것을 확인할 수 있다.
- 두 변수가 같은 인스턴스를 **참조** 하고 있는 것 같다.
- 마치 C언어의 포인터 처럼...

## 메서드

새로운 클래스(객체)를 하나 만들었다.

이 클래스는 객체의 2차원상의 좌표를 저장하는 역할을 수행한다.

만약 이 두개의 객체가 있다면 둘의 서로간의 거리를 계산하려면 어떻게 해야할까?

~~~csharp
class vector2
{
  public int x;
  public int y;
}
~~~

~~~csharp
static void Main(string[] args)
{
  vector2 obj1 = new vector2();
  obj1.x = 0;
  obj1.y = 0;

  vector2 obj2 = new vector2();
  obj2.x = 10;
  obj2.y = 10;

  // 두 객체 사이의 직선 거리를 계산
  int distance = (int)Math.Sqrt(Math.Pow(obj2.x - obj1.x, 2) + Math.Pow(obj2.y - obj1.y, 2));
  Console.WriteLine(distance);
}
~~~

보통 이런 경우 삼각함수를 떠올리고 거리를 계산하게 된다.

매번 이런 복잡한 수식을 타이핑하는 것이 효율적일까?

물론 복잡한 것과 귀찮음은 다른 의미지만 만약 거리 계산 기능에 변화가 생긴다면?

대표적인 예시로 위 좌표계는 절대좌표를 의미한다.

객체를 기준으로 거리를 계산한 것이 아니라는 의미이다.

그러면 어떤 객체를 기준으로 삼으며 거리를 게산할 상대는 어떤 객체로 해야 할까를 고민하여 수식에 반영해야 하는데 이 과정을 매번 거리를 계산할 때 하는 것은 복잡한 것이 맞다.

이때 우리는 메서드라는 것을 정의하고 사용해 볼 수 있다.

~~~csharp
static void Main(string[] args)
{
  Vector2 obj1 = new Vector2();
  obj1.x = 0;
  obj1.y = 0;

  Vector2 obj2 = new Vector2();
  obj2.x = 10;
  obj2.y = 10;

  int distance = Distance2D(obj1, obj2);
  Console.WriteLine(distance);
}

static int Distance2D(Vector2 obj1, Vector2 obj2)
{
  int distance = (int)Math.Sqrt(Math.Pow(obj2.x - obj1.x, 2) + Math.Pow(obj2.y - obj1.y, 2));
  return distance;
}
~~~

코드가 한결 보기 편해졌다.

여기에 상대 적인 거리를 표현하도록 코드를 수정하면 된다.

이렇게 미리 메서드(함수)를 정의해 놓으면 같은 기능을 여러번 코드로 작성할 필요도 없어지고 관리도 수월해 진다.

## 배열 (Array)

때로는 변수를 한번에 여러개를 선언하고 관리해야할 때가 있다.

어느 학급의 선생님이 되었다고 가정하고 학생 A, B, C의 국영수 시험 점수를 관리한다고 하자.

그러면 학생 A의 시험 점수, B의 시험 점수, C의 시험 점수를 따로 하나하나 변수를 만들어 관리하기란 번거로운 일이다.

이때 배열을 사용하게 되면 쉽게 데이터를 관리하고 활용할 수 있게 된다.

~~~csharp
private static void Main(string[] args)
{
  // 각 학생 별 시험 점수(국, 영, 수)
  int[] studentA = { 80, 90, 85 };
  int[] studentB = { 90, 95, 90 };
  int[] studentC = { 85, 80, 80 };
}
~~~

정수형 데이터를 저장하는 배열을 정의하고 데이터를 저장한 것을 예시로 확인할 수 있다.

덕분에 관리하고 있는 학생의 시험 성적을 관리하고 활용하기 쉬워졌다.

그런데 어느날 전학생이 왔다.

전학생의 이름을 학생 D라고 하고, 이 학생이 아직 시험을 치루지 않아 성적을 모른다면 어떻게 해야 할까?

필요한 메모리 만큼 공간을 할당만 하고 값을 저장하지 않을 수 있다.

이후에 배열의 인덱스 번호를 이용해 시험 성적을 입력하고 관리할 수 있다.

~~~csharp
private static void Main(string[] args)
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
}
~~~

대부분의 프로그래밍 언어는 인덱스 번호를 0부터 계산한다.

배열의 첫번째 요소에 접근하기 위해서는 인덱스 번호 0을 []안에 입력하면 된다.

이때 주의할 점이 있다.

정의된 배열의 길이를 넘어서는 인덱스 값을 입력하면 시스템에서 오류라 판단하고 컴파일 단계에서 프로그램이 정지된다.

~~~csharp
using System;

namespace CSharp
{
  class Program
  {
    private static void Main(string[] args)
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
      studentD[3] = 95; // 오류 발생
    }
  }
}
~~~

~~~
'Index was outside the bounds of the array.'
~~~

이 점만 유의하여 사용하면 배열은 프로그래밍에서 유용한 기능이다.

## 리스트 (List)

학생의 수가 3~4명으로 유지되는 경우 배열로도 충분하지만, 현실은 그렇지 않다.

나 때만해도 한 반에 최소 30명 많게는 40명을 넘을 정도였다.

그리고 전학생을 생각하면 그 이상의 학생 정보를 하나하나 손수 배열로 만들어 관리하는 것은 비합리적이다.

이때 리스트를 사용하면 문제를 해결할 수 있다.

~~~csharp
using System;

namespace CSharp
{
  class Program
  {
    private static void Main(string[] args)
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
      foreach (int[] student in students)
      {
        Console.WriteLine($"{student[0]}, {student[1]}, {student[2]}");
      }
    }
  }
}
~~~

~~~
학생들의 시험 점수
80, 90, 85
90, 95, 90
85, 80, 80
80, 85, 90
90, 85, 80
85, 90, 80
~~~

```List<자료형> 리스트이름 = 리스트 인스턴스 생성;``` 형식으로 리스트를 생성하고 관리한다.

리스트에 요소를 추가하기 위해서 ```Add()```를 사용하고 추가된 요소는 리스트의 가장 마지막에 저장된다.

리스트에 대한 더 상세한 내용은 다음에 다룰 예정이다.