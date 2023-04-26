using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	public class ReponseObject
	{
		public int StatusCode { get; set; }
		public bool Status { get; set; }
		public string? Message { get; set; } = string.Empty;
		public IEnumerable<string>? Errors { get; set; } = Array.Empty<string>();
		public object Data { get; set; }
		public static ReponseObject Fail(IEnumerable<string> errors, int statusCode = (int)HttpStatusCode.BadRequest)
		{
			return new ReponseObject
			{
				Status = false,
				Errors = errors,
				StatusCode = statusCode
			};

		}
		public static ReponseObject Success(object data, string succssMessage = "", int statusCode = (int)HttpStatusCode.OK)
		{
			return new ReponseObject
			{
				Status = true,
				Message = succssMessage,
				Data = data,
				StatusCode = statusCode
			};
		}
	}
}
