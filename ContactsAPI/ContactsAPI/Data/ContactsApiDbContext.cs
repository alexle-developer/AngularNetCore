using ContactsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsAPI.Data
{
    public class ContactsApiDbContext : DbContext
    {
        public ContactsApiDbContext(DbContextOptions<ContactsApiDbContext> options) : base(options)
        {
            // do not forget to add Services DbContext in the program.cs
        }
        public DbSet<Contact> Contacts { get; set; }


    }
}
