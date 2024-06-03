using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Entitiy;

namespace WebApplication1.Data.Configuration
{
    public class QuestionOptionConfiguration : IEntityTypeConfiguration<QuestionOptionEntity>
    {
        public void Configure(EntityTypeBuilder<QuestionOptionEntity> builder)
        {
            builder.HasKey(qo => qo.Id);

            builder
                .HasOne(qo => qo.Question)
                .WithMany(q => q.QuestionOptions)
                .HasForeignKey(qo => qo.QuestionId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder
                .Property(qo => qo.Text)
                .IsRequired();

            builder
                .Property(qo => qo.IsCorrect)
                .IsRequired();
        }
    }

}
