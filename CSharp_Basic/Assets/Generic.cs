using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace CSharp_Basic.Assets
{
    public class Generic
    {
        public static void GenericMain()
        {
            Console.WriteLine("GenericMain");

            // 제네릭 (Generic): 데이터 타입을 특정하지 않고 유연하게 코드 작성이 가능한 문법
            // 컴파일 시점에서 타입이 결정되므로 성능적인 이점도 있다.

            List<int> GenericList = new List<int>(); // 제네릭 사용 예제

            Console.WriteLine("Is Generic Class");

            GenericBox<int> intBox = new GenericBox<int>();
            GenericBox<double> doubleBox = new GenericBox<double>();

            intBox.mData = 100;
            doubleBox.mData = 3.141;

            Console.WriteLine($"intBos: {intBox.mData}, doubleBox: {doubleBox.mData}");

            Console.WriteLine("Is Generic Func");

            CharacterManager characterManager = new CharacterManager();

            characterManager.Init();

            characterManager.ShowStats();

            Console.WriteLine("플레이어가 몬스터를 공격합니다.");
            characterManager.PlayerAttack();

            characterManager.ShowStats();

            Console.WriteLine("몬스터들이 플레이어를 공격합니다.");

            characterManager.MonstersAttack();

            characterManager.ShowStats();
        }
    }

    #region GenericClass
    public class GenericBox<T>
    {
        public T mData { set; get; }
    }
    #endregion
    public class Character
    {
        public string Name { set; get; }

        public int HP { set; get; }

        public int MP { set; get; }

        public Character(string name, int hp, int mp)
        {
            Name = name;
            HP = hp;
            MP = mp;
        }

        #region GenericFunc
        public void Attack<T>(T Target, int Demage) where T : Character
        {
            Character target = Target as Character;

            if (target == null)
            {
                Console.WriteLine("공격할 대상이 없습니다.");
                return;
            }

            Console.WriteLine("공격할 대상을 발견했습니다.");
            Console.WriteLine($"대상: {target.Name}");

            target.HP -= Demage;
        }
        #endregion
    }

    public class Player : Character
    {
        public Player(string name, int hp, int mp) : base(name, hp, mp)
        {
        }
    }

    public class Monster : Character
    {
        public Monster(string name, int hp, int mp) : base(name, hp, mp)
        {
        }
    }

    public class CharacterManager
    {
        public Player mPlayer;

        public List<Monster> lMonsters;

        public CharacterManager()
        {

        }

        public void Init()
        {
            mPlayer = new Player("용사", 100, 100);

            lMonsters = new List<Monster>();
            for (int i = 1; i < 11; i++)
                lMonsters.Add(new Monster($"몬스터{i}", 100, 100));

            Console.WriteLine("전투 시뮬레이터가 동작합니다.");
        }

        public void ShowStats()
        {
            Console.WriteLine("[플레이어 상태]\n" + $"이름:\t{mPlayer.Name}\n" + $"HP:\t{mPlayer.HP}\n" + $"MP:\t{mPlayer.MP}\n");

            string monsterNameList = "이름:\t";

            foreach (Monster i in lMonsters)
                monsterNameList += $"{i.Name}\t";

            string monsterHPList = "HP:\t";

            foreach (Monster i in lMonsters)
                monsterHPList += $"{i.HP.ToString()}\t";

            string monsterMPList = "MP:\t";

            foreach (Monster i in lMonsters)
                monsterMPList += $"{i.MP.ToString()}\t";

            Console.WriteLine("[몬스터 상태]\n" + monsterNameList + "\n" + monsterHPList + "\n" + monsterMPList + "\n");
        }

        public void PlayerAttack()
        {
            mPlayer.Attack(lMonsters[0], 100);
        }

        public void MonstersAttack()
        {
            foreach (Monster i in lMonsters)
            {
                if (i.HP > 0)
                    i.Attack(mPlayer, 10);
            }
        }
    }
}