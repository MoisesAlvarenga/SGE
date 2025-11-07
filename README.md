# SGE - Sistema de Gestão Escolar

## Sobre
Sistema de gestão escolar com funcionalidades para gerenciar alunos, professores, cursos e avaliações.

## Requisitos
- .NET 9.0 SDK
- Visual Studio Code ou Visual Studio 2022
- SQLite

## Configuração Inicial

1. Instale as ferramentas do Entity Framework Core:
```powershell
dotnet tool install --global dotnet-ef
```

2. Clone o repositório:
```powershell
git clone <repository-url>
cd SGE
```

3. Restaure os pacotes:
```powershell
dotnet restore
```

4. Crie e aplique as migrations:
```powershell
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## Executando o Projeto

1. Para executar em desenvolvimento:
```powershell
dotnet run
```

2. Acesse o Swagger UI:
- URL: https://localhost:5004/swagger
- Ou http://localhost:5004/swagger se não estiver usando HTTPS

## Estrutura do Projeto

- `/API` - Endpoints da API
- `/Data` - Contexto do banco de dados
- `/Entity` - Modelos de domínio
- `/Model` - DTOs
- `/Repository` - Camada de acesso a dados
- `/Service` - Lógica de negócios
- `/Mapping` - Configurações do AutoMapper

## Migrations

Para criar uma nova migration:
```powershell
dotnet ef migrations add NomeDaMigration
```

Para atualizar o banco:
```powershell
dotnet ef database update
```

Para remover a última migration:
```powershell
dotnet ef migrations remove
```

## Pacotes Utilizados

- AutoMapper.Extensions.Microsoft.DependencyInjection (12.0.1)
- Microsoft.EntityFrameworkCore.Design (9.0.10)
- Microsoft.EntityFrameworkCore.Sqlite (9.0.10)
- Swashbuckle.AspNetCore (9.0.6)

## Endpoints Principais

- `POST /api/alunos` - Criar novo aluno
- `GET /api/alunos` - Listar todos os alunos
- `POST /api/alunos/{alunoId}/matricular/{cursoId}` - Matricular aluno em curso
- `POST /api/alunos/avaliacoes` - Registrar avaliação

## Banco de Dados

O projeto utiliza SQLite como banco de dados. O arquivo do banco (`SGE.db`) é criado automaticamente na pasta raiz do projeto após aplicar as migrations.

## Suporte

Para reportar problemas ou sugerir melhorias, abra uma issue no repositório do projeto.