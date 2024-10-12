using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Dominos.Bl;
using Pharmacy_Website.Models;
namespace Pharmacy_Website.Controllers
{
    public class UsersController : Controller
    {
        // ApplicationUser it's found DbContext => PharmacyContext it's inhertince from IdintityUser 
        UserManager<ApplicationUser> _userManger;
        SignInManager<ApplicationUser> _signInManager;
        public UsersController(UserManager<ApplicationUser> userManager ,
            SignInManager<ApplicationUser> signinManager)
        {
            _userManger = userManager;
            _signInManager = signinManager;
        }

        public IActionResult Login(string returnUrl)
        {
            UserModel model = new UserModel(){
                ReturnUrl = returnUrl,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserModel model)
        {
            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                UserName = model.Email
            };
            try
            {
                var loginResult = await _signInManager.PasswordSignInAsync(user.Email, model.Password, true, true);
               
                if (loginResult.Succeeded)
                {
                    if (string.IsNullOrEmpty(model.ReturnUrl))
                        return Redirect("~/");
                    else
                        return Redirect(model.ReturnUrl);
                }
            }
            catch (Exception ex)
            {

            }
            return View(new UserModel());
        }

        public IActionResult Register()
        {
            return View(new UserModel());
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserModel model)
        {
            if (!ModelState.IsValid)
                return View("Register", model);

            ApplicationUser user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
            };
            try
            {
                // assign password
                var result = await _userManger.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var loginResult = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);
                    var myUser = await _userManger.FindByEmailAsync(user.Email);
                    await _userManger.AddToRoleAsync(myUser, "Customer");
                    if (loginResult.Succeeded)
                        return RedirectToAction("Login");
                }
                else
                {
                    Console.WriteLine("successfuly login");
                    return View("Register", model);
                }
            }
            catch(Exception ex){ 
                Console.WriteLine(ex.ToString());
            }

            return View("Register", model);
        }

        public async Task<IActionResult> LoginOut() {
            await _signInManager.SignOutAsync();
            return Redirect("~/");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
