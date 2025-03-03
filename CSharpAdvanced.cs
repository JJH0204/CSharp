using System;
using System.Collections.Generic;
using System.Dynamic;
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

        #region Class
        public static void Class()
        {
            #region Casting
            Object inhaviancedObject = new InhaviancedObject("InhaviancedObject", 1, DateTime.Now, DateTime.Now + new TimeSpan(1, 0, 0));
            inhaviancedObject.Print();

            // InhaviancedObject downCasting = (InhaviancedObject)inhaviancedObject;    // 강제 Down Casting (안전하지 않음)
            InhaviancedObject downCasting = inhaviancedObject as InhaviancedObject;   // 안전 Down Casting

            // downCasting?.Print();   // null 체크
            if (downCasting != null)
                downCasting.Print();


            // List<Object> objects = new List<Object>();
            ObjectManager objectManager = new ObjectManager();
            objectManager.AddObject(new InhaviancedObject("Object1", 1, DateTime.Now, DateTime.Now + new TimeSpan(1, 0, 0)));
            objectManager.AddObject(new Object("Object1", 1, DateTime.Now, DateTime.Now + new TimeSpan(1, 0, 0)));
            objectManager.AddObject(new InhaviancedObject("Object1", 1, DateTime.Now, DateTime.Now + new TimeSpan(1, 0, 0)));

            // foreach (Object obj in objects)
            // {
            //     if (obj is InhaviancedObject)   // is 연산자 (형식 검사)
            //     {
            //         InhaviancedObject inhavianced = obj as InhaviancedObject;
            //         inhavianced.Print();
            //     }
            //     else
            //     {
            //         obj.Print();
            //     }
            // }

            List<InhaviancedObject> inhaviancedObjects = objectManager.GetInhaviancedObject();
            foreach (InhaviancedObject obj in inhaviancedObjects)
            {
                obj.Print();
            }

            List<Object> objects = objectManager.GetObject();
            foreach (Object obj in objects)
            {
                obj.Print();
            }
            #endregion
        }
        #endregion

        #region Dependency Injection
        public static void DependencyInjection()
        {
            
        }
        #endregion
    }

    public class Object
    {
        // 객체에 대한 정의
        #region 접근 제한자 & 읽기 전용
        private readonly string Name;               // 객체의 이름

        public string GetName()
        {
            return Name;
        }
        #endregion
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

        #region virtual
        public virtual void Print()
        {
            Console.WriteLine("Is Object");
            Console.WriteLine($"Name: {Name}, CreateTime: {CreateTime}, DeleteTime: {DeleteTime}");
        }
        #endregion
    }

    public class ObjectManager
    {
        private List<Object> objects = new List<Object>();

        public ObjectManager()
        {
        }
        #region params
        public void AddObject(Object? obj = null)
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
            objects.RemoveAll(x => x.GetName() == name);
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
                Console.WriteLine($"Name: {obj.GetName()}, CreateTime: {obj.CreateTime}, DeleteTime: {obj.DeleteTime}");
            }
        }

        public void PrintObjects(int index)
        {
            Console.WriteLine($"Name: {objects[index].GetName()}, CreateTime: {objects[index].CreateTime}, DeleteTime: {objects[index].DeleteTime}");
        }
        #endregion

        public List<InhaviancedObject> GetInhaviancedObject()
        {
            List<InhaviancedObject> result = new List<InhaviancedObject>();
            foreach (Object obj in objects)
            {
                if (obj is InhaviancedObject)
                    result.Add(obj as InhaviancedObject);
            }
            return result;
        }

        public List<Object> GetObject()
        {
            List<Object> result = new List<Object>();
            foreach (Object obj in objects)
                if (obj is Object)
                    result.Add(obj);
            return result;
        }
    }

    #region Inhavianced
    public class InhaviancedObject : Object
    {
        public InhaviancedObject(string name = "default", int id = 0, DateTime createTime = new DateTime(), DateTime deleteTime = new DateTime())
            : base(name, id, createTime, deleteTime)
        {
        }
        #region override
        public override void Print()
        {
            base.Print();

            Console.WriteLine("Is InhaviancedObject");
            Console.WriteLine($"Name: {GetName()}, CreateTime: {CreateTime}, DeleteTime: {DeleteTime}");

            base.Print();
        }
        #endregion
    }
    #endregion
}