namespace ApiAngularCsharp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Pessoa
{
    public Guid Id { get; set; }
    
    [Range(0, 120, ErrorMessage = "Age must be between 0 and 120.")]
    public int Age { get; set; }
    
    public string Name { get; set; }
    public string Email { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime BirthDate { get; set; }

    public Pessoa() { }

    public Pessoa(Guid id, string name, string email, int age, DateTime birthDate)
    {
        Id = id;
        Name = name;
        Email = email;
        Age = age;
        BirthDate = birthDate;
    }
}