using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Entitiy;

namespace WebApplication1.Data.Configuration
{
    public class UserAnswerConfiguration : IEntityTypeConfiguration<UserAnswerEntity>
    {
        public void Configure(EntityTypeBuilder<UserAnswerEntity> builder)
        {
            builder.HasKey(ua => ua.Id);

            builder
                .HasOne(ua => ua.TestResult)
                .WithMany(tr => tr.UserAnswers)
                .HasForeignKey(ua => ua.TestResultId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder
                .HasOne(ua => ua.Question)
                .WithMany(q => q.UserAnswers)
                .HasForeignKey(ua => ua.QuestionId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder
                .HasOne(ua => ua.QuestionOption)
                .WithMany(qo => qo.UserAnswers)
                .HasForeignKey(ua => ua.QuestionOptionId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder
                .Property(ua => ua.IsCorrect)
                .IsRequired();
        }
    }

}
