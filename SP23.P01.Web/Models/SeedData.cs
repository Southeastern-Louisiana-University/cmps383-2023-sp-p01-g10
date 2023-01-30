using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SP23.P01.Web.Data;
using SP23.P01.Web.Entities;
using System;
using System.Linq;
using System.Net;

namespace SP23.P01.Web.Models;

public class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {

        using (var context = new DataContext(
        serviceProvider.GetRequiredService <
        DbContextOptions < DataContext >> ()))
            {

                // Check that the database has not already been seeded.
                if (context.TrainStation.Any())
                {
                    return;   // It has, so don't initialize the database with data.
                }

                context.TrainStation.AddRange(

                new TrainStation
                {
                    Name = "Kyoto Station",
                    Address = "456 East Avenue"
                },
                new TrainStation
                {
                    Name = "Kosher Station",
                    Address = "123 West Avenue"
                },
                new TrainStation
                {
                    Name = "Keen Station",
                    Address = "789 North Avenue"
                }

                // etc.

            );

            context.SaveChanges();
            
        }
    }
}