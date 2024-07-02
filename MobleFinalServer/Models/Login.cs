using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobleFinalServer.Models
{
    public class Login
    {
        [Required(ErrorMessage = "이메일을 입력하세요.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "비밀번호를 입력하세요.")]
        public string Password { get; set; }
        [NotMapped]
		public string ErrorMessage { get; set; }
	}
}
