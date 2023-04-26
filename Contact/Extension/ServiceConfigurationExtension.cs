using Data.Repositories.Interface;
using Data.Repositories;

namespace Contact.Extension
{
	public static class ServiceConfigurationExtension
	{
		public static void ServiceConfig(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IPaginated, Paginated>();
			services.AddScoped<TokenGen>();
			services.AddScoped<IUpload, Upload>();

			services.AddAutoMapper(typeof(Program));
		}
	}
}
