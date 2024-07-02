using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using MobleFinalServer.Controllers;

namespace MobleFinalServer.Service
{
    public class AuthorizationFilter : IAuthorizationFilter
    {
        private readonly int _requiredRole;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AuthorizationFilter> _logger;

        public AuthorizationFilter(int requiredRole, ILogger<AuthorizationFilter> logger, IHttpContextAccessor httpContextAccessor)
        {
            _requiredRole = requiredRole;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //세션에 저장해둔 사용자 권한
            var userRoleString = _httpContextAccessor.HttpContext.Session.GetString("UserRole");
            
            //세션에 문자열로 저장된 권한을 인트로 변환, 로그인 하지 않은 상태에선 null이므로 TryParse 사용.
            //지정된 권한을 만족하지 못하면 기존 페이지로 리다이렉트
            bool parse = int.TryParse(userRoleString, out var userRole);

            if (!parse || userRole < _requiredRole)
            {
                var session = _httpContextAccessor.HttpContext.Session;
                if (parse == false)
                {
                    session.SetString("AuthError", "로그인 후 이용 가능합니다.");
                }
                else
                {
                    session.SetString("AuthError", "권한이 없습니다.");
                }
                
                // 이전 페이지 URL 가져오기
                var referer = _httpContextAccessor.HttpContext.Request.Headers["Referer"].ToString();

                if (!string.IsNullOrEmpty(referer))
                {
                    context.Result = new RedirectResult(referer); // 이전 페이지로 리디렉트
                }
                else
                {
                    context.Result = new RedirectToActionResult("Index", "Home", null); // 기본 페이지로 리디렉트
                }
            }
        }
    }
}
