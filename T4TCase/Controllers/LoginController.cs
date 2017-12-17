using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using T4TCase.Data;
using T4TCase.Model;
using T4TCase.ViewModel;

namespace T4TCase.Controllers
{
    public class LoginController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public LoginController(DatabaseContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            //check if User is logged in
            if (User.Identity.IsAuthenticated) return RedirectToAction("Order", "Order");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            //log in 
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(vm.UserName, vm.Password, vm.RememberMe, false);
                if (result.Succeeded) return RedirectToAction("Order", "Order", vm);
            }
            ModelState.AddModelError("", "Wrong user information.");
            return View(vm);
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            //create User
            if (ModelState.IsValid)
            {
                var user = new User { UserName = vm.UserName, Email = vm.Email };
                var result = await _userManager.CreateAsync(user, vm.Password);

                //add Customer linked to the user
                _context.Customer.Add(new Customer
                {
                    UserName = vm.UserName,
                    LastName = vm.LastName,
                    FirstName = vm.FirstName,
                    Email = vm.Email,
                    Age = vm.Age,
                    PhoneNumber = vm.PhoneNumber,
                    Address = vm.Address,
                    City = vm.City
                });
                _context.SaveChanges();

                //give the User a Member Role
                if (result.Succeeded)
                {
                    var role = await _userManager.AddToRoleAsync(user, "Member");

                    //Log in
                    if (role.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, false);
                        return RedirectToAction("Order", "Order");
                    }
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(vm);
        }

        [HttpGet]
        public async Task<RedirectToActionResult> Logout()
        {
            //Log out
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Login");
        }
    }
}