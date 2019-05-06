using System.ComponentModel.DataAnnotations;

namespace Dg.ERM.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}