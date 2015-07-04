using System;
using Configuration;
using Contracts;

namespace TestPublisher
{
  class Program
  {
    static void Main(string[] args)
    {
      var bus = BusInitializer.CreateBus();
      var busHandle = bus.Start();
      var text = "";

      while (text != "quit")
      {
        Console.Write("Enter a message: ");
        text = Console.ReadLine();

        var message = new SomethingHappenedMessage() { What = text, When = DateTime.Now };
        bus.Publish<SomethingHappened>(message);
      }

      busHandle.Stop().Wait();
    }
  }
}
