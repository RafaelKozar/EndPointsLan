using EndPoints.Core;
using EndPoints.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EndPoints.Data
{
    public class EndPointContext : DbContext, IUnitOfWork
    {
        public EndPointContext(DbContextOptions<EndPointContext> options) : base(options)
        {
        }   

        public DbSet<EndPointGyr> EndPoints { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "EndPoints");            
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;   
        }
    }
}
