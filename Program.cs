using System;
// using System.Collections.Generic;

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
      // foreach문 예제
      foreach (var student in students)
      {
        Console.WriteLine($"{student[0]}, {student[1]}, {student[2]}");
      }

      // 반복문 예제
      for (int i = 0; i < students.Count; i++)
      {
        Console.WriteLine($"{students[i][0]}, {students[i][1]}, {students[i][2]}");
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
}