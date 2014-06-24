using System;

namespace Contracts
{
  public interface SomethingHappened
  {
    string What { get; }
    DateTime When { get; }
  }
}
