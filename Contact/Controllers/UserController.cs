using Data.Entities;
using Data.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Collections;

namespace Contact.Controllers
{

	[Route("api/[controller]")]
	//[Authorize(Policy = "RegularRole")]
	//[Authorize]
	public class UserController : ControllerBase
	{
		private readonly IUserRepository _userRepository;
		private readonly IUpload _upload;

		public UserController(IUserRepository userRepository, IUpload upload)
		{
			_userRepository = userRepository;
			_upload = upload;
		}

        [Authorize(Roles = "Regular")]
        [HttpPost("Upload")]
        public async Task<IActionResult> Upload([FromForm] UploadFileDTOs formFile)
		{
			var result = await _upload.UploadFileAsync(formFile.file);
			if (result != null)
				return Ok($"PublicId {result["publicId"]}, Url: {result["Url"]}");

			return BadRequest("Upload was not successful");
		}

        [Authorize(Roles = "Admin")]
        [HttpPost("Add-Role")]
		public async Task<IActionResult> AddRole([FromBody] RoleDTOs roleDTOs)
		{
			var result = await _userRepository.AddRole(roleDTOs);
			return Ok(result);
		}

		//[Authorize(Roles = "Admin")]
		[HttpGet]
		[Route("allcontact")]
		public async Task<IActionResult> GetAll(int page, int perpage)
		{
			return Ok(await _userRepository.GetAllUser(page, perpage));
		}

       // [Authorize(Roles = "Admin, Regular")]
        //[Authorize(Roles = "Regular")]
        [HttpGet("GetUser/{id}")]
		public async Task<IActionResult> GetContactById(string id)
		{
			try
			{
				var user = await _userRepository.GetById(id);
				return Ok(user);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

        [Authorize(Roles = "Regular")]
        [HttpPut("UpdateUser/{id}")]
		public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserDTO updateUser)
		{
			return Ok(await _userRepository.Update(id, updateUser));
		}

        [Authorize(Roles = "Admin")]
        [HttpGet("Searchterm")]
		public async Task<IActionResult> SearchUser(string name, int page, int perpage)
		{
			return Ok(await _userRepository.SearchByName(name, page, perpage));
		}

        /*[HttpPatch("UpdateImage/{id}")]
		public async Task<IActionResult> UpdateImage(string id, [FromForm] UpdateImageDTO updateImage)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _userRepository.UpdateImage(id, updateImage);
			if (result != null)
				return Ok(result);

			return BadRequest("Failed to update user");
		}*/

        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{id}")]
		public async Task<IActionResult> DeleteContact(string id)
		{
			var result = await _userRepository.Delete(id);
			return Ok(result);
		}
	}
}
