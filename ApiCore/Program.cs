
using ApiCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Console;
using Serilog;

namespace ApiCore
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Host.UseSerilog();

			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Debug()
				.WriteTo.File(@"MyLogs/apiLog")
				.CreateLogger();

			// Add services to the container.

			builder.Services.AddDbContext<ApplicationDbContext>(option =>
			{
				string connectionString = builder.Configuration.GetConnectionString("DbConnection")!;
				option.UseSqlServer(connectionString);
			});

			builder.Services.AddResponseCaching();

			builder.Services.AddControllers().AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(); 

			var app = builder.Build();

			app.UseResponseCaching();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			app.UseSerilogRequestLogging();

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
