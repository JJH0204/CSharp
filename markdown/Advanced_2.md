# 클래스

## 생성자

- 해당 객체가 생성(인스턴스화)될때 초기 값을 입력받아 저장 또는 초기 동작을 수행하도록 하는 지정 함수 오버로딩 가능

## 접근 제어자

```csharp
private readonly string Name;               // 객체의 이름

public string GetName()
{
  return Name;
}
```

- private: 외부에서 접근(사용)할 수 없다. (캡슐화)
- public: 외부에서 사용할 수 있다. (인터페이스)
> 기존에 obj.Name 으로 접근해 객체의 이름을 가져오던 방식을 사용할 수 없기에 getter를 정의하여 인터페이스로 사용한다.
- 아래 순서로 보안 강도가 내려간다.
1) private readonly string name;
2) public readonly string name;
3) public string name { get; private set; }
4) public string name { get; set; }

## readonly

- 객체 생성 당시 값을 저장한 후에 값 수정이 불가능하도록 설정하는 키워드
- 초기에 값 저장은 생성자에서 진행

```csharp
public Object(string name = "default", int id = 0, DateTime createTime = new DateTime(), DateTime deleteTime = new DateTime())
{
  Name = name;
  Id = id;
  CreateTime = createTime;
  DeleteTime = deleteTime;
}
```

## 상속

```csharp
public class InhaviancedObject : Object
{
  public InhaviancedObject(string name = "default", int id = 0, DateTime createTime = new DateTime(), DateTime deleteTime = new DateTime())
    : base(name, id, createTime, deleteTime)
  {
  }
}
```

- Object 라는 이름의 부모객체(상위객체)를 상속 받는 InhaviancedObject라는 객체를 만들었다.
- 상위 객체와 동일한 이름의 맴버 변수(속성)는 가질 수 없다.
- 생성자를 정의할 때 부모객체의 생성자에도 값을 전달할 수 있도록 설정해야 한다. `: base(name, id, createTime, deleteTime)`

## virtual override

```csharp
// 부모 객체의 print 함수
public virtual void Print()
{
  Console.WriteLine("Is Object");
  Console.WriteLine($"Name: {Name}, CreateTime: {CreateTime}, DeleteTime: {DeleteTime}");
}

// 자식 객체의 print 함수
public override void Print()
{
  base.Print();

  Console.WriteLine("Is InhaviancedObject");
  Console.WriteLine($"Name: {GetName()}, CreateTime: {CreateTime}, DeleteTime: {DeleteTime}");

  base.Print();
}
```

```csharp
public static void Class()
{
  InhaviancedObject inhaviancedObject = new InhaviancedObject("InhaviancedObject", 1, DateTime.Now, DateTime.Now + new TimeSpan(1, 0, 0));
  inhaviancedObject.Print();
}
```

```
Is Object
Name: InhaviancedObject, CreateTime: 2025. 3. 3. 오후 1:28:27, DeleteTime: 2025. 3. 3. 오후 2:28:27
Is InhaviancedObject
Name: InhaviancedObject, CreateTime: 2025. 3. 3. 오후 1:28:27, DeleteTime: 2025. 3. 3. 오후 2:28:27
Is Object
Name: InhaviancedObject, CreateTime: 2025. 3. 3. 오후 1:28:27, DeleteTime: 2025. 3. 3. 오후 2:28:27
```

- 자식 객체에서 부모객체에서 virtual로 정의된 메소드를 override하여 제정의할 수 있다.
- 제정의된 메소드는 부모객체의 메소드와 다른 메소드지만 같은 이름으로 사용할 수 있다.
- 외부에서 메소드를 호출하면 먼저 자식의 메소드가 호출되어 실행된다.
- 자식의 메소드 내에서 부모 메소드를 실행시킬 수 있다.

## up, down casting

```csharp
Object inhaviancedObject = new InhaviancedObject("InhaviancedObject", 1, DateTime.Now, DateTime.Now + new TimeSpan(1, 0, 0));
inhaviancedObject.Print();
```

- 자식 객체 타입으로 생성한 인스턴스를 부모 객체 타입의 변수에 저장할 수 있다.
- 실제 자료형은 자식 객체임으로 override된 메소드를 호출하면 자식의 메소드를 먼저 호출하게 된다.

```
Is Object
Name: InhaviancedObject, CreateTime: 2025. 3. 3. 오후 1:38:32, DeleteTime: 2025. 3. 3. 오후 2:38:32
Is InhaviancedObject
Name: InhaviancedObject, CreateTime: 2025. 3. 3. 오후 1:38:32, DeleteTime: 2025. 3. 3. 오후 2:38:32
Is Object
Name: InhaviancedObject, CreateTime: 2025. 3. 3. 오후 1:38:32, DeleteTime: 2025. 3. 3. 오후 2:38:32
```

