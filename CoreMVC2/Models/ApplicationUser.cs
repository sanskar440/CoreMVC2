using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
namespace CoreMVC2.Models
{
    [Table("AspNetUsers")]
    public class ApplicationUser:IdentityUser
    {
        public string firstName {  get; set; }
        public string lastName { get; set; }
    }
}
