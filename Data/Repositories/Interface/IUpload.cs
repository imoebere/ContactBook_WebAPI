using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interface
{
	public interface IUpload
	{
		Task<Dictionary<string, string>> UploadFileAsync(IFormFile file);
	}
}
