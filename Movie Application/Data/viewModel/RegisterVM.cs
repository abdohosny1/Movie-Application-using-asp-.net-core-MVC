namespace Movie_Application.Data.viewModel
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Name is Requried")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Required(ErrorMessage ="Emaill is Requried")]
        [Display(Name = "Emaill Address")]
        public string Emaill { get; set; }
        [Required(ErrorMessage = "Password is Requried")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Conifrm Password is Requried")]
        [Display(Name = "Conifrm Password")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password do Not Match")]
        [NotMapped]
        public string ConfirmPassword { get; set; }
    }
}
