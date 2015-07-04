using System;
using MassTransit;
using MassTransit.Log4NetIntegration.Logging;

namespace TestSubscriber
{
  class Program
  {
    static void Main(string[] args)
    {
      Log4NetLogger.Use();
      var bus = Bus.Factory.CreateUsingRabbitMq(x =>
      {
        var host = x.Host(new Uri("rabbitmq://localhost/"), h => { });

        x.ReceiveEndpoint(host, "MtPubSubExample_TestSubscriber", e =>
          e.Consumer<SomethingHappenedConsumer>());
      });
      var busHandle = bus.Start();
      Console.ReadKey();
      busHandle.Stop().Wait();
    }
  }
}
