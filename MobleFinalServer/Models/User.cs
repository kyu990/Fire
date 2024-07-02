using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MobleFinalServer.Models
{
	public class User
	{
		//모델 검증을 위한 어노테이션
		[Key]
		[StringLength(30)]
		public string Email { get; set; }

		[Required]
		[StringLength(20, MinimumLength = 6, ErrorMessage = "비밀번호는 6~20 길이를 가져야 함")]
		public string Password { get; set; }

		[NotMapped]
		[Compare("Password")]
		public string ConfirmPassword { get; set; }

		[Required]
		[StringLength(10)]
		public string Name { get; set; }

		[Required]
		[StringLength(50)]
		public string Address { get; set; }

		public int Authority { get; set; }

		[Required]
		[StringLength(11)]
		public string Tel { get; set; }

		[Required]
		[StringLength(30)]
		public string ClientSerial { get; set; }
        public bool IsEmailConfirmed { get; set; } // 이메일 인증 여부
        public string? EmailConfirmationToken { get; set; } // 이메일 인증 토큰
		public string? ProfilePicturePath { get; set; }

		[NotMapped]
		public IFormFile ProfilePicture { get; set; }
	}

	public class EditUser
	{
        //모델 검증을 위한 어노테이션
        [Key]
        [StringLength(30)]
        public string Email { get; set; }

        [Required]
        [StringLength(10)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Address { get; set; }

        [Required]
        public int Authority { get; set; }

        [Required]
        [StringLength(11)]
        public string Tel { get; set; }
    }
}
