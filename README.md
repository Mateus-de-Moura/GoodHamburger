# GoodHamburger

Projeto com API em .NET 9 + WebApp Blazor Server para gestao de cardapio, produtos e pedidos.

## Decisoes de arquitetura

- API em estilo minimalista com `FastEndpoints` para rotas mais organizadas e baixo overhead para o cenario de sistema pequeno.
- Arquitetura em camadas seguindo `Clean Architecture`:
  - `Api`
  - `Application`
  - `Domain`
  - `Infrastructure`
- Desacoplamento entre API e regras de aplicacao com `MediatR` + padrao `CQRS`.
- Validacoes com `FluentValidation`.
- Padrao de retorno com `Ardalis.Result`.
- Persistencia com EF Core suportando `SqlServer` e `InMemory`.

## Observacao sobre Network no navegador (Blazor Server)

O frontend roda com `@rendermode InteractiveServer` (Blazor Server).
Por isso, no DevTools do browser voce normalmente nao vera uma request HTTP para cada clique/acao.

Em vez disso, o browser mantem uma conexao SignalR (`/_blazor`) e os eventos trafegam nesse canal.
As chamadas para a API podem acontecer no servidor, entao tambem nao aparecem como request individual no Network do browser.

## O que ficou fora (deliberadamente)

- Autenticacao e autorizacao com JWT.
  - Para um sistema real, esse seria um ponto inicial importante (cadastro/login/controle de acesso).
- Armazenamento de imagens em Blob Storage.
  - Neste projeto, as imagens dos produtos foram mantidas em banco para simplificar os testes.

## Como executar

### Pre-requisitos

- .NET SDK 9
- SQL Server (apenas se quiser rodar com `SqlServer`)

### 1) Escolher o provider do banco

Voce pode usar `InMemory` (mais rapido para teste) ou `SqlServer`.

Opcao via variavel de ambiente (PowerShell):

```powershell
# InMemory
$env:Database__Provider = "InMemory"

# SqlServer
$env:Database__Provider = "SqlServer"

# Connection string (quando SqlServer)
$env:ConnectionStrings__DefaultConnection = "Server=DESKTOP-3UTOJPR\SQLEXPRESS;Database=GoodHamburgerDb;Trusted_Connection=True;TrustServerCertificate=True;"
```

Tambem e possivel configurar direto no arquivo:
`src/Backend/GoodHamburger.Api/appsettings.Development.json`

### 2) Rodar a API

```powershell
dotnet run --project src/Backend/GoodHamburger.Api/GoodHamburger.Api.csproj
```

Swagger:
- `http://localhost:5092/swagger`
- `https://localhost:7245/swagger`

### 3) Rodar o WebApp

```powershell
dotnet run --project src/WebApp/GoodHamburger/GoodHamburger.csproj
```

Web:
- `http://localhost:5044`
- `https://localhost:7112`

## Migrations (somente SQL Server)

Se precisar criar/aplicar migrations manualmente:

```powershell
dotnet ef migrations add InitialCreate --project src/Backend/GoodHamburger.Infrastructure/GoodHamburger.Infrastructure.csproj --startup-project src/Backend/GoodHamburger.Api/GoodHamburger.Api.csproj --output-dir Migrations
dotnet ef database update --project src/Backend/GoodHamburger.Infrastructure/GoodHamburger.Infrastructure.csproj --startup-project src/Backend/GoodHamburger.Api/GoodHamburger.Api.csproj
```

No startup com `SqlServer`, a API tenta criar o banco se necessario e executa `Migrate`.
Com `InMemory`, os dados sao temporarios e sao perdidos ao reiniciar.

## Resumo rapido para testar

1. Suba a API com `InMemory` ou `SqlServer`.
2. Suba o WebApp.
3. Acesse `https://localhost:7112`.
4. Cadastre/edite produtos (com imagem), veja o cardapio e finalize pedidos.
5. Se quiser validar endpoints isoladamente, use o Swagger da API.
