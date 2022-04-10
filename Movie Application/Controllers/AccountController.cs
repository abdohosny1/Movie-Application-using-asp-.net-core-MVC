using Microsoft.AspNetCore.Mvc;
using Movie_Application.Data.Static;

namespace Movie_Application.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<IActionResult> Users()
        {
            var res = await _context.Users.ToListAsync();

            return View(res);
        }

        public IActionResult Login()
        {
            var response =new LoginVM();
            return View(response);
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }
            var user = await _userManager.FindByEmailAsync(loginVM.Emaill);
            if(user != null)
            {
                var passwordCheck=await _userManager.CheckPasswordAsync(user,loginVM.Password);

                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Movie");
                    }
                }
                
                
                    TempData["Error"] = "Wrong Email Credentials. please try again";
                    return View(loginVM);
     
            }
            TempData["Error"] = "Wrong password Credentials. please try again";
            return View(loginVM);


        }

      
        public IActionResult Register()
        {
            var response = new RegisterVM();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return  View(registerVM);

            var user = await _userManager.FindByEmailAsync(registerVM.Emaill);

            if (user != null)
            {
                TempData["Error"] = "This Email is already in use !";
                return View(registerVM);

            }
            var newUser = new ApplicationUser()
            {
                FullName = registerVM.FullName,
                Email=registerVM.Emaill,
                UserName=registerVM.Emaill,
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);
            if (newUserResponse.Succeeded)
            
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);


                return View("RegisterCompleted");
            


        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {

             await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Movie");
        }

    }
}
