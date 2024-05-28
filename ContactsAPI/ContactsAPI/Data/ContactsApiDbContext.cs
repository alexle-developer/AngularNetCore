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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // if we use Identity, we should use base.OnModelCreating(modelBuilder);
            // otherwise, we can remove the base.OnModelCreating(modelBuilder);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contact>().HasData(new Contact
            {
                Id = Guid.NewGuid(),
                Fullname = "Unknown Data",
                Phone = 1112223333,
                Email = "unknown@email.com",
                Address = "1234 Seed Data St",
                Nickname = "seed data"
            });
        }
    }
}
