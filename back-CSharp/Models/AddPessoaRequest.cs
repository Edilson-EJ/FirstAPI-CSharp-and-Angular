using System.ComponentModel.DataAnnotations;

namespace ApiAngularCsharp.Models
{
    public record AddPessoaRequest(
        [Required(ErrorMessage = "The name is required.")]
        [StringLength(100, ErrorMessage = "The name must not exceed 100 characters.")]
        string Name,

        [Required(ErrorMessage = "The email address is mandatory.")]
        [EmailAddress(ErrorMessage = "The email address must be valid.")]
        string Email,

        [Required(ErrorMessage = "Age is mandatory.")]
        [Range(0, 150, ErrorMessage = "The age must be between 0 and 150.")]
        int Age,

        [Required(ErrorMessage = "The date of birth is mandatory.")]
        [DataType(DataType.Date)]
        DateTime BirthDate
    );
}