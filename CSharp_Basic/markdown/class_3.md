# List 

- ```System.Collections.Generic``` 라이러리에 포함된 자료형 임으로 하위 버전에서는 using으로 포함시켜야 할 때가 있다.
- 최근에는 ```CSharp.GlobalUsings.g.cs``` 파일에 사용하는 전역 변수를 라이브러리를 미리 포함시켜 놓은 상태로 개발이 가능하다.
- 위 파일에서 선언 내용을 지워버리면 소스코드에서 using을 사용해야 하는 것이 됨
- 사용자 편의를 위한 리스트 관련 기본 메소드들이 있다.

List.Clear(); // 리스트 초기화
List.Remove(인스턴스); // 인스턴스와 동일한 요소 리스트에서 삭제
List.Add(인스턴스); // 리스트의 마지막에 동일한 인스턴스를 요소로 추가
List.RemoveAt(요소번호); // 해당 순서의 요소 리스트에서 제거
List.Find(조건); // 조건에 맞는 요소를 검색 후 반환

~~~csharp
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

    // 학생들의 시험 점수를 리스트에서 삭제
    students.RemoveAt(3); // studentD 삭제 (4번째 요소)
    students.RemoveAt(3); // { 90, 85, 80 } 삭제 (4번째 요소)

    Console.WriteLine("학생들의 시험 점수");
    foreach (int[] student in students)
    {
      Console.WriteLine($"{student[0]}, {student[1]}, {student[2]}");
    }

    //특정 학생의 시험 점수 수정
    students[0][0] = 95; // studentA의 국어 점수 수정
    students[0][1] = 100; // studentA의 영어 점수 수정
    students[0][2] = 95; // studentA의 수학 점수 수정

    Console.WriteLine("학생들의 시험 점수");
    foreach (int[] student in students)
    {
      Console.WriteLine($"{student[0]}, {student[1]}, {student[2]}");
    }

    // 조건에 맞는 학생의 시험 성적 열람
    int[] studentE = students.Find(s => s[0] == 90 && s[1] == 95 && s[2] == 90);
    if (studentE != null)
    {
      Console.WriteLine("학생 E의 시험 점수");
      Console.WriteLine($"{studentE[0]}, {studentE[1]}, {studentE[2]}");
    }
    else
    {
      Console.WriteLine("학생 E의 시험 점수가 없습니다.");
    }

    // 리스트 초기화
    students.Clear();
    Console.WriteLine("학생들의 시험 점수");  // 아무것도 출력되지 않음
    foreach (int[] student in students)
    {
      Console.WriteLine($"{student[0]}, {student[1]}, {student[2]}");
    }
  }
}
~~~