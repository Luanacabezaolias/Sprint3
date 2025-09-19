using Challenge_sprint.src.Application.Services;
using Challenge_sprint.src.Domain.Entities;
using Challenge_sprint.src.Infrastructure.Data;
using Challenge_sprint.src.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

class Program
{
    static async Task Main(string[] args)
    {
        // Configuração do Host e DI
        var builder = Host.CreateDefaultBuilder(args)
            .ConfigureServices((ctx, services) =>
            {
                services.AddDbContext<AppDbContext>(o => o.UseSqlite("Data Source=app.db"));
                services.AddScoped<IUnitOfWork, UnitOfWork>();
                services.AddScoped<AssessmentService>();
                services.AddScoped<JournalService>();
            });

        using var host = builder.Build();
        using var scope = host.Services.CreateScope();
        var sp = scope.ServiceProvider;

        var db = sp.GetRequiredService<AppDbContext>();
        db.Database.EnsureCreated();

        var uow = sp.GetRequiredService<IUnitOfWork>();

        while (true)
        {
            Console.WriteLine("\n=== Menu Principal ===");
            Console.WriteLine("1. Criar usuário");
            Console.WriteLine("2. Registrar autoavaliação");
            Console.WriteLine("3. Listar usuários");
            Console.WriteLine("4. Exportar usuários para JSON");
            Console.WriteLine("0. Sair");
            Console.Write("Escolha: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await CriarUsuario(uow);
                    break;
                case "2":
                    await CriarAutoAvaliacao(uow);
                    break;
                case "3":
                    ListarUsuarios(uow);
                    break;
                case "4":
                    ExportarUsuarios(uow);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        }
    }

    static async Task CriarUsuario(IUnitOfWork uow)
    {
        Console.Write("Nome: ");
        var name = Console.ReadLine() ?? "";

        Console.Write("Email: ");
        var email = Console.ReadLine() ?? "";

        var user = new User { Name = name, Email = email };
        await uow.Users.AddAsync(user);
        await uow.SaveChangesAsync();

        Console.WriteLine($"\nUsuário criado! ID: {user.Id}, Nome: {user.Name}, Email: {user.Email}");
    }

    static async Task CriarAutoAvaliacao(IUnitOfWork uow)
    {
        Console.Write("ID do usuário: ");
        if (!int.TryParse(Console.ReadLine(), out int userId))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        var user = uow.Users.Query().FirstOrDefault(u => u.Id == userId);
        if (user == null)
        {
            Console.WriteLine("Usuário não encontrado.");
            return;
        }

        Console.Write("Pontuação da autoavaliação (0-27): ");
        if (!int.TryParse(Console.ReadLine(), out int score) || score < 0 || score > 27)
        {
            Console.WriteLine("Pontuação inválida.");
            return;
        }

        var riskLevel = score <= 7 ? "Baixo" : score <= 15 ? "Médio" : "Alto";

        var assessment = new SelfAssessment
        {
            UserId = user.Id,
            Score = score,
            RiskLevel = riskLevel,
            Notes = "Avaliação inicial"
        };

        await uow.Assessments.AddAsync(assessment);
        await uow.SaveChangesAsync();

        Console.WriteLine($"Autoavaliação criada! Usuário: {user.Name}, Score: {score}, Risco: {riskLevel}");
    }

    static void ListarUsuarios(IUnitOfWork uow)
    {
        var users = uow.Users.Query().ToList();
        Console.WriteLine("\n=== Usuários ===");
        foreach (var u in users)
        {
            Console.WriteLine($"ID: {u.Id}, Nome: {u.Name}, Email: {u.Email}");
        }
    }

    static void ExportarUsuarios(IUnitOfWork uow)
    {
        var users = uow.Users.Query().ToList();

        // Cria um DTO para evitar referência circular
        var usersDto = users.Select(u => new
        {
            u.Id,
            u.Name,
            u.Email,
            CreatedAt = u.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"),
            Assessments = u.Assessments.Select(a => new
            {
                a.Id,
                a.Score,
                a.RiskLevel,
                a.Notes,
                Date = a.Date.ToString("yyyy-MM-dd HH:mm:ss")
            }).ToList()
        }).ToList();

        var json = System.Text.Json.JsonSerializer.Serialize(usersDto, new System.Text.Json.JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText("users.json", json);
        Console.WriteLine("Usuários exportados para users.json com sucesso!");
    }

}
