namespace StudentRegistrationSystem.Models;

// Public domain model: it deliberately does not contain Password or ConfirmPassword.
public class Student
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Faculty { get; set; } = string.Empty;
    public int Course { get; set; }
    public decimal GPA { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string? AboutMe { get; set; }
}
