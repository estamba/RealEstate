using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.MVC.Models
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; } = true;


    }
    public class UpdateProfileVM
    {
        public Guid AgentId { get; set; }
         public string Email { get; set; }
        [Required] public string Name { get; set; }
        public string City { get; set; }
        [Required] public string PhoneNumber { get; set; }

        public Guid ProfilePhotoId { get; set; }
        public List<string> Cities { get; set; }

    }
    public class SignUpVm
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]

        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "Password mismatch")]
        public string ConfirmPassword { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }


    }
}
