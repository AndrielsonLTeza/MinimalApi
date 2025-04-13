using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao contêiner
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar serviços e repositórios
builder.Services.AddSingleton<ICursoRepositorio, CursoRepositorio>();
builder.Services.AddSingleton<IAlunoRepositorio, AlunoRepositorio>();
builder.Services.AddScoped<ICursoServico, CursoServico>();
builder.Services.AddScoped<IAlunoServico, AlunoServico>();

var app = builder.Build();

// Configure o pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Definição de endpoints para Cursos
app.MapGet("/cursos", async (ICursoServico cursoServico) =>
{
    return Results.Ok(await cursoServico.ListarTodosAsync());
});

app.MapGet("/cursos/{id}", async (int id, ICursoServico cursoServico) =>
{
    var curso = await cursoServico.ObterPorIdAsync(id);
    if (curso == null)
        return Results.NotFound();
    
    return Results.Ok(curso);
});

app.MapPost("/cursos", async (Curso curso, ICursoServico cursoServico) =>
{
    var novoCurso = await cursoServico.AdicionarAsync(curso);
    return Results.Created($"/cursos/{novoCurso.Id}", novoCurso);
});

app.MapPut("/cursos/{id}", async (int id, Curso curso, ICursoServico cursoServico) =>
{
    if (id != curso.Id)
        return Results.BadRequest();
    
    var atualizado = await cursoServico.AtualizarAsync(curso);
    if (!atualizado)
        return Results.NotFound();
    
    return Results.NoContent();
});

app.MapDelete("/cursos/{id}", async (int id, ICursoServico cursoServico) =>
{
    var excluido = await cursoServico.ExcluirAsync(id);
    if (!excluido)
        return Results.NotFound();
    
    return Results.NoContent();
});

// Definição de endpoints para Alunos
app.MapGet("/alunos", async (IAlunoServico alunoServico) =>
{
    return Results.Ok(await alunoServico.ListarTodosAsync());
});

app.MapGet("/alunos/{id}", async (int id, IAlunoServico alunoServico) =>
{
    var aluno = await alunoServico.ObterPorIdAsync(id);
    if (aluno == null)
        return Results.NotFound();
    
    return Results.Ok(aluno);
});

app.MapPost("/alunos", async (Aluno aluno, IAlunoServico alunoServico, ICursoServico cursoServico) =>
{
    // Validar se o curso existe
    var curso = await cursoServico.ObterPorIdAsync(aluno.CursoId);
    if (curso == null)
        return Results.BadRequest("Curso não encontrado");
    
    var novoAluno = await alunoServico.AdicionarAsync(aluno);
    return Results.Created($"/alunos/{novoAluno.Id}", novoAluno);
});

app.MapPut("/alunos/{id}", async (int id, Aluno aluno, IAlunoServico alunoServico) =>
{
    if (id != aluno.Id)
        return Results.BadRequest();
    
    var atualizado = await alunoServico.AtualizarAsync(aluno);
    if (!atualizado)
        return Results.NotFound();
    
    return Results.NoContent();
});

app.MapDelete("/alunos/{id}", async (int id, IAlunoServico alunoServico) =>
{
    var excluido = await alunoServico.ExcluirAsync(id);
    if (!excluido)
        return Results.NotFound();
    
    return Results.NoContent();
});

// Endpoint para matricular aluno em um curso
app.MapPost("/cursos/{cursoId}/matricular", async (int cursoId, MatriculaRequest request, IAlunoServico alunoServico, ICursoServico cursoServico) =>
{
    var curso = await cursoServico.ObterPorIdAsync(cursoId);
    if (curso == null)
        return Results.NotFound("Curso não encontrado");
    
    var aluno = await alunoServico.ObterPorIdAsync(request.AlunoId);
    if (aluno == null)
        return Results.NotFound("Aluno não encontrado");
    
    aluno.CursoId = cursoId;
    await alunoServico.AtualizarAsync(aluno);
    
    return Results.Ok($"Aluno {aluno.Nome} matriculado com sucesso no curso {curso.Nome}");
});

app.Run();