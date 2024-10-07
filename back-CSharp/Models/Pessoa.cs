namespace ApiAngularCsharp.Models;

public class Pessoa
{
    private Guid id;
    private int age;
    private string name;
    private string email;
    private DateTime birthDate;

    // Propriedade Id
    public Guid? Id
    {
        get => id;
        set => id = value ?? Guid.NewGuid(); // Se o valor for nulo, gera um novo Guid
    }

    // Propriedade Age
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

    // Propriedade Name
    public string Name
    {
        get => name;
        set => name = value;
    }

    // Propriedade Email
    public string Email
    {
        get => email;
        set => email = value;
    }

    // Propriedade BirthDate
    public DateTime BirthDate
    {
        get => birthDate;
        set => birthDate = value;
    }

    // Construtor padrão (necessário para a desserialização)
    public Pessoa() { }

    // Construtor parametrizado (com nomes que correspondem ao JSON)
    public Pessoa(Guid id, string name, string email, int age, DateTime birthDate)
    {
        Id = id;
        Name = name;
        Email = email;
        Age = age;
        BirthDate = birthDate;
    }
}