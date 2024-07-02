using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MobleFinalServer.Models;
using MobleFinalServer.Repository;
using MobleFinalServer.Service;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace MobleFinalServer.Controllers
{
    public class UserController : Controller
    {
        /// <summary>
        /// 공통
        /// </summary>
        private readonly ILogger<UserController> _logger;
        private readonly UserRepository _userRepository;
        private readonly EmailSender _emailSender;

        public UserController(ILogger<UserController> logger, UserRepository userRepository, EmailSender emailSender)
        {
            _logger = logger;
            _userRepository = userRepository;
            _emailSender = emailSender;
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// 회원가입 관련
        /// </summary>
        public IActionResult SignUp() //회원가입 페이지
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(User user)
        {
            user.EmailConfirmationToken = Guid.NewGuid().ToString();
            if (ModelState.IsValid)
            {
				if (user.ProfilePicture != null && user.ProfilePicture.Length > 0)
				{
					var filePath = FilePath.PicturePath+@$"\{user.Email}.jpeg";

					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await user.ProfilePicture.CopyToAsync(stream);
					}

					user.ProfilePicturePath = filePath;
				}

				_userRepository.AddUser(user);

                var token = user.EmailConfirmationToken;
                var callbackUrl = Url.Action("ConfirmEmail", "User", new { email = user.Email, token = token }, protocol: HttpContext.Request.Scheme);

                await _emailSender.SendEmailAsync(user.Email, $"<a href='{callbackUrl}'>여기를 클릭해주세요</a>.");

                HttpContext.Session.SetString("emailAuth", "인증 메일이 전송되었습니다.");
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                _logger.LogError(error.ErrorMessage);
            }

            _logger.LogInformation("필수 정보 누락으로 회원가입 실패");
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> CheckEmail(string email)
        {
            var isEmailDuplicate = await _userRepository.CheckEmailDuplicateAsync(email);
            _logger.LogInformation(isEmailDuplicate.ToString());
            _logger.LogInformation(email);
            if (isEmailDuplicate)
            {
                return Json(false); // 이미 사용 중인 이메일
            }
            return Json(true); // 사용 가능한 이메일
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            _logger.LogInformation($"Confirm email: {email} Token : {token}");
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user != null && token == user.EmailConfirmationToken)
            {
                bool updateResult = _userRepository.UpdateUser(email, true);
                if (updateResult)
                {
                    _logger.LogInformation("이메일 인증 성공 : {Email}", email);
                    return Redirect("SignIn"); // 인증 성공 뷰
                }
            }

            _logger.LogInformation("이메일 인증 실패 : {Email}", email);
            return View("Error"); // 인증 실패 뷰
        }

		[HttpGet]
		public async Task<IActionResult> GetProfilePicture(string email)
		{
			var path = await _userRepository.GetProfilePathByEmailAsync(email);
            _logger.LogInformation(path);
			if (path == null || string.IsNullOrEmpty(path))
			{
				return NotFound();
			}

			var fileBytes = System.IO.File.ReadAllBytes(path);
			return File(fileBytes, "image/jpeg"); // 또는 적절한 이미지 MIME 타입
		}

		/// <summary>
		/// 로그인 관련
		/// </summary>
		public IActionResult SignIn() //로그인 페이지
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(Login user) //로그인 버튼
        {
            if (await _userRepository.LoginAsync(user))
            {
                User temp = await _userRepository.GetUserByEmailAsync(user.Email);
                HttpContext.Session.SetString("UserId", temp.Email);
                HttpContext.Session.SetString("UserRole", temp.Authority.ToString());
                HttpContext.Session.SetString("UserClient", temp.ClientSerial);
                return RedirectToAction("Index", "Home");
            }
            user.ErrorMessage = "아이디 혹은 비밀번호가 일치하지 않습니다.";
            return View(user);
        }


        /// <summary>
        /// 로그아웃 관련
        /// </summary>
        public IActionResult SignOut() //로그아웃 버튼
        {
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("UserRole");
            HttpContext.Session.Remove("UserClient");
            return RedirectToAction("Index", "Home");
        }


        /// <summary>
        /// 프로필
        /// </summary>
        public async Task<IActionResult> Profile()
        {
            string email = HttpContext.Session.GetString("UserId");
            if (email == null)
            {
                return RedirectToAction("SignIn");
            }

            User user = await _userRepository.GetUserByEmailAsync(email);
            return View(user);
        }
        [HttpPost]
        public IActionResult Profile(User user)
        {
            if (ModelState.IsValid)
            {
                //DB에서 데이터 수정 로직
                _userRepository.UpdateUser(user);
                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }

        /// <summary>
        /// 회원 탈퇴
        /// </summary>
        public IActionResult Withdraw()
        {
            string email = HttpContext.Session.GetString("UserId");
            if (email != null)
            {
                _userRepository.DeleteUser(email);
                HttpContext.Session.Remove("UserId");
                HttpContext.Session.Remove("UserRole");
                HttpContext.Session.Remove("UserClient");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
