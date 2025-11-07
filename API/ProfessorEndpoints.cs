using AutoMapper;
using SGE.Model;
using SGE.Service;
using SGE.Service.Interfaces;

namespace SGE.ProfessorEndpoints;

public static class ProfessorEndpoints
{
    public static void MapProfessorEndpoints(this WebApplication app)
    {
        var professores = app.MapGroup("/api/professores").WithTags("Professores");

        professores.MapPost("/", async (CreateProfessorDto dto, IProfessorService s) =>
            await s.CreateAsync(dto));

        professores.MapGet("/", async (IProfessorService s) =>
            await s.GetAllAsync());
    }
}
