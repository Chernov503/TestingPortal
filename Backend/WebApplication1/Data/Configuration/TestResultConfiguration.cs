using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Entitiy;

namespace WebApplication1.Data.Configuration
{
    public class TestResultConfiguration : IEntityTypeConfiguration<TestResultEntity>
    {
        public void Configure(EntityTypeBuilder<TestResultEntity> builder)
        {
            builder.HasKey(tr => tr.Id);

            builder
                .HasOne(tr => tr.User)
                .WithMany(u => u.TestResults)
                .HasForeignKey(tr => tr.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder
                .HasOne(tr => tr.Test)
                .WithMany(t => t.TestResults)
                .HasForeignKey(tr => tr.TestId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder
                .Property(tr => tr.ResultAnswers)
                .IsRequired();

            builder
                .Property(tr => tr.ResultPercent)
                .IsRequired();

            builder
                .HasMany(tr => tr.UserAnswers)
                .WithOne(ua => ua.TestResult);

        }
    }

}
