using CarAuctionApi.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarAuctionApi.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasIndex(c => c.FinCode)
            .IsUnique();
        builder.Property(c => c.FinCode).HasMaxLength(7);

        builder.HasIndex(c => c.SerialNumber).IsUnique();
        builder.Property(c => c.SerialNumber).HasMaxLength(10);
    }
}