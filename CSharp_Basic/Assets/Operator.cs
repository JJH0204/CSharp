using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace CSharp_Basic.Assets
{
    public static class Operator
    {
        public static void OperatorMain()
        {
            Console.WriteLine("Operator Main");

            NPC npcA = new NPC("A", 100, 100);
            NPC npcB = new NPC("B", 100, 100);

            // 클래스 간 덧샘 연산
            NPC npcTotal = npcA + npcB;

            npcTotal.Print();

            int totalState = (int)npcA;

            Console.WriteLine(totalState);

            double power = npcA;

            Console.WriteLine(power);
        }
    }

    public class NPC : Character
    {
        public NPC(string name, int hp, int mp) : base(name, hp, mp)
        {
        }

        // 클래스 덧셈 연산 구현
        public static NPC operator +(NPC a, NPC b)
        {
            return new NPC($"result {a.Name} + {b.Name}", a.HP + b.HP, a.MP + b.MP);
        }

        // 클래스 뺄셈 구현
        public static NPC operator -(NPC a, NPC b)
        {
            return new NPC($"result {a.Name} - {b.Name}", a.HP - b.HP, a.MP - b.MP);
        }

        public static NPC operator *(NPC a, NPC b)
        {
            return new NPC($"result {a.Name} * {b.Name}", a.HP * b.HP, a.MP * b.MP);
        }

        public static NPC operator /(NPC a, NPC b)
        {
            return new NPC($"result {a.Name} / {b.Name}", a.HP / b.HP, a.MP / b.MP);
        }

        // 클래스 형변환 연산 정의 (명시적)
        public static explicit operator int(NPC a)
        {
            return a.HP + a.MP;
        }

        // 자동 형변환
        public static implicit operator double(NPC a)
        {
            return (double)a.HP + (double)a.MP;
        }

        public void Print()
        {
            Console.WriteLine($"name: {Name}, HP: {HP}, MP: {MP}");
        }
    }
}