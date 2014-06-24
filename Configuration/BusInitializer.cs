using MassTransit;
using MassTransit.BusConfigurators;
using MassTransit.Log4NetIntegration.Logging;
using System;

namespace Configuration
{
  public class BusInitializer
  {
    public static IServiceBus CreateBus(string queueName, Action<ServiceBusConfigurator> moreInitialization)
    {
      Log4NetLogger.Use();
      var bus = ServiceBusFactory.New(x =>
      {
        x.UseRabbitMq();
        x.ReceiveFrom("rabbitmq://localhost/MtPubSubExample_" + queueName);
        moreInitialization(x);
      });

      return bus;
    }
  }
}
