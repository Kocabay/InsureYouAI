using InsureYouAI.Dtos.RegisterDto;
using InsureYouAI.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAI.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserRegisterDto createRegisterDto)
        {
            AppUser appUser = new AppUser
            {
                Name = createRegisterDto.Name,
                Surname = createRegisterDto.Surname,
                UserName = createRegisterDto.Username,
                Email = createRegisterDto.Email,
                Password = createRegisterDto.Password,
                Imageurl = "TEST",
                Description = "TEST"
            };
            await _userManager.CreateAsync(appUser, createRegisterDto.Password);
            return RedirectToAction("UserList");
        }
    }
}
