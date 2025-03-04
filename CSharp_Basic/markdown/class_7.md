# 데이터 가공

- 실무에서 사용하는 여러 데이터 가공 형식을 서술한다.

## 문자열 가공

```csharp
{
  // stirng.Format() 메서드
  string str = string.Format("이름: {0}, 나이: {1}, 키: {2}", name, age, height);
  Console.WriteLine(str);

  // 보간법 ($ 사용)
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
```

## 문자열 값 수정

```csharp
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
```

## 수 데이터 가공

```csharp
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
```

## 날짜 표현

```csharp
{
  // local
  DateTime now = DateTime.Now;
  Console.WriteLine(now);

  // UTC <그리니치 표준 시간 기준>
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
```
