using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Data.Repository;
using WebApplication1.Services;
using WebApplication1.Abstractions;
using WebApplication1.Infrastructure.AutoMapper;
using WebApplication1.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var configuration = builder.Configuration;

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddCors();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey
                            (Encoding.UTF8.GetBytes("SecretSecretSecrxzcxzcxzczxcxzczxcxzcxcxczxcxzczxczxczxccxzcxzczxcz"))
                };
            });
        builder.Services.AddAuthorization();

        builder.Services.AddAutoMapper(
        typeof(QuestionOptionProfile),
        typeof(QuestionProfile),
        typeof(TestProfile),
        typeof(TestResultProfile),
        typeof(UserProfile),
        typeof(QuestionCorrectInfoProfile)
        );

        builder.Services.AddScoped<ITestRepository, TestRepository>();
        builder.Services.AddScoped<IQuestionOptionRepository, QuestionOptionRepository>();
        builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
        builder.Services.AddScoped<IUserAnswerRepository, UserAnswerRepository>();
        builder.Services.AddScoped<ITestResultRepository, TestResultRepository>();
        builder.Services.AddScoped<IQuestionsCorrectInfoRepository, QuestionsCorrectInfoRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IAccessRepository, AccessRepository>();

        builder.Services.AddScoped<IModeratorService, ModeratorService>();
        builder.Services.AddScoped<ISuperAdminService, SuperAdminService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IAdminService, AdminService>();

        builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
        builder.Services.AddScoped<IJwtProvider, JwtProvider>();

        builder.Services.AddScoped<ICustomFilterService, CustomFilterService>();




        builder.Services.AddDbContext<ApiDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("ApiDbContext"));
        });

        var app = builder.Build();

        MigrateDb(app);

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();


        void MigrateDb(WebApplication app)
        {
            var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();

            using var scope = scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApiDbContext>();
            dbContext.Database.Migrate();
        }
    }
}