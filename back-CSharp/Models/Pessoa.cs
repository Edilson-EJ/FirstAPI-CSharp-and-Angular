namespace ApiAngularCsharp.Models;

public class Pessoa
{
    private Guid id;
    private int idade;
    private string nome;
    private string email;
    private DateTime dataNascimento;
    public Guid? Id
    {
        get => id;
        set => id = (Guid)value;
    }
    
    public int Idade 
    { 
        get => idade; 
        set
        {
            if (value < 0)
                throw new ArgumentException("Age cannot be negative.");
            idade = value;
        }
    }

    public string Nome
    {
        get => nome;
        set => nome = value;
    }

    public string Email
    {
        get => email;
        set => email = value;
    }

    public DateTime DataNascimento
    {
        get => dataNascimento;
        set => dataNascimento = value;
    }

    public Pessoa(Guid id, string nome, string email, int idade, DateTime dataNascimento)
    {
        Id = id;
        Nome = nome;
        Email = email;
        Idade = idade;
        DataNascimento = dataNascimento;
    }
}

