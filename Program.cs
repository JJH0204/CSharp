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
      // (1) 인스턴스 생성 방법
      new Box()
      {
        Name = "Box",
        Money = 100
      };

      // (2) 인스턴스 생성 방법
      Box box1 = new Box()
      {
        Name= "Box1",
        Money = 100
      };

      // (3) 인스턴스 생성 방법
      Box box2 = new Box();
      box2.Name = "Box2";
      box2.Money = 200;

      Console.WriteLine("Box1 Name: " + box1.Name + " Money: " + box1.Money);
      Console.WriteLine("Box2 Name: " + box2.Name + " Money: " + box2.Money);
    }
  }
}