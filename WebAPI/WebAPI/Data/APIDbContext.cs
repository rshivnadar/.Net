using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class APIDbContext : DbContext
    {
        public APIDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Contact> Contacts { get; set; }

    }
}
