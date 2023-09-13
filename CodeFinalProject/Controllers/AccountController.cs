using CodeFinalProject.DAL;
using CodeFinalProject.Email;
using CodeFinalProject.Models;
using CodeFinalProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeFinalProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly HimaraDbContext _context;
        private readonly IMailService _mailService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, HimaraDbContext context, IMailService mailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _mailService = mailService;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(MemberRegisterViewModel memberVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser user = new AppUser
            {
                FullName = memberVM.FullName,
                Email = memberVM.Email,
                UserName = memberVM.UserName,
                PhoneNumber = memberVM.Phone
            };
            var result = await _userManager.CreateAsync(user, memberVM.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }
            await _userManager.AddToRoleAsync(user, "Member");
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(MemberLoginViewModel memberVM, string returnUrl = null)
        {
           
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser member = await _userManager.FindByNameAsync(memberVM.UserName);
            if (member == null)
            {
                ModelState.AddModelError("", "UserName or Password incorrect!");
                return View();
            }
            var result = await _signInManager.PasswordSignInAsync(member, memberVM.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "UserName or Password incorrect!");
                return View();
            }
            return returnUrl == null ? RedirectToAction("index", "home") : Redirect(returnUrl);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> Profile()
        {
            AppUser member = await _userManager.FindByNameAsync(User.Identity.Name);
            var reservations = await _context.Reservation.Include(x=>x.Room)
                 .Where(p => p.AppUserId == member.Id)
                 .ToListAsync();
            ProfileViewModel vm = new ProfileViewModel
            {
                Member = new MemberUpdateViewModel
                {
                    FullName = member.FullName,
                    Email = member.Email,
                    Phone = member.PhoneNumber,
                    UserName = member.UserName,
                },
                Reservations=reservations
            };
            return View(vm);
        }
        [HttpPost]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> MemberUpdate(MemberUpdateViewModel memberVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Tab = "Profile";
                ProfileViewModel vm = new ProfileViewModel() { Member = memberVM };
                return View("profile", vm);
            }
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            user.FullName = memberVM.FullName;
            user.PhoneNumber = memberVM.Phone;
            user.Email = memberVM.Email;
            user.UserName = memberVM.UserName;
            var result = await _userManager.UpdateAsync(user);
            var resultCurrentP = await _signInManager.PasswordSignInAsync(user, memberVM.CurrentPassword, false, false);
            if (!resultCurrentP.Succeeded)
            {

                ViewBag.Tab = "Profile";
                ModelState.AddModelError("", "Current password is incorrect");
                ProfileViewModel vm = new ProfileViewModel() { Member = memberVM };
                return View("profile", vm);
            }
            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                    ProfileViewModel vm = new ProfileViewModel() { Member = memberVM };
                    return View("profile", vm);
                }
            }
            if (memberVM.NewPassword != null)
            {
                var checkResult = await _signInManager.PasswordSignInAsync(user, memberVM.CurrentPassword, false, false);
                if (!checkResult.Succeeded)
                {
                    ModelState.AddModelError("CurrentPassword", "Pasword is incorrect");
                    ProfileViewModel vm = new ProfileViewModel() { Member = memberVM };
                    return View("profile", vm);
                }
                await _userManager.ChangePasswordAsync(user, memberVM.CurrentPassword, memberVM.NewPassword);
            }
            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("profile");
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel passwordVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser user = await _userManager.FindByEmailAsync(passwordVM.Email);
            if (user == null)
            {
                ModelState.AddModelError("Email", "Email is incorrect");
                return View();
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var url = Url.Action("verify", "account", new { email = passwordVM.Email, token = token }, Request.Scheme);
            await _mailService.SendEmailAsync(new MailRequest
            {
                ToEmail = user.Email,
                Subject = "Change Password",
                Body = $"<a href={url}  >Click Here</a>"
            });
            return RedirectToAction("index", "home");
        }
        public async Task<IActionResult> Verify(string email, string token)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);
            if (await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", token))
            {
                TempData["Email"] = email;
                TempData["Token"] = token;
                return RedirectToAction("resetpassword");
            }
            return View("Error");
        }
        public IActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPassword)
        {
            AppUser user = await _userManager.FindByEmailAsync(resetPassword.Email);
            var result = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
            if (!result.Succeeded)
            {
                return View("error");
            }
            return RedirectToAction("login");
        }
        public async Task<IActionResult> ConfirmedEmail(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return View("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded) 
            {
                return RedirectToAction("login");
            }
            else
            {
                return View("error");
            }
        }
    }
}
