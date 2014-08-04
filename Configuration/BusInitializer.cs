using MassTransit;
using MassTransit.BusConfigurators;
using MassTransit.Log4NetIntegration.Logging;
using System;
using System.Configuration;

namespace Configuration
{
  public class BusInitializer
  {
    public static IServiceBus CreateBus(string queueName, Action<ServiceBusConfigurator> moreInitialization)
    {
      Log4NetLogger.Use();
      var bus = ServiceBusFactory.New(x =>
      {
        var serverName = GetConfigValue("rabbitmq-server-name", "localhost");
        var userName = GetConfigValue("rabbitmq-username", "");
        var password = GetConfigValue("rabbitmq-password", "");
        var queueUri = "rabbitmq://" + serverName + "/MtPubSubExample_" + queueName + "?prefetch=64";

        if (userName != "")
        {
          x.UseRabbitMq(r =>
          {
            r.ConfigureHost(new Uri(queueUri), h =>
            {
              h.SetUsername(userName);
              h.SetPassword(password);
            });
          });
        }
        else
          x.UseRabbitMq();

        x.ReceiveFrom(queueUri);
        moreInitialization(x);
      });

      return bus;
    }

    private static string GetConfigValue(string key, string defaultValue)
    {
      string value = ConfigurationManager.AppSettings[key];
      return string.IsNullOrEmpty(value) ? defaultValue : value;
    }
  }
}
