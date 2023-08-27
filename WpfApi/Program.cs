using DataLibrary;
using Microsoft.EntityFrameworkCore;
using WpfApi.Extensions;
using Microsoft.Extensions.DependencyInjection;
using DataLibrary.AutoMapper;

namespace WpfApi
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 根据环境变量合并配置文件
            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<DbWPFContext>(option => option.UseSqlServer(configurationRoot.GetConnectionString("DefaultConnection")));
            builder.Services.AddExtensionScope();
            builder.Services.AddAutoMapper(c => c.AddProfile(new AutoMapperProFile()));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}