```csharp
// InhaviancedObject downCasting = (InhaviancedObject)inhaviancedObject;    // 강제 Down Casting (안전하지 않음)
InhaviancedObject downCasting = inhaviancedObject as InhaviancedObject;   // 안전 Down Casting
```

- `as` 문을 활용해 안전하게 down casting 할 수 있다.
- down casting을 실패하면 null 인스턴스가 반환됨으로 null 검사가 필요하다(try, if, ? 등을 사용해야 안전하다)

```csharp
// downCasting?.Print();   // null 체크
if (downCasting != null)
  downCasting.Print();
```

- casting의 장점은 부모 객체 타입으로 리스트를 만들어 자식 객체들을 관리할 수 있다.

```csharp
List<Object> objects = new List<Object>();
objects.Add(new InhaviancedObject("Object1", 1, DateTime.Now, DateTime.Now + new TimeSpan(1, 0, 0)));
objects.Add(new Object("Object1", 1, DateTime.Now, DateTime.Now + new TimeSpan(1, 0, 0)));
objects.Add(new InhaviancedObject("Object1", 1, DateTime.Now, DateTime.Now + new TimeSpan(1, 0, 0)));

foreach (Object obj in objects)
{
  if (obj is InhaviancedObject)   // is 연산자 (형식 검사)
  {
    InhaviancedObject inhavianced = obj as InhaviancedObject;
    inhavianced.Print();
  }
  else
  {
    obj.Print();
  }
}
```

- 이때 `is` 문을 활용해 자식 객체의 종류를 확인해 지정된 동작을 하도록 설정할 수 있다.
- 좀 더 고도화된 cs 프로그래밍 기법에서는 리스트를 관리하는 객체를 정의해서 내부 메서드를 활용해 객체들을 관리한다.

## 의존성 주입(Dependency Injection, DI)

- 대다수의 게임, 소프트웨어는 config 파일에 설정 값을 저장해 두고 프로그램 실행 시 불러와서 사용한다.
- 이와 비슷하게 객체 속성, 메서드에 대한 설정을 외부에서 하고 이를 주입해서 사용할 수 있다면 사용성이 올라갈 것이라는 생각에서 시작한 방식
- 의존성 문제를 해결해야 안전한 프로그래밍이 가능하다.

## static

- 메서드(또는 변수) 앞에 static 키워드가 추가되면 클래스 메서드(변수)로 분류된다.
- 인스턴스를 만들지 않아도 클래스 맴버에 엑세스 할 수 있게 해준다.
- 필드, 메서드, 속성, 연산자, 이벤트, 생성자 또는 클래스에 적용할 수 있다.

```csharp
public static void Static()
{
  Person baby = new Person("아기", new DateTime(
    year: 2020,
    month: 1,
    day: 1
  ), SEX.FEMALE);

  Office.BirthReport(baby);
}
```

- 실행결과

```
출생신고가 완료되었습니다.
이름: 아기
생년월일: 2020. 1. 1. 오전 12:00:00
성별: FEMALE
```

```csharp
public enum SEX
{
  MALE, FEMALE, OTHER
}
public class Person
{
  private string name;        // 이름
  private DateTime birthDate; // 생년월일
  private SEX sex;            // 성별
  public string Name
  {
    get => name;
    set => name = value;
  }
  public DateTime BirthDate
  {
    get => birthDate;
    set => birthDate = value;
  }
  public SEX sEX
  {
    get => sex;
    set => sex = value;
  }
  public Person(string name = "", DateTime birthDate = new DateTime(), SEX sex = SEX.OTHER)
  {
    this.name = name;
    this.birthDate = birthDate;
    this.sex = sex;
  }
}
```

```csharp
public class Office
{
  // 출생신고
  public static void BirthReport(Person person)
  {
    // person.BirthDate = DateTime.Now;
    Console.WriteLine("출생신고가 완료되었습니다.");
    Console.WriteLine("이름: " + person.Name);
    Console.WriteLine("생년월일: " + person.BirthDate);
    Console.WriteLine("성별: " + person.sEX);
  }
}
```

- static 선언 덕분에 인스턴스 생성 없이도 메서드를 호출해 실행할 수 있다.
> static 선언 없으면 발생하는 에러: static이 아닌 필드, 메서드 또는 속성 'Office.BirthReport(Person)'에 개체 참조가 필요합니다.CS0120

