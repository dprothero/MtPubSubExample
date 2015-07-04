using System;
using Configuration;
using MassTransit;

namespace TestSubscriber
{
  class Program
  {
    static void Main(string[] args)
    {
      var bus = BusInitializer.CreateBus("TestSubscriber", e => e.Consumer<SomethingHappenedConsumer>());
      var busHandle = bus.Start();
      Console.ReadKey();
      busHandle.Stop().Wait();
    }
  }
}
