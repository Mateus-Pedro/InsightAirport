using InsightAirport.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace InsightAirport.Data.Map
{
    public class CommunicationLogMap : IEntityTypeConfiguration<CommunicationLogModel>
    {
        public void Configure(EntityTypeBuilder<CommunicationLogModel> builder)
        {
            builder.ToTable("CommunicationLog");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Message).IsRequired();
            builder.Property(x => x.Timestamp).IsRequired();
        }
    }
}