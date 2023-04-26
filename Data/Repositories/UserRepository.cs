using CloudinaryDotNet.Actions;
using Data.Entities;
using Microsoft.EntityFrameworkCore.Query;
using Models;
using Data.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace Data.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly ContactContext _context;
		private readonly IPaginated _paginated;
		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;
		private readonly IConfiguration _config;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IUpload _upload;

		public UserRepository(ContactContext context, IPaginated paginated, IMapper mapper, UserManager<User> userManager, IConfiguration configuration,
			RoleManager<IdentityRole> roleManager, IUpload upload)
		{
			_paginated = paginated;
			_context = context;
			_mapper = mapper;
			_userManager = userManager;
			_config = configuration;
			_roleManager = roleManager;
			_upload = upload;
		}
		public async Task<ReponseObject> AddRole(RoleDTOs roleDTOs)
		{
			var res = new ReponseObject();
			if (roleDTOs.Roles != null && roleDTOs.Roles.Count > 0)
			{
				foreach (var role in roleDTOs.Roles)
				{
					await _roleManager.CreateAsync(new IdentityRole(role));
				}

				/*res.StatusCode = 200;
				res.Message = "Added Successfull";
				res.Data = roleDTOs.Roles;*/
				return ReponseObject.Success("Role added ", statusCode: 200);
			}
			/*res.StatusCode = 400;
			res.Message = "Null or empty entry!";*/
			return ReponseObject.Fail(new List<string> { $"Fail to add " }, 400);
		}
		public async Task<ReponseObject> AddUser(AddUserDTO addUserDto)
		{
			if (addUserDto == null)
				throw new ArgumentNullException(nameof(addUserDto));

			// Validate input data
			if (string.IsNullOrWhiteSpace(addUserDto.FirstName) || string.IsNullOrWhiteSpace(addUserDto.LastName) ||
				string.IsNullOrWhiteSpace(addUserDto.Email) || string.IsNullOrWhiteSpace(addUserDto.PasswordHash) ||
				string.IsNullOrWhiteSpace(addUserDto.PhoneNumber))
				return ReponseObject.Fail(new List<string> { $"Fields should not be empty " }, 400);

			if (await _context.Users.AnyAsync(u => u.Email == addUserDto.Email))
				return ReponseObject.Fail(new List<string> { "Email already exists" }, 400);

			var mappUser = _mapper.Map<User>(addUserDto);
			mappUser.UserName = addUserDto.Email;
			var result = await _userManager.CreateAsync(mappUser, addUserDto.PasswordHash);
			if (result.Succeeded)
			{
				var mapper = _mapper.Map<UserToReturnDTO>(mappUser);
				await _userManager.AddToRoleAsync(mappUser, "Regular");
				return ReponseObject.Success(mapper, "Added Successful", 200);
			}

			return ReponseObject.Fail(new List<string> { "Unable to add" }, 400);
		}

		public async Task<ReponseObject> Delete(string Id)
		{
			if (string.IsNullOrEmpty(Id))
				return ReponseObject.Fail(new List<string> { "Id provided must not be empty" }, 400);
			
			var contact = await GetById(Id);
			var delete = await _userManager.DeleteAsync(contact);
			if (delete.Succeeded)
			{
				return ReponseObject.Success(delete, "Deleted Successful", 200);
			}
			return ReponseObject.Fail(new List<string> { "Unable to delete" }, 400);
		}

		public async Task<IEnumerable<User>> GetAllUser(int page, int perpage)
		{
			var result = await _userManager.Users.ToListAsync();
			if (result.Count >= 0)
			{
				var paged = _paginated.Pagination(result, page, perpage);
				return paged;
			}
			return result;
		}

		public async Task<User> GetById(string id)
		{
			if (string.IsNullOrEmpty(id))
				return null;

			var user = await _userManager.FindByIdAsync(id);
			
			if (user != null)
				return user;

			return null;
		}
		public async Task<IEnumerable<User>> SearchByName(string search, int page, int perpage)
		{
			if (string.IsNullOrEmpty(search))
				return null;

			var result = await _userManager.Users.Where(u => u.FirstName.Contains(search) ||
											  u.LastName.Contains(search) ||
											  u.Email.Contains(search) ||
											  u.PhoneNumber.Contains(search)).ToListAsync();

			if (result.Count >= 0)
			{
				var paged = _paginated.Pagination(result, page, perpage);
				return paged;
			}
			return result;
		}

		public async Task<ReponseObject> Update(string id, UpdateUserDTO updateUser)
		{
			if (string.IsNullOrEmpty(id))
				return ReponseObject.Fail(new List<string> { $"Invalid : {id} " }, 400);

			if (updateUser == null)
				return ReponseObject.Fail(new List<string> { "Update fields should not be empty" }, 400);

			var user = await GetById(id);
			user.FirstName = updateUser.FirstName;
			user.LastName = updateUser.LastName;
			user.Email = updateUser.Email;
			user.UserName = updateUser.Email;
			user.PhoneNumber = updateUser.PhoneNumber;

			var update = await _userManager.UpdateAsync(user);
			if (update.Succeeded)
			{
				return ReponseObject.Success(updateUser, "Update was successful", 200);
			}
			return ReponseObject.Fail(new List<string> { "Update Fail" }, 400);
		}

		/*public async Task<User> UpdateImage(string id, UpdateImageDTO updateImage)
		{
			try
			{

				if (string.IsNullOrEmpty(id))
					throw new ArgumentNullException("Id provided must not be empty");

				if (updateImage == null)
					throw new ArgumentNullException(nameof(updateImage));

				var contact = await GetById(id);

				if (contact.Id != updateImage.Id)
					throw new ArgumentNullException("Id Mismatch");

				var update = await _upload.UploadFileAsync(updateImage.ImageUrl);
				//await _userManager.UpdateAsync(update)
				_context.Users.Update(contact);
				var status = _context.SaveChanges();
				if (status > 0)
					return contact;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			return null;
		}*/

	}
}

