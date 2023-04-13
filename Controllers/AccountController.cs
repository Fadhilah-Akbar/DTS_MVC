using DTS_WebApplication.Repository.Contracts;
using DTS_WebApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DTS_WebApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        [HttpGet]
        public IActionResult index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Register()
        {
            var gender = new List<SelectListItem>(){
            new SelectListItem{
                Text = "Male",
                Value = "0",
            },
            new SelectListItem{
                Text = "Female",
                Value = "1",
            }};

            ViewBag.Gender = gender;

            return View();
        }

        // POST - Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterVM registerVM)
        {
            var result = _accountRepository.Register(registerVM);
            if (result > 0)
            {
                return RedirectToAction("Login", "Account");
            }

            var gender = new List<SelectListItem>(){
            new SelectListItem{
                Text = "Male",
                Value = "0",
            },
            new SelectListItem{
                Text = "Female",
                Value = "1",
            }};

            ViewBag.Gender = gender;
            return View();
        }

        // GET - Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginVM loginVM)
        {
            var result = _accountRepository.Login(loginVM);
            if (result > 0)
            {
                return RedirectToAction("Index", "Home");
            }
            var message = "Email or Password is incorrect ";
            ViewBag.messagge = message;
            return View();
        }
    }
}
