using Contracts;
using MassTransit;
using System;

namespace TestSubscriber
{
  class SomethingHappenedConsumer : Consumes<SomethingHappened>.Context
  {
    public void Consume(IConsumeContext<SomethingHappened> message)
    {
      Console.Write("TXT: " + message.Message.What);
      Console.Write("  SENT: " + message.Message.When.ToString());
      Console.Write("  PROCESSED: " + DateTime.Now.ToString());
      Console.WriteLine(" (" + System.Threading.Thread.CurrentThread.ManagedThreadId.ToString() + ")");
    }
  }
}
