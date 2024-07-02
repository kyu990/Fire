using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MobleFinalServer.Models;
using MobleFinalServer.Repository;
using MobleFinalServer.Service;

namespace MobleFinalServer.Controllers
{
	[TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { 2 })]
	public class AuthorityController : Controller
	{
		private readonly UserRepository _userRepository;
		private readonly ILogger<AuthorityController> _logger;
		private const int pageSize = 10;

		public AuthorityController(UserRepository userRepository, ILogger<AuthorityController> logger)
		{
			_userRepository = userRepository;
			_logger = logger;
		}

		public async Task<IActionResult> Index(int pageNumber = 1)
		{
			string serial = HttpContext.Session.GetString("UserClient");

			IEnumerable<User> users = await _userRepository.GetUserBySerialAsync(serial, pageSize, pageNumber);
			int count = await _userRepository.GetUserCountAsync(serial);

			var viewModel = new AuthorityView
			{
				Authorities = users,
				CurrentPage = pageNumber,
				TotalPage = (int)Math.Ceiling(count / (double)pageSize)
			};

			return View(viewModel);
		}

		public async Task<IActionResult> EditAuthority(string email)
		{
            EditUser user = await _userRepository.GetEUserByEmailAsync(email);
			if (user == null)
			{
				return NotFound();
			}
			return View(user);
		}

		[HttpPost]
		public IActionResult EditAuthority(EditUser user)
		{
            if (ModelState.IsValid)
			{
				// 사용자 업데이트
				_userRepository.UpdateUser(user);
				return RedirectToAction("Index");
			}
			// 유효성 검사 실패 시, 사용자 정보를 다시 전달하여 동일한 뷰를 렌더링
			return View(user);
		}
	}
}
