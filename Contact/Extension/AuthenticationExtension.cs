using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Contact.Extension
{
	public static class AuthenticationExtension
	{
		public static void AuthenticationModel(this IServiceCollection services,  IConfiguration configuration)
		{
			services.AddAuthentication(options =>
			{
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(option =>
			{
				//option.SaveToken = true;
				//option.RequireHttpsMetadata = false;
				var key = Encoding.UTF8.GetBytes(configuration.GetSection("JWT:Key").Value);
				option.TokenValidationParameters = new TokenValidationParameters
				{
					//ValidateIssuer = true,
					//ValidateAudience = true,
					//ValidAudience = configuration["JWT : Audience"],
					//ValidIssuer = configuration["JWT : Issuer"],
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateAudience = false, 
					ValidateIssuer = false
					
				};
			});

		}

		public static void AuthorizationModel(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddAuthorization(options =>
			{
				options.AddPolicy("AdminRole", policy => policy.RequireRole("Admin"));
				options.AddPolicy("RegularRole", policy => policy.RequireRole("Regular"));
			});
			/*services.AddAuthorization(options =>
			{
				options.AddPolicy("RegularRole", policy => policy.RequireRole("Regular"));
			});*/
		}
	}
}
