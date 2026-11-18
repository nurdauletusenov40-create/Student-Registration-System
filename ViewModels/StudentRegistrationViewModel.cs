using System.ComponentModel.DataAnnotations;

namespace StudentRegistrationSystem.ViewModels;

// Form/View model: validation rules belong here because they describe the registration form.
public class StudentRegistrationViewModel : IValidatableObject
{
    [Required(ErrorMessage = "Атыңызды енгізіңіз.")]
    [StringLength(40, MinimumLength = 2, ErrorMessage = "Атыңыз 2–40 таңбадан тұруы керек.")]
    [Display(Name = "Аты")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Тегіңізді енгізіңіз.")]
    [StringLength(40, MinimumLength = 2, ErrorMessage = "Тегіңіз 2–40 таңбадан тұруы керек.")]
    [Display(Name = "Тегі")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Электрондық поштаңызды енгізіңіз.")]
    [EmailAddress(ErrorMessage = "Электрондық пошта мекенжайын дұрыс енгізіңіз.")]
    [Display(Name = "Электрондық пошта")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Құпиясөзді енгізіңіз.")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Құпиясөз кемінде 8 таңбадан тұруы керек.")]
    [Display(Name = "Құпиясөз")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Құпиясөзді растаңыз.")]
    [Compare("Password", ErrorMessage = "Құпиясөздер сәйкес келмейді.")]
    [DataType(DataType.Password)]
    [Display(Name = "Құпиясөзді растау")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Жасыңызды енгізіңіз.")]
    [Range(16, 60, ErrorMessage = "Жасыңыз 16 мен 60 аралығында болуы керек.")]
    [Display(Name = "Жасы")]
    public int? Age { get; set; }

    [Required(ErrorMessage = "Университетіңізді немесе факультетіңізді көрсетіңіз.")]
    [Display(Name = "Университет / факультет")]
    public string Faculty { get; set; } = string.Empty;

    [Required(ErrorMessage = "Оқу курсын таңдаңыз.")]
    [Range(1, 4, ErrorMessage = "Курс 1 мен 4 аралығында болуы керек.")]
    [Display(Name = "Оқу курсы")]
    public int? Course { get; set; }

    [Required(ErrorMessage = "Үлгерім көрсеткішін енгізіңіз.")]
    [Range(typeof(decimal), "0", "4", ErrorMessage = "Үлгерім көрсеткіші 0 мен 4 аралығында болуы керек.")]
    [Display(Name = "Үлгерім көрсеткіші")]
    public decimal? GPA { get; set; }

    [Required(ErrorMessage = "Телефон нөміріңізді енгізіңіз.")]
    [RegularExpression(@"^\+?[0-9][0-9\-\s]{7,14}[0-9]$", ErrorMessage = "Телефон нөмірін дұрыс енгізіңіз. Мысалы: +77001234567.")]
    [Display(Name = "Телефон нөмірі")]
    [Phone(ErrorMessage = "Телефон нөмірін дұрыс енгізіңіз.")]
    public string PhoneNumber { get; set; } = string.Empty;

    [StringLength(250, ErrorMessage = "Бұл өрістегі мәтін 250 таңбадан аспауы керек.")]
    [Display(Name = "Өзім туралы")]
    public string? AboutMe { get; set; }

    // Custom class-level validation rule.
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!string.IsNullOrWhiteSpace(AboutMe))
        {
            var normalized = new string(AboutMe.Where(char.IsLetterOrDigit).ToArray()).ToLowerInvariant();
            if (normalized.Length >= 5)
            {
                var first = normalized[0];
                if (normalized.Take(normalized.Length).All(character => character == first))
                {
                    yield return new ValidationResult(
                        "Мәтінде бірдей таңбалар шамадан тыс қайталанбауы керек.",
                        new[] { nameof(AboutMe) });
                }
            }
        }
    }
}
