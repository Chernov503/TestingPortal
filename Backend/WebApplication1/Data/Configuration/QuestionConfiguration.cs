using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Entitiy;

namespace WebApplication1.Data.Configuration
{
    public class QuestionConfiguration : IEntityTypeConfiguration<QuestionEntity>
    {
        public void Configure(EntityTypeBuilder<QuestionEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .HasOne(q => q.Test)
                .WithMany(t => t.Questions)
                .HasForeignKey(q => q.TestId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder
                .Property(q => q.QuestionTitle)
                .IsRequired();

            builder
                .Property(q => q.OptionCount)
                .IsRequired();

            builder
                .Property(q => q.CorrectOptionCount)
                .IsRequired();

            builder
                .HasMany(q => q.QuestionOptions)
                .WithOne(qo => qo.Question);
        }
    }

}
