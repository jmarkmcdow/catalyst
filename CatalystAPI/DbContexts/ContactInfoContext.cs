

using CatalystAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalystAPI.DBContexts
{
    public class ContactInfoContext : DbContext
    {
        public DbSet<Contact> Contacts {get; set;}
        public DbSet<Address> Addresses {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}