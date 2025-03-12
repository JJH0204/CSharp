using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSharp_Basic.Assets
{
    public class Async
    {
        // 비동기 프로그래밍 (Thread, aysnc await)
        public static async Task AsyncMain()    // 함수 선언에도 async Task 선언 필요
        {
            Console.WriteLine("AsyncMain\n");

            // Funcs
            // EX_Thread();
            await EX_AysncAwait();              // await 기능을 이용하는 모든 함수들은 await를 지정해야 한다.
        }

        // 스레드??
        // 일반적으로 하나의 메인 루프를 중심으로 게임이 동작 한다.
        // 모든 작업을 단일 스레드에서 처리하면 CPU 리소스를 효과적으로 사용하지 못해 성능이 저하될 수 있다.
        // 주요 시스템을 여러 개의 스레드로 준리하면 더 부드럽게 실행이 가능하다.
        // 예) 물리 연산, 렌더링, AI (및 경로 탐색), 오디오 처리, 네트워크 등

        // 고전 방식의 스레드 프로그래밍(실수할 여지가 많아서 잘 사용하지 않는다고 한다.)
        public static void EX_Thread()
        {
            ManualResetEventSlim task = new ManualResetEventSlim(false);

            Thread worker = new Thread(() =>
            {
                Console.WriteLine("Sub Thread Start...");
                Thread.Sleep(2000);
                Console.WriteLine("Sub Thread End.");
                task.Set();
            });

            worker.Start();

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Main Thread...");
                Thread.Sleep(1000);
            }

            Console.WriteLine("Main Thread End.");

            // Main Thread...
            // Sub Thread Start...   // 메인 스레드 실행 중간에 서브 스레드가 실행된다.
            // Main Thread...
            // Sub Thread End.       // 서브 스레드는 메인 스레드와 별도로 동작하고 종료된다. (종료 시점은 PC 마다 다른 것 같다.)
            // Main Thread...
            // Main Thread...
            // Main Thread...
            // Main Thread End.
        }

        public static async Task EX_AysncAwait()
        {
            // Task<int> task = Task.Run(ServerRequest // () =>
            //                                         // {
            //                                         // Console.WriteLine("Sub Thread Start...");
            //                                         // Thread.Sleep(2000);
            //                                         // Console.WriteLine("Sub Thread End.");
            //                                         // return 200;     // 서버로 부터 값을 받아오는 기능이라 가정
            //                                         // }
            // );

            int result = await ServerRequestAsync();

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Main Thread...");
                Thread.Sleep(5);
            }

            // int result = await task;         // 서브 스레드가 정상 종료될 때까지 대기 

            Console.WriteLine($"Main Thread End.\nResult: {result}");

            // 보다 간단하게 스레드를 사용할 수 있다.
        }

        static int ServerRequest()
        {
            Console.WriteLine("Sub Thread Start...");
            Thread.Sleep(2000);
            Console.WriteLine("Sub Thread End.");
            return 200;     // 서버로 부터 값을 받아오는 기능이라 가정
        }

        // 주의: 메인 스레드가 서브 스레드 보다 빨리(일찍) 종료되는 경우를 대비해서 await를 꼭 사용
        // Main Thread...
        // Main Thread...
        // Main Thread...
        // Sub Thread Start...
        // Main Thread...
        // Main Thread...
        // Sub Thread End.
        // Main Thread End.
        // Result: 200

        // 동작 방식만 이해하고 넘어가자. 실무에서는 위와 같은 형식으로 스레드를 구성하지 않는다고 한다.

        // 실무에 가까운 방식
        static async Task<int> ServerRequestAsync()    // 비동기 함수임을 알 수 있도록 Async를 함수이름으로 사용(룰)
        {
            Console.WriteLine("Sub Thread Start...");
            await Task.Delay(2000);
            Console.WriteLine("Sub Thread End.");
            return 200;
        }
    }

    
}