using Contracts;
using System;

namespace TestPublisher
{
  class SomethingHappenedMessage : SomethingHappened
  {
    public string What { get; set; }
    public DateTime When { get; set; }
  }
}
