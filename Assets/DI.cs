using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

// using Microsoft.Extensions.DependencyInjection;

namespace CSharp.Assets
{
    public static class DI
    {
        public static void DIMain()
        {
            #region Constructor Injection
            IMessageService messageService = new EmailService();
            Notification notification = new Notification(messageService);
            notification.Send("Hello World!");
            #endregion

            #region Method Injection
            IMessageService messageService2 = new EmailService();
            Notification notification2 = new Notification();
            notification2.Send("Hello World!", messageService2);
            #endregion

            #region Property Injection
            Notification notification1 = new Notification
            {
                MessageService = new EmailService()
            };
            notification1.Send("Hello World!", 1);
            #endregion
        }

        #region Constructor Injection
        // 객체가 생성될 떄 필요한 의존성을 생성자를 통해 주입받는 방법
        // 생성자를 통해 의존성을 주입받기 때문에 객체가 생성될 때 의존성이 해결된다.

        public interface IMessageService
        {
            void SendMessage(string message);
        }

        public class EmailService : IMessageService
        {
            public void SendMessage(string message)
            {
                Console.WriteLine("EmailService : " + message);
            }
        }

        public class Notification
        {
            private readonly IMessageService _messageService;

            public Notification() { }
            public Notification(IMessageService messageService)
            {
                _messageService = messageService;
            }

            public void Send(string message)
            {
                _messageService.SendMessage(message);
            }
            #region Method Injection
            // 메서드의 인자로 의존성을 주입받는 방법
            // 메서드를 호출할 때 의존성을 주입받기 때문에 메서드 호출 시점에 의존성이 해결된다.

            public void Send(string message, IMessageService messageService)
            {
                messageService.SendMessage(message);
            }
            #endregion

            #region Property Injection
            // 프로퍼티를 통해 의존성을 주입받는 방법
            // 프로퍼티를 통해 의존성을 주입받기 때문에 객체 생성 후 별도로 의존성을 주입해주어야 한다.
            public IMessageService MessageService { get; set; }

            public void Send(string message, int a)  // 메서드 오버라이드를 위해 인자 추가
            {
                if (MessageService != null)
                    MessageService.SendMessage(message);
            }
            #endregion
        }
        #endregion


    }
}