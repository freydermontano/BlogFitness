using System.ComponentModel.DataAnnotations;

namespace BlogFitnessApp.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        public string Password { get; set; }
    }

}
