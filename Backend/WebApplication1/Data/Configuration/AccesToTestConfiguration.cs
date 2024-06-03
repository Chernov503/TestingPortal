using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Entitiy;

namespace WebApplication1.Data.Configuration
{
    public class AccesToTestConfiguration : IEntityTypeConfiguration<AccesToTestEntity>
    {
        public void Configure(EntityTypeBuilder<AccesToTestEntity> builder)
        {
            builder.HasKey(a => a.Id);

            builder
                .HasOne(a => a.User)
                .WithMany(u => u.AccesToTests)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder
                .HasOne(a => a.Test)
                .WithMany(t => t.AccesToTests)
                .HasForeignKey(t => t.TestId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

        }
    }

}
