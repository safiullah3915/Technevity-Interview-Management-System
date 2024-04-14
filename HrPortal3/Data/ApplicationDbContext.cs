using HrPortal3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace HrPortal3.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }
        
        public DbSet<HrPortal3.Models.Interviewee> Interviewee { get; set; } = default!;
        public DbSet<HrPortal3.Models.Panel> Panel { get; set; } = default!;
        public DbSet<HrPortal3.Models.Post> Post { get; set; } = default!;
        public DbSet<HrPortal3.Models.User> ApplicationUsers { get; set; } = default!;
       
    }
}