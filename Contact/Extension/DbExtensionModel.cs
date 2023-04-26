using Data;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Contact.Extension
{
	public static class DbExtensionModel
	{
		public static void DbModel(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			services.AddEndpointsApiExplorer();
			services.AddDbContext<ContactContext>(options =>
					options.UseSqlServer(configuration.GetConnectionString("Default"))
			);
			services.AddIdentity<User, IdentityRole>(option =>
			{
				//option.SignIn.RequireConfirmedEmail = true;
				option.User.RequireUniqueEmail = true;
				option.Password.RequireDigit = true;
			}).AddEntityFrameworkStores<ContactContext>();
		}
	}
}
