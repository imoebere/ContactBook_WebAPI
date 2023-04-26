using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	public class UpdateImageDTO
	{
		public string Id { get; set; }

		[Required]
		public IFormFile ImageUrl { get; set; } 
	}
}
