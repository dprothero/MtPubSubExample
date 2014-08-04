using Configuration;
using MassTransit;
using System;

namespace TestSubscriber
{
  class Program
  {
    static void Main(string[] args)
    {
      var bus = BusInitializer.CreateBus("TestSubscriber", x =>
      {
        x.SetConcurrentConsumerLimit(64);
        x.Subscribe(subs =>
        {
          subs.Consumer<SomethingHappenedConsumer>().Permanent();
        });
      });

      Console.ReadKey();

      bus.Dispose();
    }
  }
}
