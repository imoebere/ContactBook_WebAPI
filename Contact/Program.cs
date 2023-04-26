using NLog;
using NLog.Web;
using Contact.Extension;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");
try
{
var builder = WebApplication.CreateBuilder(args);
	// Add services to the container.
	builder.Services.DbModel(builder.Configuration);
	builder.Services.AuthorizationModel(builder.Configuration);
	builder.Services.AuthenticationModel(builder.Configuration);
	builder.Services.ServiceConfig(builder.Configuration);
	builder.Services.SwaggerConfig(builder.Configuration);

	var app = builder.Build();
	// Configure the HTTP request pipeline.
	if (app.Environment.IsDevelopment())
	{
		app.UseSwagger();
		app.UseSwaggerUI();
	app.UseExceptionHandler(builder =>
	{
		builder.Run(async context =>
		{
			await Task.Run(() => context.Response.StatusCode = (int)HttpStatusCode.InternalServerError);
			var error = context.Features.Get<IExceptionHandlerPathFeature>();
			logger.Error($"Error path: {error.Path}, Error thrown: {error.Error.Message} Inner Message: {error.Error.InnerException}");
		});
	});
	}
	app.UseHttpsRedirection();
	app.UseAuthentication();
	app.UseAuthorization();

	app.MapControllers();

	app.Run();

}
catch (Exception exception)
{
	// NLog: catch setup errors
	logger.Error(exception, "Stopped program because of exception");
	throw;
}
