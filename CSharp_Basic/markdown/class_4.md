# for, foreach

## for

```csharp
 // 반복문 예제
      for (int i = 0; i < students.Count; i++)
      {
        Console.WriteLine($"{students[i][0]}, {students[i][1]}, {students[i][2]}");
      }
```

- ```list.Count``` = 리스트 변수에 저장된 인스턴스(요소)들의 개수를 반환
- i = 0 부터 list.Count - 1 까지 값을 i++ 하며 리스트의 모든 요소를 순회

## foreach

```csharp
foreach (int[] student in students)
{
  Console.WriteLine($"{student[0]}, {student[1]}, {student[2]}");
}
```

- list의 첫 요소부터 요소값을 하나씩 가져와 반복에 사용하는 간단한 방식
- 알아서 리스트(또는 배열)의 범위를 넘어가면 반복이 종료된다.
- int[] 대신 var(추론형)을 넣어서 자료형을 지정하지 않아도 요소에 접근하여 사용할 수 있게 된다.

### 추론형(var)
장점 : 자료형을 고민하지 않아도 된다.
단점 : 코드 가독성이 떨어진다.