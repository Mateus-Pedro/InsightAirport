using InsightAirport.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace InsightAirport.Data.Map
{
    public class AirplaneMap : IEntityTypeConfiguration<AirplaneModel>
    {
        public void Configure(EntityTypeBuilder<AirplaneModel> builder)
        {
            builder.ToTable("Airplane");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.FlightStatus).IsRequired();
            builder.Property(x => x.ArrivalTime).IsRequired();
            builder.HasMany(a => a.Pilots).WithOne(p => p.Airplane).HasForeignKey(p => p.AirplaneId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}