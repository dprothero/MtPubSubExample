using Configuration;
using Contracts;
using System;
using System.Threading.Tasks;

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
        Console.Write("Enter number of messages to generate (quit to exit): ");
        text = Console.ReadLine();

        int numMessages = 0;
        if (int.TryParse(text, out numMessages) && numMessages > 0)
        {
          Parallel.For(1, numMessages, i =>
          {
            var message = new SomethingHappenedMessage() { What = "message " + i.ToString(), When = DateTime.Now };
            bus.Publish<SomethingHappened>(message, x => { x.SetDeliveryMode(MassTransit.DeliveryMode.Persistent); });
          });
        }
        else
        {
          Console.WriteLine("\"" + text + "\" is not a number.");
        }
      }

      bus.Dispose();
    }
  }
}
