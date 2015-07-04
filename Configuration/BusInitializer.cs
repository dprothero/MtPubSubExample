using System;
using MassTransit;
using MassTransit.Log4NetIntegration.Logging;

namespace Configuration
{
  public class BusInitializer
  {
    public static IBusControl CreateBus(string queueName = null, Action<IReceiveEndpointConfigurator> endpointInitialization = null)
    {
      Log4NetLogger.Use();
      var hostAddress = new Uri("rabbitmq://localhost/");
      var bus = Bus.Factory.CreateUsingRabbitMq(x =>
      {
        var host = x.Host(hostAddress, h => {});

        if (!string.IsNullOrEmpty(queueName))
        {
          x.ReceiveEndpoint(host, "MtPubSubExample_" + queueName, e =>
          {
            if (endpointInitialization != null) endpointInitialization(e);
          });
        }
      });

      return bus;
    }
  }
}
