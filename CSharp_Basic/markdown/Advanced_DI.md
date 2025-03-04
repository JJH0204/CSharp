# 의존성 주입

객체 간의 의존성을 외부에서 주입하는 설계 패턴

클래스가 직접 다른 객체를 생성하지 않고 필요한 객체를 외부에서 제공받아 사용

코드의 결합도 감소: 한 클래스가 다른 클래스의 구체적인 구현을 몰라도 됨, 변경이 쉬워 유지보수성이 향상된다.

유닛 테스트(Unit Test) 용이

객체 생성과 관리의 일관성 유지

```csharp
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
```

## 생성자 주입 방법

```csharp
public Notification(IMessageService messageService)
{
  _messageService = messageService;
}

public void Send(string message)
{
  _messageService.SendMessage(message);
}
```

- 객체를 생성할 때 생성자를 통해 필요한 의존성을 주입받는다.

```csharp
IMessageService messageService = new EmailService();
Notification notification = new Notification(messageService);
notification.Send("Hello World!");
```

- 강한 의존성을 제거하고, 변경이 쉬워진다.
- 필요한 의존성이 많아지면 생성자가 복잡해진다.

## 메서드 주입

```csharp
public void Send(string message, IMessageService messageService)
{
  messageService.SendMessage(message);
}
```

- 특정 메서드를 호출할 때 의존성을 전달하는 방식

```csharp
IMessageService messageService2 = new EmailService();
Notification notification2 = new Notification();
notification2.Send("Hello World!", messageService2);
```

- 필요한 순간에만 의존성을 전달할 수 있다.
- 모든 호출에 의존성을 전달해야 함으로 불편할 수 있다.

## 속성 주입

```csharp
public IMessageService MessageService { get; set; }

public void Send(string message, int a)  // 메서드 오버라이드를 위해 인자 추가
{
  if (MessageService != null)
    MessageService.SendMessage(message);
}
```

- 외부에서 의존성을 할당할 수 있도록 속성을 제공(public)하는 방식

```csharp
Notification notification1 = new Notification
{
  MessageService = new EmailService()
};
notification1.Send("Hello World!", 1);
```

- 유연한 객체 설정이 가능
- 속성 값을 설정하지 않으면 NullReferenceException이 발생할 가능성이 있다.

## C#의 의존성 주입 프레임워크를 사용하는 방법

- ASP.NET Core의 내장 DI 컨테이너를 활용해 의존성을 관리할 수 있다.