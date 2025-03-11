using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSharp_Basic.Assets
{
    public static class Lambda
    {
        public static void LambdaMain()
        {
            Console.WriteLine("LambdaMain");

            #region Lambda_case1
            // Action<int> printActorCount = PrintActorCount;
            // printActorCount(10000);
            #endregion
            #region Lambda_case2
            // Action<int> printActorCount = (actorCount) => Console.WriteLine($"Actor Count: {actorCount}");
            // printActorCount(10000);
            #endregion
            #region Lambda_case3
            Action<int> printActorCount = actorCount =>
            {
                Console.WriteLine($"Actor Count: {actorCount}");

                if (actorCount > Config.VAILD_ACTOR_COUNT)
                    Console.WriteLine($"{Config.BOSS_NAME} is coming!");
            };
            #endregion
            #region Lambda_case4
            // Func<int, bool> isActorCountCheck = IsActorCountCheck(actorCount);
            Func<int, bool> isActorCountCheck = (int actorCount) =>
            {
                return actorCount > Config.VAILD_ACTOR_COUNT;
            };
            #endregion

            printActorCount(10000);
            Console.WriteLine(isActorCountCheck(100000));
        }

        internal class Config
        {
            public const string BOSS_NAME = "Lambda";
            public const int VAILD_ACTOR_COUNT = 10000;
        }
        #region Lambda_Expression
        // private static void PrintActorCount(int actorCount)
        // {
        //     Console.WriteLine($"Actor Count: {actorCount}");
        // }

        private static void PrintActorCount(int actorCount) => Console.WriteLine($"Actor Count: {actorCount}");

        // private static bool IsActorCountCheck(int actorCount)
        // {
        //     if (actorCount > Config.VAILD_ACTOR_COUNT)
        //         return true;
        //     return false;
        // }

        private static bool IsActorCountCheck(int actorCount) => actorCount > Config.VAILD_ACTOR_COUNT;
        #endregion

    }
}