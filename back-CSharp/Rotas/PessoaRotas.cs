using ApiAngularCsharp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiAngularCsharp.Rotas;

public static class PessoaRotas
{
    public static List<Pessoa> Pessoas = new()
    {
        new Pessoa(id:Guid.NewGuid(), nome:"Neymar"),
        new Pessoa(id:Guid.NewGuid(), nome:"Edilson"),
        new Pessoa(id:Guid.NewGuid(), nome:"CR7"),
    };

    public static void MapPessoaRotas(this WebApplication app)
    {
        // Rota para obter todas as pessoas
        app.MapGet("/pessoas", handler: () => Pessoas);

        // Rota para obter uma pessoa pelo nome
        app.MapGet("/pessoas/{nome}", 
            handler: (string nome) => Pessoas.Find(p => p.Nome.StartsWith((nome))));

        // Rota para adicionar uma nova pessoa
        app.MapPost("/pessoas/create", (HttpContext context, [FromBody] Pessoa pessoa) =>
        {
            pessoa.Id = Guid.NewGuid();
            Pessoas.Add(pessoa);
            return Results.Ok(pessoa);
        });


        // Rota para atualizar uma pessoa pelo ID
        app.MapPut(pattern: "/pessoa/{id}", handler: (Guid id, Pessoa pessoa) =>
        {
            // Corrigindo a declaração da variável 'encontrado'
            var encontrado = Pessoas.Find(x => x.Id == id);

            if (encontrado == null)
                return Results.NotFound();

            // Atualiza as propriedades da pessoa encontrada
            encontrado.Nome = pessoa.Nome;

            return Results.Ok(encontrado);
        });
    }
}