using System;

namespace App.Core.Entities
{
    public class Match : EntityBase
    {
      public Competition Competition { get; set; }
      public DateTime Date { get; set; }
      public Club Home { get; set; }
      public Club Away { get; set; }
      public Score Score { get; set; }
    }
}