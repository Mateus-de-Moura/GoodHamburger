# GoodHamburger - Backend

Este backend suporta 2 provedores de banco:

- `SqlServer`
- `InMemory`

## Configuracao atual

O arquivo `src/Backend/GoodHamburger.Api/appsettings.Development.json` ja vem com:

- `Database:Provider = "SqlServer"`
- instancia SQL Server: `DESKTOP-3UTOJPR\\SQLEXPRESS`

Trecho:

```json
"Database": {
  "Provider": "SqlServer",
  "InMemoryDatabaseName": "GoodHamburgerDb"
},
"ConnectionStrings": {
  "DefaultConnection": "Server=DESKTOP-3UTOJPR\\SQLEXPRESS;Database=GoodHamburgerDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

## Como alternar entre SQL Server e InMemory

### Opcao 1: alterando appsettings

No arquivo `src/Backend/GoodHamburger.Api/appsettings.Development.json`:

1. Para usar SQL Server:

```json
"Database": {
  "Provider": "SqlServer"
}
```

2. Para usar InMemory:

```json
"Database": {
  "Provider": "InMemory",
  "InMemoryDatabaseName": "GoodHamburgerDb"
}
```

### Opcao 2: sobrescrevendo por variavel de ambiente (PowerShell)

Sem editar arquivo, para a sessao atual:

1. SQL Server

```powershell
$env:Database__Provider = "SqlServer"
```

2. InMemory

```powershell
$env:Database__Provider = "InMemory"
```

Se quiser trocar a connection string via ambiente:

```powershell
$env:ConnectionStrings__DefaultConnection = "Server=DESKTOP-3UTOJPR\\SQLEXPRESS;Database=GoodHamburgerDb;Trusted_Connection=True;TrustServerCertificate=True;"
```

## Rodar a API

```powershell
dotnet run --project src/Backend/GoodHamburger.Api/GoodHamburger.Api.csproj
```

Swagger (Development):

- `http://localhost:5092/swagger`
- `https://localhost:7245/swagger`

## Criar migrations (quando necessario)

Se ainda nao houver migrations, crie a inicial:

```powershell
dotnet ef migrations add InitialCreate --project src/Backend/GoodHamburger.Infrastructure/GoodHamburger.Infrastructure.csproj --startup-project src/Backend/GoodHamburger.Api/GoodHamburger.Api.csproj --output-dir Migrations
```

No Package Manager Console (Visual Studio), use com projeto e startup explicitos:

```powershell
Add-Migration InitialCreate -Project GoodHamburger.Infrastructure -StartupProject GoodHamburger.Api -OutputDir Migrations
```

Para aplicar no banco:

```powershell
Update-Database -Project GoodHamburger.Infrastructure -StartupProject GoodHamburger.Api
```

CLI equivalente:

```powershell
dotnet ef database update --project src/Backend/GoodHamburger.Infrastructure/GoodHamburger.Infrastructure.csproj --startup-project src/Backend/GoodHamburger.Api/GoodHamburger.Api.csproj
```

Depois, ao iniciar a API com `Database:Provider = "SqlServer"`, o `Migrate` sera executado automaticamente.

## Observacoes

- Com `SqlServer`, o backend usa a `ConnectionStrings:DefaultConnection`.
- Com `SqlServer`, no startup a API tenta criar o banco (se nao existir) e depois executa `Migrate` automaticamente.
- Com `InMemory`, os dados sao volateis e sao perdidos ao reiniciar a API.
- Com `InMemory`, o startup chama `EnsureCreated`.
- Para o `Migrate` aplicar mudancas de schema, e necessario ter migrations criadas no projeto.
- Nao existe fallback de connection string em codigo: se `DefaultConnection` nao estiver no appsettings/ambiente, a inicializacao de SQL Server e o design-time do EF vao falhar.
