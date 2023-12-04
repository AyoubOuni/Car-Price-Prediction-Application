using Microsoft.EntityFrameworkCore;
using Projet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projet.Data
{
    public class AppDB:DbContext
    {
        public AppDB(DbContextOptions<AppDB> options):base(options)
        {

        }
        public DbSet <User> users { get; set; }
        public DbSet<ResetPassword> resetpasswords { get; set; }
        public DbSet<CarPredictionModel> CarPredictions { get; set; }

    }
}
