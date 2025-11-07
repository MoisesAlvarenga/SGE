using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SGE.AlunoEndpoints;
using SGE.CursoEndpoints;
using SGE.Data;
using SGE.Mapping;
using SGE.ProfessorEndpoints;
using SGE.Repository;
using SGE.Service;
using SGE.Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Configura banco de dados (SQLite)
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositórios e serviços
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IAlunoService, AlunoService>();
builder.Services.AddScoped<IProfessorService, ProfessorService>();
builder.Services.AddScoped<ICursoService, CursoService>();
builder.Services.AddScoped<IAvaliacaoService, AvaliacaoService>();
builder.Services.AddScoped<ITurmaService, TurmaService>();


// Configuração do AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// CORS e Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(opt =>
    opt.AddDefaultPolicy(p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod())
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SGE - Sistema de Gestão Educacional",
        Version = "v1",
        Description = "API para gerenciamento de alunos, professores, cursos, turmas e avaliações.",
        Contact = new OpenApiContact
        {
            Name = "EduConnect",
            Email = "contato@educonnect.com.br"
        }
    });
});

var app = builder.Build();

app.UseStaticFiles();
app.UseCors();

// aplicar migrations automaticamente (cria/atualiza o arquivo SGE.db)
using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    ctx.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        opt.SwaggerEndpoint("/swagger/v1/swagger.json", "SGE API v1");
        opt.RoutePrefix = "swagger"; // Abre direto na raiz "/"
    });
}

app.MapAlunoEndpoints();
app.MapProfessorEndpoints();
app.MapCursoEndpoints();

app.Run();
