using InsightAirport.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InsightAirport.Data.Map
{
    public class PilotMap : IEntityTypeConfiguration<PilotModel>
    {
        public void Configure(EntityTypeBuilder<PilotModel> builder)
        {
            builder.ToTable("Pilot");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.DateBirth).IsRequired();
            builder.HasOne(p => p.Airplane).WithMany(a => a.Pilots).HasForeignKey(p => p.AirplaneId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
