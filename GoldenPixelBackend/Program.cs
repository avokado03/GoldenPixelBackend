using GoldenPixel.Common.Middleware.Configurations;
using GoldenPixel.Db;
using LinqToDB;
using LinqToDB.AspNet;
using LinqToDB.AspNet.Logging;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
using System.Reflection;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

var builder = WebApplication.CreateBuilder(args);

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
	builder.Services.AddLinqToDBContext<GpDbConnection>((provider, options)
				=> options
					.UsePostgreSQL(builder.Configuration.GetConnectionString("GpDb"))
					.UseDefaultLogging(provider));

	var environment = builder.Environment.EnvironmentName;
	builder.Services.AddControllers();
	builder.Configuration.AddJsonFile("appsettings.json", false, true);
	builder.Configuration.AddJsonFile($"appsettings.{environment}.json", true);
	builder.Configuration.AddEnvironmentVariables();
	builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly());

	builder.Services.AddEndpointsApiExplorer();
	builder.Services.AddSwaggerGen(c => SwaggerMiddlewareConfigurations.ConfigureSwaggerGenOptions(c));

	builder.Services.AddLogging(logger =>
	{
		logger.ClearProviders();
		logger.SetMinimumLevel(LogLevel.Information);
	});

	builder.Services.AddSingleton<ILoggerProvider, NLogLoggerProvider>();

	string? specificOrigins = "SpecificOrigins";
	builder.Services.AddCors(options =>
	{
		options.AddPolicy(name: specificOrigins,
		policy =>
		{
			policy
			.WithOrigins(
				"http://localhost:5000",
				"https://localhost:5000",
				"http://localhost:80",
				"https://localhost:80",
				"https://golden-pixel.kz"
				)
			.SetIsOriginAllowedToAllowWildcardSubdomains()
			.AllowCredentials()
			.WithMethods("POST", "PUT", "DELETE", "OPTIONS")
			.WithHeaders("Origin", "X-Requested-With", "Content-Type", "Accept", "Authorization");
		});
	});

	var app = builder.Build();

	if (app.Environment.IsDevelopment())
	{
		app.UseSwagger(c =>
			c.RouteTemplate = "api/swagger/{documentName}/swagger.json");
		app.UseSwaggerUI(c => SwaggerMiddlewareConfigurations.GetSwaggerUIOptions(c));
	}

	app.UseHttpsRedirection();

	app.UseAuthorization();

	app.MapControllers();

	app.Run();
}
catch(Exception ex)
{
	logger.Error(ex);
}
finally
{
	LogManager.Shutdown();
}
