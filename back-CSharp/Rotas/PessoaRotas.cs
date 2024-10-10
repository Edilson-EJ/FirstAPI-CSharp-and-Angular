using ApiAngularCsharp.Models;
using ApiAngularCsharp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiAngularCsharp.Rotas
{
    public static class PessoaRotas
    {
        public static void MapPessoaRotas(this WebApplication app)
        {
            // Route to get all the people
            app.MapGet("/pessoas", async (AppDbContext db) =>
            {
                var pessoas = await db.Pessoas.ToListAsync();
                return Results.Ok(pessoas);
            });

            // Route to get a person by name (case-insensitive and exact search)
            app.MapGet("/pessoas/{name}", async (string name, AppDbContext db) =>
            {
                var foundPersons = await db.Pessoas
                    .Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                    .ToListAsync();

                return foundPersons.Any() ? Results.Ok(foundPersons) : Results.NotFound("Nenhuma pessoa encontrada.");
            });

            // Route to add a new person
            app.MapPost("/pessoas/create", async ([FromBody] AddPessoaRequest request, AppDbContext db) =>
            {
                var newPerson = new Pessoa
                {
                    Id = Guid.NewGuid(), 
                    Name = request.Name,
                    Email = request.Email,
                    Age = request.Age,
                    BirthDate = request.BirthDate
                };

                db.Pessoas.Add(newPerson);
                // Saves changes to the database
                await db.SaveChangesAsync(); 

                return Results.Created($"/pessoas/{newPerson.Id}", newPerson);
            });

            // Route to update a person by ID
            app.MapPut("/pessoas/update/{id}", async (Guid id, [FromBody] AddPessoaRequest request, AppDbContext db) =>
            {
                var found = await db.Pessoas.FindAsync(id);

                if (found == null)
                    return Results.NotFound("Pessoa não encontrada.");

                // Update the person's properties
                found.Name = request.Name;
                found.Email = request.Email;
                found.Age = request.Age;
                found.BirthDate = request.BirthDate;
                
                // Saves changes to the database
                await db.SaveChangesAsync(); 
                return Results.Ok(found);
            });

            // Route to delete a person by ID
            app.MapDelete("/pessoas/delete/{id}", async (Guid id, AppDbContext db) =>
            {
                var found = await db.Pessoas.FindAsync(id);

                if (found == null)
                    return Results.NotFound("Pessoa não encontrada.");

                db.Pessoas.Remove(found);
                // Saves changes to the database
                await db.SaveChangesAsync(); 

                return Results.NoContent();
            });
        }
    }
}
