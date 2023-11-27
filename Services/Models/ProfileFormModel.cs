using System.ComponentModel.DataAnnotations;

namespace Services.Models
{
    public class ProfileFormModel
    {
        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string Street { get; set; }
        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string City { get; set; }
        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string ZipCode { get; set; }
        [Required]
        [StringLength(2)]
        public string Country { get; set; }
        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string EmergencyContactName { get; set; }
        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string EmergencyContactRelationship { get; set; }
        [Required]
        public string EmergencyContactPhoneNumber { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password should contain at least 8 characters including at least 1 number, upper and lower character and special character e.g. \"StrongPwd2$\"")]

        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Passwords don't match.")]
        [DataType(DataType.Password)]

        public string ConfirmPassword { get; set; }
        public List<string>Roles { get; set; }
    }
}