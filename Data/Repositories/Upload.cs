using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Data.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
	public class Upload : IUpload
	{
		private readonly IConfiguration _config;
        public Upload(IConfiguration config)
        {
			_config = config;
        }
        public async Task<Dictionary<string, string>> UploadFileAsync(IFormFile file)
		{
			/*var account = new Account
			{
				ApiKey = _config.GetSection("Cloudary : ApiKey").Value,
				ApiSecret = _config.GetSection("Cloudary : ApiSecret").Value,
				Cloud = _config.GetSection("Cloudary : CloudName").Value
			};
			var cloudinary = new Cloudinary(account);*/

			var cloudinary = new Cloudinary(new Account("drffjvlyj", "576728253658965", "3GARx39w0SPGEyqhSVhlbL8JLNc"));

			if (file.Length > 0 && file.Length <= (1024 * 1024 * 2))
			{
				if(file.ContentType.Equals("image/png") || file.ContentType.Equals("image/jpg") || file.ContentType.Equals("image/jpeg")) 
				{
					var uploadResult = new ImageUploadResult();
					using(var stream = file.OpenReadStream()) 
					{
						var uploadparams = new ImageUploadParams
						{
							File = new FileDescription(file.FileName, stream),
							Transformation = new Transformation().Width(300).Height(300).Crop("fill").Gravity("face")
						};
						uploadResult = await cloudinary.UploadAsync(uploadparams);
					}
					var result = new Dictionary<string, string>();
					result.Add("publicId", uploadResult.PublicId);
					result.Add("Url", uploadResult.Url.ToString());
					return result;
				}
				else
				{
					return null;
				}

			}
			else
			{
				return null;
			}
		}
	}
}
