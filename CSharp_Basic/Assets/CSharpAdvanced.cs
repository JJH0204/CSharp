using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace CSharp_Basic
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
        #region static
        public static void Static()
        {
            Person baby = new Student("아기", new DateTime(
                year: 2020,
                month: 1,
                day: 1
            ), SEX.FEMALE);

            Office.BirthReport(baby);
        }
        #endregion
        #region deepCopy
        public static void DeepCopy()
        {
            Person baby = new Student();
            Person baby2 = baby;   // 얕은 복사

            baby.Name = "아기";
            baby2.Name = "아기2";

            System.Console.WriteLine(baby.Name);
            System.Console.WriteLine(baby2.Name);

            baby2 = baby.DeepCopy();    // 깊은 복사

            baby.Name = "아기";
            baby2.Name = "아기2";

            System.Console.WriteLine(baby.Name);
            System.Console.WriteLine(baby2.Name);

            Person baby3 = baby.Clone() as Person;

            baby3.Name = "아기3";

            System.Console.WriteLine(baby.Name);
            System.Console.WriteLine(baby2.Name);
            System.Console.WriteLine(baby3.Name);

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

    public enum SEX
    {
        MALE, FEMALE, OTHER
    }
    public abstract class Person
    {
        #region subClass
        public class Info
        {
            public int OptionType { get; private set; }
            public string OptionValue { get; private set; }
        }
        #endregion
        private string name;        // 이름
        private DateTime birthDate; // 생년월일
        private SEX sex;            // 성별
        public string Name
        {
            get => name;
            set => name = value;
        }
        public DateTime BirthDate
        {
            get => birthDate;
            set => birthDate = value;
        }
        public SEX sEX
        {
            get => sex;
            set => sex = value;
        }
        public Person(string name = "", DateTime birthDate = new DateTime(), SEX sex = SEX.OTHER)
        {
            this.name = name;
            this.birthDate = birthDate;
            this.sex = sex;
        }
        #region DeepCopy
        public Person DeepCopy()
        {
            Person person = new Student();
            person.name = this.name;
            person.birthDate = this.birthDate;
            person.sex = this.sex;
            return person;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion

        #region abstract
        public abstract void GrownUp();
        #endregion
    }

    public class Student : Person
    {
        private string school;      // 학교
        private int grade;          // 학년
        public string School
        {
            get => school;
            set => school = value;
        }
        public int Grade
        {
            get => grade;
            set => grade = value;
        }

        public Student(string name = "",DateTime birthDate = new DateTime(), SEX sex = SEX.OTHER, string school = "", int grade = 0)
            : base
            (
                name,
                birthDate,
                sex
            )
        {
            this.school = school;
            this.grade = grade;
            }
        public override void GrownUp()
        {
            Console.WriteLine("학생이 성인이 되었습니다.");
        }
    }

    // 동사무소 클래스
    public class Office
    {
        // private List<Person> persons = new List<Person>();

        // 출생신고
        public static void BirthReport(Person person)
        {
            // person.BirthDate = DateTime.Now;
            Console.WriteLine("출생신고가 완료되었습니다.");
            Console.WriteLine("이름: " + person.Name);
            Console.WriteLine("생년월일: " + person.BirthDate);
            Console.WriteLine("성별: " + person.sEX);
        }

        // 성별 확인
        public static string CheckSex(Person person)
        {
            string sex = person.sEX == SEX.FEMALE ? "여성" : "남성";
            return person.Name + "님의 성별은 " + sex + "입니다.";
        }
    }

    public interface IObject
    {
        public string Name { get; set; }
        public int Id { get; set; }
        void Print();
    }
    
    public class ExObject : IObject
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public void Print()
        {
            Console.WriteLine("Is ExObject");
        }
    }
}