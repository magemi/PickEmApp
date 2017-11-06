using System;
using System.Collections.Generic;

namespace App.Core.Entities
{
  public class Round : EntityBase
  {
    public int Number { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public IEnumerable<Match> Matches { get; set; }
  }
}