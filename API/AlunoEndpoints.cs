using AutoMapper;
using SGE.Model;
using SGE.Service;
using SGE.Service.Interfaces;

namespace SGE.AlunoEndpoints;

public static class AlunoEndpoints
{
    public static void MapAlunoEndpoints(this WebApplication app)
    {
        var alunos = app.MapGroup("/api/alunos").WithTags("Alunos");
        alunos.MapPost("/", async (CreateAlunoDto dto, IAlunoService s) => await s.CreateAsync(dto));
        alunos.MapGet("/", async (IAlunoService s) => await s.GetAllAsync());
        alunos.MapPost("/avaliacoes", async (CreateAvaliacaoDto dto, IAlunoService s) => await s.AddAvaliacaoAsync(dto));
        alunos.MapGet("/relatorios", async (IAlunoService s) => await s.GerarRelatoriosAsync());
        alunos.MapPost("/{alunoId:guid}/matricular/{cursoId:guid}", async (Guid alunoId, Guid cursoId, IAlunoService s) => 
        {
            try
            {
                return Results.Ok(await s.MatricularAsync(alunoId, cursoId));
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
        });
    }
}
