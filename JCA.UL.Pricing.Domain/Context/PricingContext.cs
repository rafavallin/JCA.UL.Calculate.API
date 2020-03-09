using JCA.UL.Pricing.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JCA.UL.Pricing.Domain.Context
{
    public class PricingContext : DbContext
    {
        private string connectionString = "Server=az-dev-sql-01.database.windows.net;Database=PricingDbHomolog;User Id=adminjca;Password=P@$$w0rd01;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
        public virtual DbSet<UsuarioMap> Usuarios { get; set; }
        public virtual DbSet<ParameterMap> Parameters { get; set; }
        public virtual DbSet<RuleMap> Rules { get; set; }
        public virtual DbSet<ProductMap> Products { get; set; }
        public virtual DbSet<RequestMap> Requests { get; set; }
        public virtual DbSet<RequestCalcMap> RequestCalcs { get; set; }
    }
}