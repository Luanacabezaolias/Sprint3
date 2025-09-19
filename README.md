# Sprint 3 — Challenge

# Plataforma C# — Prevenção e Recuperação de Apostas Compulsivas

# Integrantes

Juliana Villalpando Maita — 99224

João Victor Dos Santos Morais — 550453

Luana Cabezaolias Miguel — 99320

Lucca Vilaça Okubo — 551538

Pedro Henrique Pontes Farath — 98608

Projeto didático para atender aos critérios do professor: classes limpas, CRUD completo com SQLite/EF Core, interface Console, manipulação de arquivos JSON/TXT, documentação e diagramas em Mermaid.

Como rodar

Requisitos: .NET 8 SDK

# na raiz do projeto
dotnet restore
dotnet build

# executar o console
dotnet run --project src/Presentation.Console


O programa cria o banco app.db automaticamente (EF Core, EnsureCreated).

Funcionalidades do Console

Criar usuário

Registrar autoavaliação (calcula nível de risco) ✔

Registrar diário (gera alerta se gasto > 100) ✔

Listar alertas

Exportar usuários para users.json

Estrutura

Domain — Entidades e regras básicas

Infrastructure — EF Core (SQLite), Repositórios, Unit of Work, File I/O (JsonExporter, Logger)

Application — Services (regras e orquestração)

Presentation.Console — Interface de linha de comando

Diagrama ER (Mermaid)

Veja docs/diagrams/erd.mmd e docs/diagrams/architecture.mmd.

Próximos passos (opcional/Formas de ganhar nota extra)

Adicionar WinForms ou Web (Minimal API) usando os Services.

Criar testes unitários para Services.

Completar CRUDs de todas as entidades nos menus.




# Diagrama

Challenge_sprint/
│
├─ src/
│  ├─ Domain/           # Entidades
│  ├─ Infrastructure/   # DbContext, UnitOfWork, Repositories
│  ├─ Application/      # Services
│  ├─ Presentation.Console/  # Program.cs
│
├─ docs/
│  ├─ diagrams/
│  │   ├─ erd.mmd
│  │   └─ architecture.mmd
│
├─ users.json           # exportado pelo programa
├─ README.md
└─ Challenge_sprint.sln

