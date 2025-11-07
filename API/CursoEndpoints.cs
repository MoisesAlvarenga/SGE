using Microsoft.AspNetCore.Mvc;
using SGE.Model;
using SGE.Service.Interfaces;

namespace SGE.CursoEndpoints;

public static class CursoEndpoints
{
    public static void MapCursoEndpoints(this WebApplication app)
    {
        var cursos = app.MapGroup("/api/cursos").WithTags("Cursos");

        cursos.MapPost("/", async (CreateCursoDto dto, ICursoService s) => 
            await s.CreateAsync(dto));

        cursos.MapGet("/", async (ICursoService s) => 
            await s.GetAllAsync());

        // Corrigido: adicionado CreateTurmaDto no POST turmas
        cursos.MapPost("/{cursoId:guid}/turmas", async (Guid cursoId, CreateTurmaDto dto, ICursoService s) => 
            await s.CreateTurmaAsync(cursoId, dto));

        // Corrigido: adicionado parâmetro cursoId no GET turmas
        cursos.MapGet("/{cursoId:guid}/turmas", async (Guid cursoId, ICursoService s) => 
            await s.GetTurmasAsync(cursoId));

        // Endpoint para adicionar aluno na turma
        cursos.MapPost("/{cursoId:guid}/turmas/{turmaId:guid}/alunos/{alunoId:guid}", 
            async (Guid cursoId, Guid turmaId, Guid alunoId, ICursoService s) => 
                await s.AdicionarAlunoTurmaAsync(cursoId, turmaId, alunoId));

        cursos.MapGet("/relatorios", async (ICursoService s) => 
            await s.GerarRelatoriosAsync());
    }
}
