using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Entitiy;

namespace WebApplication1.Data.Configuration
{
    public class QuestionCorrectInfoConfiguration : IEntityTypeConfiguration<QuestionCorrectInfoEntity>
    {
        public void Configure(EntityTypeBuilder<QuestionCorrectInfoEntity> builder)
        {
            builder.HasKey(q => q.Id);

            builder.HasOne(qci => qci.Question)
                .WithOne(q => q.QuestionCorrectInfo)
                .HasForeignKey <QuestionCorrectInfoEntity> (qci => qci.QuestionId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder
                .Property(qci => qci.Title)
                .IsRequired();

            builder
                .Property(qci => qci.ImageLink);

            builder
                .Property(qci => qci.VideoLink);
        }
    }

}
