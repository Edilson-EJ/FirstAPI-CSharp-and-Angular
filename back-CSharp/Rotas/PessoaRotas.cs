// Importar o namespace para trabalhar com JSON
using System.Text.Json; 
using ApiAngularCsharp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiAngularCsharp.Rotas;

public static class PessoaRotas
{
    // Lista de pessoas carregadas a partir do arquivo JSON
    public static List<Pessoa> Pessoas = LoadPessoasFromJson();

    // Método para carregar dados do arquivo JSON
    private static List<Pessoa> LoadPessoasFromJson()
    {
        try
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Fixtures", "pessoas.json");
            Console.WriteLine($"Caminho do arquivo JSON: {path}"); 
            var jsonString = File.ReadAllText(path);
            var pessoas = JsonSerializer.Deserialize<List<Pessoa>>(jsonString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<Pessoa>();

            Console.WriteLine($"Dados carregados: {JsonSerializer.Serialize(pessoas)}"); 

            return pessoas;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao carregar dados do JSON: {ex.Message}");
            return new List<Pessoa>();
        }
    }



    // Mapeamento das rotas de Pessoa
    public static void MapPessoaRotas(this WebApplication app)
    {
        // Rota para obter todas as pessoas
        app.MapGet("/pessoas", () => Results.Ok(Pessoas));

        // Rota para obter uma pessoa pelo nome (busca case-insensitive e exata)
        app.MapGet("/pessoas/{name}", (string name) =>
        {
            var foundPersons = Pessoas
                .Where(p => p.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();

            return foundPersons.Any() ? Results.Ok(foundPersons) : Results.NotFound("Nenhuma pessoa encontrada.");
        });



        // Rota para adicionar uma nova pessoa
        app.MapPost("/pessoas/create", ([FromBody] Pessoa pessoa) =>
        {   
            // Gerar um novo ID
            pessoa.Id = Guid.NewGuid(); 
            // Adicionar à lista
            Pessoas.Add(pessoa); 
            return Results.Created($"/pessoas/{pessoa.Id}", pessoa); 
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
            
            // Retornar a pessoa atualizada
            return Results.Ok(found); 
        });

        // Rota para deletar uma pessoa pelo ID
        app.MapDelete("/pessoas/delete/{id}", (Guid id) =>
        {
            var found = Pessoas.Find(x => x.Id == id);

            if (found == null)
                return Results.NotFound("Pessoa não encontrada.");
            
            // Remover a pessoa da lista
            Pessoas.Remove(found); 
            
            // Retornar 204 No Content
            return Results.NoContent(); 
        });
    }
}
