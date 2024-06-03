using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Entitiy;

namespace WebApplication1.Data.Configuration
{
    public class TestConfiguration : IEntityTypeConfiguration<TestEntity>
    {
        public void Configure(EntityTypeBuilder<TestEntity> builder)
        {
            builder.HasKey(t => t.Id);

            builder
            .Property(t => t.Title)
                .IsRequired();

            builder
            .Property(t => t.Description)
                .IsRequired();

            builder
                .Property(t => t.Category);

            builder
                .Property(t => t.Level)
                .IsRequired();

            builder
                .Property(t => t.CreatedDate)
                .IsRequired();

            builder
                .Property(t => t.IsPrivate)
                .IsRequired();


            builder
                .HasMany(q => q.Questions)
                .WithOne(t => t.Test);



        }
    }

}
