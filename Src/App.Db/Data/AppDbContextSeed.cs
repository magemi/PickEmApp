using App.Core.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Db.Data
{
  public class AppDbContextSeed
  {
    public static async Task Seed(IApplicationBuilder builder, AppDbContext context, ILoggerFactory loggerFactory, int? retry = 0)
    {
      int retryForAvailability = retry.Value;

      try
      {
        // TODO: Only run this if using a real database
        // context.Database.Migrate();

        if (!context.Clubs.Any())
        {
          context.Clubs.AddRange(GetPreconfiguredClubs());

          await context.SaveChangesAsync();
        }
      }
      catch (Exception ex)
      {
        if (retryForAvailability < 10)
        {
          retryForAvailability++;
          var log = loggerFactory.CreateLogger<AppDbContextSeed>();
          log.LogError(ex.Message);
          await Seed(builder, context, loggerFactory, retryForAvailability);
        }
      }


    }

    static IEnumerable<Club> GetPreconfiguredClubs()
    {
      return new List<Club>()
      {
        new Club()
        {
          Name = "Atlanta United FC",
          Location = "Atlanta",
          Initialism = "ATL"
        },
        new Club()
        {
          Name = "Chicago Fire",
          Location = "Chicago",
          Initialism = "CHI"
        }
      };
    }
  }
}