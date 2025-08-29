using GreenSpace.Data;
using GreenSpace.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GreenSpace.Controllers
{
    public class AuthsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthsController(ApplicationDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                ViewBag.Error = "Sai tên đăng nhập hoặc mật khẩu!";
                return View();
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
            if (result != PasswordVerificationResult.Success)
            {
                ViewBag.Error = "Sai tên đăng nhập hoặc mật khẩu!";
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role ?? "User")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties
            );

            if (user.Role?.Equals("Admin", StringComparison.OrdinalIgnoreCase) == true)
            {
                return RedirectToAction("Index", "Home");
            }

            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("Username", user.Username);

            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string username, string password, string email)
        {
            if (_context.Users.Any(u => u.Username == username))
            {
                ViewBag.Error = "Tên người dùng đã tồn tại.";
                return View();
            }

            var newUser = new User
            {
                Username = username,
                Password = _passwordHasher.HashPassword(null, password),
                Email = email,
                DisplayName = "user"
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            ViewBag.Message = "Đăng ký thành công.";
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(string username, string oldPassword, string newPassword)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                ViewBag.Error = "Không tìm thấy người dùng.";
                return View();
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, oldPassword);
            if (result != PasswordVerificationResult.Success)
            {
                ViewBag.Error = "Mật khẩu cũ không đúng.";
                return View();
            }

            user.Password = _passwordHasher.HashPassword(user, newPassword);
            _context.SaveChanges();

            ViewBag.Message = "Đổi mật khẩu thành công!";
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult SendOtp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendOtp(string email, [FromServices] EmailService emailService)
        {
            if (string.IsNullOrEmpty(email))
            {
                ViewBag.Error = "Vui lòng nhập email";
                return View();
            }

            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                ViewBag.Error = "Email không tồn tại";
                return View();
            }

            var otp = new Random().Next(100000, 999999).ToString();
            var expiration = DateTime.Now.AddMinutes(5);

            var otpEntity = new PasswordResetOtp
            {
                Email = user.Email,
                Otp = otp,
                ExpiresAt = expiration
            };

            _context.PasswordResetOtp.Add(otpEntity);
            _context.SaveChanges();

            emailService.SendEmail(
                email,
                "Mã OTP khôi phục mật khẩu",
                $"<p>Mã OTP của bạn là: <b>{otp}</b></p><p>Có hiệu lực trong 5 phút.</p>"
            );

            ViewBag.Message = $"OTP đã được gửi tới email: {email}";
            return RedirectToAction("VerifyOtp"); ;
        }

        [HttpGet]
        public IActionResult VerifyOtp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult VerifyOtp(string email, string otp)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(otp))
            {
                ViewBag.Error = "Vui lòng nhập email và OTP";
                return View();
            }

            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                ViewBag.Error = "Email không tồn tại";
                return View();
            }

            var otpEntity = _context.PasswordResetOtp
                                    .Where(o => o.Email == user.Email && o.UsedAt == null)
                                    .OrderByDescending(o => o.ExpiresAt)
                                    .FirstOrDefault();

            if (otpEntity == null)
            {
                ViewBag.Error = "OTP không tồn tại hoặc đã được sử dụng, vui lòng gửi lại OTP";
                return View();
            }

            if (otpEntity.Otp != otp)
            {
                ViewBag.Error = "OTP không chính xác";
                return View();
            }

            if (otpEntity.ExpiresAt < DateTime.Now)
            {
                ViewBag.Error = "OTP đã hết hạn, vui lòng gửi lại";
                return View();
            }

            otpEntity.UsedAt = DateTime.Now;
            _context.SaveChanges();

            TempData["UserId"] = user.Id;
            return RedirectToAction("ResetPassword");
        }

        [HttpGet]
        public IActionResult ResetPassword()
        {
            if (TempData["UserId"] == null)
            {
                return RedirectToAction("SendOtp");
            }

            ViewBag.UserId = TempData["UserId"];
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(int userId, string newPassword, string confirmPassword)
        {
            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                ViewBag.Error = "Vui lòng nhập mật khẩu mới";
                return View();
            }

            if (newPassword != confirmPassword)
            {
                ViewBag.Error = "Mật khẩu xác nhận không khớp";
                return View();
            }

            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                ViewBag.Error = "Người dùng không tồn tại";
                return View();
            }

            var hashedPassword = _passwordHasher.HashPassword(user, newPassword);
            user.Password = hashedPassword;

            _context.Users.Update(user);
            _context.SaveChanges();

            ViewBag.Message = "Đặt lại mật khẩu thành công! Bạn có thể đăng nhập lại.";
            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult Profile()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Auths");
            }

            var username = User.Identity.Name; 
            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            var viewModel = new UserEditViewModel
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Email = user.Email,
                Address = user.Address,
                Phone = user.Phone,
                Dob = user.Dob
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserEditViewModel model)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                TempData["Error"] = "Bạn cần đăng nhập để thực hiện chức năng này.";
                return RedirectToAction("Login", "Auths");
            }

            var existingUser = _context.Users.Find(userId);

            if (existingUser == null)
            {
                TempData["Error"] = "Không tìm thấy người dùng.";
                return RedirectToAction("Profile");
            }

            existingUser.DisplayName = model.DisplayName;
            existingUser.Email = model.Email;
            existingUser.Dob = model.Dob;
            existingUser.Address = model.Address;
            existingUser.Phone = model.Phone;

            if (ModelState.IsValid)
            {
                _context.SaveChanges();
                HttpContext.Session.SetString("Username", existingUser.Username);
                TempData["Message"] = "Thông tin cá nhân đã được cập nhật thành công!";
                return RedirectToAction("Profile");
            }

            return View(model);
        }

    }
}
