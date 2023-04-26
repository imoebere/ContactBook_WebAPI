using Data.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Contact.Extension
{
	public class TokenGen
	{ 
			private readonly IConfiguration _configuration;

			public TokenGen(IConfiguration configuration)
			{
				_configuration = configuration;
			}
			public string GenerateJWT(User user, string role)
			{
				var listOfClaims = new List<Claim>();
				listOfClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
				listOfClaims.Add(new Claim(ClaimTypes.Name, user.FirstName, user.LastName));
				listOfClaims.Add(new Claim(ClaimTypes.Role, role));
				
				var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Key").Value);
				
				var tokendescriptor = new SecurityTokenDescriptor
				{
					Subject = new ClaimsIdentity(listOfClaims),
					Expires = DateTime.UtcNow.AddDays(7),
					SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
				};
				
				var tokenHandler = new JwtSecurityTokenHandler();
				var createdToken = tokenHandler.CreateToken(tokendescriptor);

				var token = tokenHandler.WriteToken(createdToken);

				return token;   
			}
	}
}
