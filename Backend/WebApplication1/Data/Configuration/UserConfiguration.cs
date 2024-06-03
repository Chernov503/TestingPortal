using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Entitiy;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplication1.Data.Configuration
{


    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(b => b.Id);

            builder
                .Property(b => b.FirstName)
                .IsRequired();

            builder
                .Property(b => b.Surname)
                .IsRequired();

            builder
                .Property(b => b.Email)
                .IsRequired();

            builder
                .Property(b => b.Company)
                .IsRequired();

            builder
                .Property(b => b.Status);



        }
    }

}
