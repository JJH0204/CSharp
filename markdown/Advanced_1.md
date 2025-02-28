# 메서드

## 오버로딩

```csharp
public class Object
{
  // 객체에 대한 정의
  public string Name { get; set; }
  // 생성시간
  public DateTime CreateTime { get; set; }
  // 삭제 예정시간
  public DateTime DeleteTime { get; set; }

  public Object(string name, DateTime time)
  {
      Name = name;
      CreateTime = time;
      DeleteTime = CreateTime.AddHours(1);
  }
}

public class ObjectManager
{
  private List<Object> objects = new List<Object>();

  public void AddObject(string name, DateTime time)
  {
    objects.Add(new Object(name, time));
  }

  public void RemoveObject(string name)
  {
    objects.RemoveAll(x => x.Name == name);
  }

  public void RemoveObject(DateTime time)
  {
    objects.RemoveAll(x => x.DeleteTime < time);
  }

  public void PrintObjects()
  {
    foreach (var obj in objects)
    {
      Console.WriteLine($"Name: {obj.Name}, CreateTime: {obj.CreateTime}, DeleteTime: {obj.DeleteTime}");
    }
  }
}
```

- 위 소스코드와 같이 예제 객체와 객체 관리자 class를 정의했다고 가정한다.
- 메서드의 오버로딩(Overloding) 기능을 이용해 같은 이름의 함수라도 다른 기능을 수행하는 함수를 정의할 수 있다.

```csharp
// 오브젝트 전체 출력
public void PrintObjects()
{
  foreach (var obj in objects)
  {
    Console.WriteLine($"Name: {obj.Name}, CreateTime: {obj.CreateTime}, DeleteTime: {obj.DeleteTime}");
  }
}

// 선택한 일부만 출력
public void PrintObjects(int index)
{
  Console.WriteLine($"Name: {objects[index].Name}, CreateTime: {objects[index].CreateTime}, DeleteTime: {objects[index].DeleteTime}");
}
```

- 이것이 가능한 이유는 매개변수의 차이 때문이다.
- 어떤 매개변수에 인자값을 전달하느냐로 함수를 구분할 수 있어 같은 이름으로 비슷한 기능을 수행하는 함수를 정의할 수 있게 된다.
- 단, 그렇다고 함수이름을 모두 통일하여 사용하거나 하면 코드 가독성이 떨어짐으로 위 예제와 같이 유사한 기능을 구현할 때 사용하는 것이 좋다.

```csharp
public static void Overloading()
{
  ObjectManager objectManager = new ObjectManager();
  objectManager.AddObject("Object1", DateTime.Now);
  objectManager.AddObject("Object2", DateTime.Now);
  objectManager.AddObject("Object3", DateTime.Now);

  objectManager.PrintObjects();
  objectManager.PrintObjects(1);
}
```

```
Name: Object1, CreateTime: 2025. 2. 28. 오후 3:14:48, DeleteTime: 2025. 2. 28. 오후 4:14:48
Name: Object2, CreateTime: 2025. 2. 28. 오후 3:14:48, DeleteTime: 2025. 2. 28. 오후 4:14:48
Name: Object3, CreateTime: 2025. 2. 28. 오후 3:14:48, DeleteTime: 2025. 2. 28. 오후 4:14:48
Name: Object2, CreateTime: 2025. 2. 28. 오후 3:14:48, DeleteTime: 2025. 2. 28. 오후 4:14:48
```

## 가변 파라미터

```csharp
public void AddObject(string name, DateTime time)
{
  objects.Add(new Object(name, time));
}

// 파라미터 수량을 원하는 만큼 유동적(가변적)으로 받을 수 있다.
public void AddObject(params string[] name)
{
  foreach (var n in name)
  {
    objects.Add(new Object(n, DateTime.Now));
  }
}
```

```csharp
public static void Params()
{
  ObjectManager objectManager = new ObjectManager();
  objectManager.AddObject("Object1", "Object2", "Object3", "Object4");

  objectManager.PrintObjects();
}
```

```
Name: Object1, CreateTime: 2025. 2. 28. 오후 6:43:47, DeleteTime: 2025. 2. 28. 오후 7:43:47
Name: Object2, CreateTime: 2025. 2. 28. 오후 6:43:47, DeleteTime: 2025. 2. 28. 오후 7:43:47
Name: Object3, CreateTime: 2025. 2. 28. 오후 6:43:47, DeleteTime: 2025. 2. 28. 오후 7:43:47
Name: Object4, CreateTime: 2025. 2. 28. 오후 6:43:47, DeleteTime: 2025. 2. 28. 오후 7:43:47
```

## 선택적 인수와 명명된 인수

- 설명을 위해 코드의 일부를 수정했다.

```csharp
public Object(string name = "default", int id = 0, DateTime createTime = new DateTime(), DateTime deleteTime = new DateTime())
{
  Name = name;
  Id = id;
  CreateTime = createTime;
  DeleteTime = deleteTime;
}
```

- 객체를 생성할 때 필요한 파라미터를 받되, 입력이 없으면 기본 값으로 받도록 설정되었다. 

```csharp
// 수정된 오브젝트 관리자 함수
public void AddObject(Object obj = null)
{
  if (obj == null)
    return;

  objects.Add(obj);
}

public void AddObject(params Object[] obj)
{
  if (string.IsNullOrEmpty(obj.ToString()))
    return;

  foreach (Object n in obj)
    objects.Add(n);
}
```

```csharp
// 실제 사용 예제
public static void Optional()
{
  ObjectManager objectManager = new ObjectManager();
  objectManager.AddObject(new Object(name: "Object1"),
                          new Object(name: "Object2", id: 2),
                          new Object(name: "Object3", id: 3, createTime: DateTime.Now));

  objectManager.PrintObjects();
}
```

- 직접 오브젝트를 생성해 파라미터로 전달할 때 생성하는 생성자에 전달하는 파라미터의 상태를 보면 선택적 인수와 명명된 인수의 사용법을 이해할 수 있다.
- 매개변수의 파라미터로 기본값이 설정되어 있는 경우 값을 입력하지 않아도 상관없다.
- 하지만 중간에 빼먹는 거는 안된다. (예, `new Object(name: "Object3", , createTime: DateTime.Now)`)
- 파라미터를 전달할 매개변수의 이름을 명시하면 가독성이 올라간다.
- 최근 업계 트렌드에 따르면 가독성 문제로 인해 잘 사용하지 않지만 구현 과정에서 편리하고 안정적으로 함수를 구성할 수 있어 종종 사용되곤 한다.

## 대리자 (Delegate, Action, Event)

- 이벤트, 콜백, 핸들러
- 의존성 또는 결합도를 낮추기 위해 사용하는 객체지향 문법
- 너무너무 중요해서 따로 분리해서 탐구할 예정