using ApiAngularCsharp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiAngularCsharp.Rotas;

public static class PessoaRotas
{
    // Lista estática de pessoas para simular um banco de dados
    public static List<Pessoa> Pessoas = new()
    {
        new Pessoa(
            id: Guid.NewGuid(),
            name: "Neymar", 
            email: "neymar@example.com",
            age: 31,
            birthDate: DateTime.Parse("05/02/1992")
        ),
        new Pessoa(
            id: Guid.NewGuid(),
            name: "Edilson", 
            email: "edilson@example.com",
            age: 28,
            birthDate: DateTime.Parse("10/03/1996")
        ),
        new Pessoa(
            id: Guid.NewGuid(),
            name: "CR7", 
            email: "cr7@example.com",
            age: 39,
            birthDate: DateTime.Parse("05/02/1985")
        )
    };

    // Mapeamento das rotas de Pessoa
    public static void MapPessoaRotas(this WebApplication app)
    {
        // Rota para obter todas as pessoas
        app.MapGet("/pessoas", () => Results.Ok(Pessoas));

        // Rota para obter uma pessoa pelo nome (busca case-insensitive)
        app.MapGet("/pessoas/{name}", (string name) =>
        {
            var foundPerson = Pessoas.Find(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            return foundPerson != null ? Results.Ok(foundPerson) : Results.NotFound("Pessoa não encontrada.");
        });

        // Rota para adicionar uma nova pessoa
        app.MapPost("/pessoas/create", ([FromBody] Pessoa pessoa) =>
        {
            pessoa.Id = Guid.NewGuid(); // Gerar um novo ID
            Pessoas.Add(pessoa); // Adicionar à lista
            return Results.Created($"/pessoas/{pessoa.Id}", pessoa); // Retornar o recurso criado
        });

        // Rota para atualizar uma pessoa pelo ID
        app.MapPut("/pessoas/update/{id}", (Guid id, [FromBody] Pessoa pessoa) =>
        {
            var found = Pessoas.Find(x => x.Id == id);

            if (found == null)
                return Results.NotFound("Pessoa não encontrada.");

            // Atualizar as propriedades da pessoa encontrada
            found.Name = pessoa.Name;
            found.Email = pessoa.Email;
            found.Age = pessoa.Age;
            found.BirthDate = pessoa.BirthDate;

            return Results.Ok(found); // Retornar a pessoa atualizada
        });

        // Rota para deletar uma pessoa pelo ID
        app.MapDelete("/pessoas/delete/{id}", (Guid id) =>
        {
            var found = Pessoas.Find(x => x.Id == id);

            if (found == null)
                return Results.NotFound("Pessoa não encontrada.");

            Pessoas.Remove(found); // Remover a pessoa da lista
            return Results.NoContent(); // Retornar 204 No Content
        });
    }
}
