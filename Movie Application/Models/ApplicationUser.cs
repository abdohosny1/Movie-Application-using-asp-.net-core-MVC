﻿
namespace Movie_Application.Models
{
    public class ApplicationUser :IdentityUser
    {
        [Display(Name ="Full Name")]
        public string FullName { get; set; }
    }
}
