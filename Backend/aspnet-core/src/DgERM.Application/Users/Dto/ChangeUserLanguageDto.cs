using System.ComponentModel.DataAnnotations;

namespace DgERM.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}