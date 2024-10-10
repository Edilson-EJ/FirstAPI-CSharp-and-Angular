using System.ComponentModel.DataAnnotations;

namespace ApiAngularCsharp.Models
{
    public record AddPessoaRequest(
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        string Name,

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email deve ser válido.")]
        string Email,

        [Required(ErrorMessage = "A idade é obrigatória.")]
        [Range(0, 150, ErrorMessage = "A idade deve estar entre 0 e 150.")]
        int Age,

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [DataType(DataType.Date)]
        DateTime BirthDate
    );
}