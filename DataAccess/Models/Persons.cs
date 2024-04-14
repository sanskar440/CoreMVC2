using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Persons
    {
        [Key]
        public int PersonID { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name must be between {2} and {1} characters.", MinimumLength = 2)]
        [Remote("IsFirstNameAvailable", "Persons", HttpMethod = "POST", ErrorMessage = "First name is already in use.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name must be between {2} and {1} characters.", MinimumLength = 2)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(100, ErrorMessage = "Address must be between {2} and {1} characters.", MinimumLength = 5)]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [StringLength(50, ErrorMessage = "City must be between {2} and {1} characters.", MinimumLength = 2)]
        public string City { get; set; }

        public int SelectedOption { get; set; }
    }
}
