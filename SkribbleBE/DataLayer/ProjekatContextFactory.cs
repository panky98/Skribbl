using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataLayer.Models
{
    public class ProjekatContextFactory : IDesignTimeDbContextFactory<ProjekatContext>
    {

        public ProjekatContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json")
             .Build();

           

            DbContextOptionsBuilder builder = new DbContextOptionsBuilder<ProjekatContext>();

            var connectionString = configuration.GetConnectionString("Konekcija");
            builder.UseSqlServer(connectionString);
            return new ProjekatContext(builder.Options);
        }
    }
}
