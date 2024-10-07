using ApiAngularCsharp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiAngularCsharp.Rotas;

public static class PessoaRotas
{
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

    public static void MapPessoaRotas(this WebApplication app)
    {
        // Route to get all people
        app.MapGet("/pessoas", handler: () => Results.Ok(Pessoas));

        // Route to get a person by name
        app.MapGet("/pessoas/{name}", 
            handler: (string name) => 
            {
                var foundPerson = Pessoas.Find(p => p.Name.StartsWith(name));
                return foundPerson != null ? Results.Ok(foundPerson) : Results.NotFound();
            });

        // Route to add a new person
        app.MapPost("/pessoas/create", (HttpContext context, [FromBody] Pessoa pessoa) =>
        {
            pessoa.Id = Guid.NewGuid();
            Pessoas.Add(pessoa);
            return Results.Ok(pessoa);
        });

        // Route to update a person by ID
        app.MapPut("/pessoas/update/{id}", (Guid id, [FromBody] Pessoa pessoa) =>
        {
            var found = Pessoas.Find(x => x.Id == id);

            if (found == null)
                return Results.NotFound();

            // Update properties of the found person
            found.Name = pessoa.Name;
            found.Email = pessoa.Email;
            found.Age = pessoa.Age;
            found.BirthDate = pessoa.BirthDate;

            return Results.Ok(found);
        });

        // Route to delete a person by ID
        app.MapDelete("/pessoas/delete/{id}", (Guid id) =>
        {
            var found = Pessoas.Find(x => x.Id == id);

            if (found == null)
                return Results.NotFound();

            Pessoas.Remove(found);
            return Results.NoContent();
        });
    }
}