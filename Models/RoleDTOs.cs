using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	public class RoleDTOs
	{
		public List<string> Roles { get; set; }
		public RoleDTOs()
		{ 
			Roles = new List<string>();
		}
	}
}
