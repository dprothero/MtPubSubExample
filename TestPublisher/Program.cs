using System;
using Contracts;
using MassTransit;
using MassTransit.Log4NetIntegration.Logging;

namespace TestPublisher
{
  class Program
  {
    static void Main(string[] args)
    {
      Log4NetLogger.Use();
      var bus = Bus.Factory.CreateUsingRabbitMq(x => 
        x.Host(new Uri("rabbitmq://localhost/"), h => { }));
      var busHandle = bus.Start();
      var text = "";

      while (text != "quit")
      {
        Console.Write("Enter a message: ");
        text = Console.ReadLine();

        var message = new SomethingHappenedMessage()
        {
          What = text, When = DateTime.Now
        };
        bus.Publish<SomethingHappened>(message);
      }

      busHandle.Stop().Wait();
    }
  }
}
