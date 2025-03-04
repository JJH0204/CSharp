# class_8

## BitArray

- 이진수에 대한 이해가 되어 있다면 이해하기 쉬운 내용

~~~csharp
BitArray bitArray = new BitArray(8);

bitArray[0] = false;
bitArray[1] = true;
bitArray[2] = false;
bitArray[3] = true;
bitArray[4] = false;
bitArray[5] = false;
bitArray[6] = false;
bitArray[7] = false;

// 00001010(2진수) → 10(10진수)
~~~

- 한정된 메모리 공간을 활용하기에 적합한 방식
- 일반적인 상황에서는 코드 난독 문제로 협업에서 선호하지 않음 (주의)

# Null 검사

```csharp
string str = null;

{
  // null 체크
  if (str == null)
  {
    Console.WriteLine("null");
    return;
  }

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
```

## 예외처리

- 파일 읽어오기, 서버 동기화 과정에서 발생하는 에러 사항들을 처리하기 위한 기능

```csharp
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
```

- 꼭 예외처리를 꼼꼼하게 해서 단단한 프로그램을 만들자

## 프로퍼티

- private: 외부에서 접근 거부 (getter, setter 필요)
- public: 외부 접근 허용

```csharp
public string Letter;
private int money;

// 초기 getter, setter
public void SetMoney(int value)
{
  money = value;
}

public int GetMoney()
{
  return money;
}

// 최신 문법
public int Money
{
  get { return money; }
  set { money = value; }
}

// 더 최신의 문법 (신세계) // 변수 선언이랑 프로퍼티 생성을 동시에 하는 방법
public int money { get; set; }
```