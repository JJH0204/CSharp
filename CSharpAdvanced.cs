using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace CSharp
{
    public class CSharpAdvanced
    {
        #region Overloading
        public static void Overloading()
        {
            ObjectManager objectManager = new ObjectManager();
            objectManager.AddObject(new Object("Object1", 1, DateTime.Now, DateTime.Now + new TimeSpan(1, 0, 0)));
            objectManager.AddObject(new Object("Object2", 2, DateTime.Now, DateTime.Now + new TimeSpan(1, 0, 0)));
            objectManager.AddObject(new Object("Object3", 3, DateTime.Now, DateTime.Now + new TimeSpan(1, 0, 0)));

            objectManager.PrintObjects();
            objectManager.PrintObjects(1);
        }
        #endregion
        #region Params
        public static void Params()
        {
            ObjectManager objectManager = new ObjectManager();
            // objectManager.AddObject(, , , );
            objectManager.AddObject(new Object("Object1", 1, DateTime.Now, DateTime.Now + new TimeSpan(1, 0, 0)),
                                    new Object("Object2", 2, DateTime.Now, DateTime.Now + new TimeSpan(1, 0, 0)),
                                    new Object("Object3", 3, DateTime.Now, DateTime.Now + new TimeSpan(1, 0, 0)));

            objectManager.PrintObjects();
        }
        #endregion
        #region Optional
        // 선택적 인수
        public static void Optional()
        {
            ObjectManager objectManager = new ObjectManager();
            objectManager.AddObject(new Object(name: "Object1"),
                                    new Object(name: "Object2", id: 2),
                                    new Object(name: "Object3", id: 3, createTime: DateTime.Now));

            objectManager.PrintObjects();
        }
        #endregion
        #region DelegateNAction
        public static void Delegate()
        {
        }
        #endregion
    }

    public class Object
    {
        // 객체에 대한 정의
        public string Name { get; set; }            // 객체의 이름
        public int Id { get; set; }                 // 객체의 고유한 ID
        public DateTime CreateTime { get; set; }    // 생성 시간
        public DateTime DeleteTime { get; set; }    // 삭제 시간

        #region Constructor
        public Object(string name = "default", int id = 0, DateTime createTime = new DateTime(), DateTime deleteTime = new DateTime())
        {
            Name = name;
            Id = id;
            CreateTime = createTime;
            DeleteTime = deleteTime;
        }
        #endregion
    }

    public class ObjectManager
    {
        private List<Object> objects = new List<Object>();

        #region params
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
            {
                objects.Add(n);
            }
        }
        #endregion
        public void RemoveObject(string name)
        {
            objects.RemoveAll(x => x.Name == name);
        }

        public void RemoveObject(DateTime time)
        {
            objects.RemoveAll(x => x.DeleteTime < time);
        }

        #region Overloading
        public void PrintObjects()
        {
            foreach (Object obj in objects)
            {
                Console.WriteLine($"Name: {obj.Name}, CreateTime: {obj.CreateTime}, DeleteTime: {obj.DeleteTime}");
            }
        }

        public void PrintObjects(int index)
        {
            Console.WriteLine($"Name: {objects[index].Name}, CreateTime: {objects[index].CreateTime}, DeleteTime: {objects[index].DeleteTime}");
        }
        #endregion
    }
}