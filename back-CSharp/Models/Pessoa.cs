namespace ApiAngularCsharp.Models;

public class Pessoa
{
    private Guid id;
    private int age; 
    private string name; 
    private string email;
    private DateTime birthDate; 

    public Guid? Id
    {
        get => id;
        set => id = (Guid)value;
    }

    public int Age 
    { 
        get => age; 
        set
        {
            if (value < 0)
                throw new ArgumentException("Age cannot be negative.");
            age = value;
        }
    }

    public string Name 
    {
        get => name;
        set => name = value;
    }

    public string Email
    {
        get => email;
        set => email = value;
    }

    
    public DateTime BirthDate 
    {
        get => birthDate;
        set => birthDate = value;
    }

    // Updated constructor parameter names
    public Pessoa(Guid id, string name, string email, int age, DateTime birthDate) 
    {
        Id = id;
        Name = name;
        Email = email;
        Age = age;
        BirthDate = birthDate;
    }
}