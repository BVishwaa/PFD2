using Google.Cloud.Firestore;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace EldenGuide.Models
{
    [FirestoreData]
    public class User
    {
        //Username
        [FirestoreProperty]

        [Required]
        [StringLength(50)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        //Email
        [FirestoreProperty]

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email Address")]
        //[UserEmailValidation]
        public string Email { get; set; }

        //Password
        [FirestoreProperty]

        [Required]
        [StringLength(25, MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
