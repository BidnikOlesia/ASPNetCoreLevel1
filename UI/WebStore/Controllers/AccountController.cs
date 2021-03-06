using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using WebStore.Domain.ViewsModels;

namespace WebStore.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _SignInManager;
        private readonly ILogger<AccountController> logger;

        public AccountController(UserManager<User> UserManager, SignInManager<User> SignInManager, ILogger<AccountController> Logger)
        {
            _UserManager = UserManager;
            _SignInManager = SignInManager;
            logger = Logger;
        }

        #region Register
        [AllowAnonymous]
        public IActionResult Register() => View(new RegisterUserViewModel());

        [AllowAnonymous]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel Model)
        {
            if (!ModelState.IsValid) return View(Model);

            logger.LogInformation($"Регистрация нового пользователя {Model.UserName}");

            var user = new User
            {
                UserName = Model.UserName
            };
            var register_result = await _UserManager.CreateAsync(user, Model.Password);
            if (register_result.Succeeded)
            {
                logger.LogInformation($"Пользователь {Model.UserName} успешно зарегистрирован");
                await _UserManager.AddToRoleAsync(user, Role.Users);
                logger.LogInformation($"Пользователю {Model.UserName} назначена роль {Role.Users}");

                await _SignInManager.SignInAsync(user, false);

                logger.LogInformation($"Пользователь {Model.UserName} автоматически вошел в систему после регистрации");
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in register_result.Errors)
                ModelState.AddModelError("", error.Description);

            logger.LogWarning($"Ошибка при регистрации пользователя {Model.UserName}: {string.Join(", ", register_result.Errors.Select(err=>err.Description))}");

            return View(Model);
        }
        #endregion

        #region Login
        [AllowAnonymous]
        public IActionResult Login(string returnUrl) => View(new LoginViewModel { ReturnUrl = returnUrl});

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel Model)
        {
            if (!ModelState.IsValid) return View(Model);

            var login_result = await _SignInManager.PasswordSignInAsync(
                Model.UserName, 
                Model.Password, 
                Model.RememberMe,
#if DEBUG
                false
#else
                true
#endif
                );
            if (login_result.Succeeded)
            {
                //if (Url.IsLocalUrl(Model.ReturnUrl))
                //    return Redirect(Model.ReturnUrl);
                //else
                //    return RedirectToAction("Index", "Home");
                logger.LogInformation($"Пользователь {Model.UserName} успешно вошел в систему");
                return LocalRedirect(Model.ReturnUrl??"/");
            }

            ModelState.AddModelError("", "Ошибка в имени пользователя или пароле");
            logger.LogInformation($"Ошибка при указании учетных данных в процессе входа {Model.UserName} в систему");
            return View(Model);
             
        }
        #endregion

        public async Task<IActionResult> Logout()
        {
            var userName = User.Identity!.Name;
            await _SignInManager.SignOutAsync();
            logger.LogInformation($"Пользователь {userName} вышел из системы");
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied() => View();

    }
}
