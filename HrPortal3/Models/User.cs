using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrPortal3.Models
{
    public class User:IdentityUser
    {
        public string Name { get; set; }
        [DisplayName("Password")]
        public string SimPassword { get; set; }
    }
}
