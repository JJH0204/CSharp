# 클래스

## Deepcopy

```csharp
public static void DeepCopy()
{
  Person baby = new Person();
  Person baby2 = baby;   // 얕은 복사

  baby.Name = "아기";
  baby2.Name = "아기2";

  System.Console.WriteLine(baby.Name);
  System.Console.WriteLine(baby2.Name);

  baby2 = baby.DeepCopy();    // 깊은 복사

  baby.Name = "아기";
  baby2.Name = "아기2";

  System.Console.WriteLine(baby.Name);
  System.Console.WriteLine(baby2.Name);
}
```

```csharp
public Person DeepCopy()
{
  Person person = new Person();
  person.name = this.name;
  person.birthDate = this.birthDate;
  person.sex = this.sex;
  return person;
}
```

- 가장 오래된 깊은 복사 방법
- 확실하게 새 인스턴스를 생성해서 반환값으로 전달하는 방식

```csharp
Person baby3 = baby.Clone() as Person;

baby3.Name = "아기3";

System.Console.WriteLine(baby.Name);
System.Console.WriteLine(baby2.Name);
System.Console.WriteLine(baby3.Name);
```

```csharp
public object Clone()
{
  return this.MemberwiseClone();
}
```

- `this.MemberwiseClone();`를 활용해서 인스턴스를 복사하는 방법
- 간혹 구조가 복잡한 경우 에러가 발생할 수 있다. (정상적으로 동작했는지 검사 필요)

## 서브 클래스

```csharp
public class Person
{
  public class Info
  {
      public int OptionType { get; private set; }
      public string OptionValue { get; private set; }
  }
  (생략)
}
```

- 클래스 내부에 서브 클래스를 선언해도 사용을 위해서는 별도의 인스턴스 선언이 필요하다.
- 외부에 동일한 이름의 클래스가 정의, 선언되어 있는 경우 이를 무시하고 사용할 수 있다는 장점이 있어서 서브클래스를 사용한다.
- Person(Info()) 형태이기 때문에 Person()에서 호출하는 Info의 클래스를 지정할 수 있다.

## 인터페이스, 추상클래스

```csharp
public abstract class Person()
{
  (생략)
  public abstract void GrownUp();
}
```

- 함수 선언과 정의를 동시에 할 수 없다.
- 추상화 사용 시 클래스에도 추상화 키워드(abstract)를 추가해야 된다.

```csharp
Person baby = new Person();
```

- 추상화 클래스를 만들면 직접 인스턴스를 생성할 수 없다.

```csharp
public class Student : Person
{
  (생략)
  public override void GrownUp()
  {
    Console.WriteLine("학생이 성인이 되었습니다.");
  }
}
```

- 추상화 클래스를 상속받은 자식 클래스틑 추상화 선언한 메서드에 대해서 무조건 정의가 이루어 져야 한다.

```csharp
public interface IObject
{
  public string Name { get; set; }
  public int Id { get; set; }
  void Print();
}

public class ExObject : IObject
{
  public string Name { get; set; }
  public int Id { get; set; }
  public void Print()
  {
    Console.WriteLine("Is ExObject");
  }
}
```

- 인터페이스의 경우 추상화와 비슷하지만 override 선언 없이 메소드와 속성을 정의하게 된다.
- 협업에서 중요한 개념이 된다. 
- (지정된 모양, 형태로 클래스를 구현하도록 제한하는 용도)