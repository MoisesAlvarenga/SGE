using Microsoft.EntityFrameworkCore;
using SGE.Entity;

namespace SGE.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Aluno> Alunos => Set<Aluno>();
    public DbSet<Professor> Professores => Set<Professor>();
    public DbSet<Curso> Cursos => Set<Curso>();
    public DbSet<Turma> Turmas => Set<Turma>();
    public DbSet<Avaliacao> Avaliacoes => Set<Avaliacao>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // relacionamento Aluno -> Avaliacao (1:N)
        modelBuilder.Entity<Avaliacao>()
            .HasOne(a => a.Aluno)
            .WithMany(a => a.Avaliacoes)
            .HasForeignKey(a => a.AlunoId)
            .OnDelete(DeleteBehavior.Cascade);

        // AutoInclude para carregar Avaliacoes sempre que consultar Aluno
        modelBuilder.Entity<Aluno>().Navigation(a => a.Avaliacoes).AutoInclude();

        // (Opcional) auto-include do Curso também
        modelBuilder.Entity<Aluno>().Navigation(a => a.Curso).AutoInclude();

        // Configurar relacionamento Curso -> Avaliacao (1:N)
        modelBuilder.Entity<Avaliacao>()
        .HasOne(a => a.Aluno)
        .WithMany(a => a.Avaliacoes)
        .HasForeignKey(a => a.AlunoId)
        .OnDelete(DeleteBehavior.Cascade);

        // Configurar índices se necessário
        modelBuilder.Entity<Avaliacao>()
            .HasIndex(a => a.AlunoId);

        // Configuração Turma -> Aluno (1:N)
    modelBuilder.Entity<Aluno>()
        .HasOne(a => a.Turma)
        .WithMany(t => t.Alunos)
        .HasForeignKey(a => a.TurmaId)
        .OnDelete(DeleteBehavior.SetNull);

    // Configuração Turma -> Professor (N:1)
    modelBuilder.Entity<Turma>()
        .HasOne(t => t.Professor)
        .WithMany()
        .HasForeignKey(t => t.ProfessorId)
        .OnDelete(DeleteBehavior.SetNull);

    // Configuração Turma -> Curso (N:1)
    modelBuilder.Entity<Turma>()
        .HasOne(t => t.Curso)
        .WithMany()
        .HasForeignKey(t => t.CursoId)
        .OnDelete(DeleteBehavior.SetNull);


    }
}
