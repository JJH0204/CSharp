using System;

namespace CSharp
{
  class Vector2
  {
    public int x;
    public int y;
  }
  class Program
  {
    static void Main(string[] args)
    {
      Vector2 obj1 = new Vector2();
      obj1.x = 0;
      obj1.y = 0;

      Vector2 obj2 = new Vector2();
      obj2.x = 10;
      obj2.y = 10;

      int distance = Distance2D(obj2, obj1);
      Console.WriteLine(distance);
    }

    static int Distance2D(Vector2 obj1, Vector2 obj2)
    {
      int distance = (int)Math.Sqrt(Math.Pow(obj2.x - obj1.x, 2) + Math.Pow(obj2.y - obj1.y, 2));
      return distance;
    }
  }
}