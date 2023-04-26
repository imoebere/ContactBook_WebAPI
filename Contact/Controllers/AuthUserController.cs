using Contact.Extension;
using Data.Entities;
using Data.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;


namespace Contact.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthUserController : ControllerBase
	{
		private readonly IUserRepository _userRepository;
		private readonly IConfiguration _config;
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;

		public AuthUserController(IUserRepository userRepository, IConfiguration config, UserManager<User> userManager, SignInManager<User> signInManager)
		{
			_userRepository = userRepository;
			_config = config;
			_userManager = userManager;
			_signInManager = signInManager;
		}


		[HttpPost("RegisterUser")]
		public async Task<IActionResult> AddUser([FromBody] AddUserDTO addUserDto)
		{
			try
			{
				var result = await _userRepository.AddUser(addUserDto);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("Login")]
		public async Task<IActionResult> Login([FromBody] LoginDTOs loginDTOs)
		{
			try
			{

				var user = await _userManager.FindByEmailAsync(loginDTOs.Email);
				var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
				if (user != null)
				{
					var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTOs.Password, false);
					if (result.Succeeded)
					{
						var jwt = new TokenGen(_config);
						var token = jwt.GenerateJWT(user, role);
						var res = ReponseObject.Success(token, "Login Successfull", 200);
						return Ok(res);
					}
					return Ok(ReponseObject.Fail(new List<string> { "Invalid Email" }, 400));
				}
				else
					return Ok(ReponseObject.Fail(new List<string> { "Login credentials invalid" }, 400));
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
