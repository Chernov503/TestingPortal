using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Configuration;
using WebApplication1.Data.Entitiy;

namespace WebApplication1.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> o) : base(o) { }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<TestEntity> Tests { get; set; }
        public DbSet<QuestionEntity> Questions { get; set; }
        public DbSet<QuestionOptionEntity> QuestionOptions { get; set; }
        public DbSet<AccesToTestEntity> AccesToTests { get; set; }
        public DbSet<TestResultEntity> TestResults { get; set; }
        public DbSet<UserAnswerEntity> UserAnswers { get; set; }
        public DbSet<QuestionCorrectInfoEntity> QuestionCorrectInfo { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TestConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionOptionConfiguration());
            modelBuilder.ApplyConfiguration(new AccesToTestConfiguration());
            modelBuilder.ApplyConfiguration(new TestResultConfiguration());
            modelBuilder.ApplyConfiguration(new UserAnswerConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionCorrectInfoConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
