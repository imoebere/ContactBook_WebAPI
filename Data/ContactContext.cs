using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
	public class ContactContext : IdentityDbContext<User>
	{
        public ContactContext (DbContextOptions<ContactContext> options) : base(options)
        { }
	}
}