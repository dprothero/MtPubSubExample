using Configuration;
using Contracts;
using System;

namespace TestPublisher
{
  class Program
  {
    static void Main(string[] args)
    {
      var bus = BusInitializer.CreateBus("TestPublisher", x => { });
      string text = "";

      while (text != "quit")
      {
        Console.Write("Enter a message: ");
        text = Console.ReadLine();

        var message = new SomethingHappenedMessage() { What = text, When = DateTime.Now };
        bus.Publish<SomethingHappened>(message, x => { x.SetDeliveryMode(MassTransit.DeliveryMode.Persistent); });
      }

      bus.Dispose();
    }
  }
}
