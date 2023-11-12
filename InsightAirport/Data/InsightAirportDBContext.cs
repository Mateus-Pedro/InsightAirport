using InsightAirport.Data.Map;
using InsightAirport.Models;
using Microsoft.EntityFrameworkCore;

namespace InsightAirport.Data
{
    public class InsightAirportDBContext: DbContext
    {
        public InsightAirportDBContext(DbContextOptions<InsightAirportDBContext> options) 
            : base(options) 
        {
        }

        public DbSet<PilotModel> Pilots { get; set; }
        public DbSet<AirplaneModel> Airplanes { get; set; }
        public DbSet<CommunicationLogModel> CommunicationLogs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PilotMap());
            modelBuilder.ApplyConfiguration(new AirplaneMap());
            modelBuilder.ApplyConfiguration(new CommunicationLogMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
