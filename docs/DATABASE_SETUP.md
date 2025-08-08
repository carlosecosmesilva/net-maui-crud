# Configuração do Banco de Dados

## ⚡ Configuração Automática (Recomendado)

O projeto está configurado para **aplicar migrações automaticamente** na inicialização. Você só precisa:

1. **Instalar o PostgreSQL**
2. **Criar o banco de dados**:

```sql
-- Conecte-se ao PostgreSQL como superuser (postgres)
CREATE DATABASE maui_customer_app;
```

3. **Ajustar a string de conexão** no arquivo `MauiProgram.cs`
4. **Executar o projeto** - As tabelas e dados de exemplo serão criados automaticamente!

## 🔧 Migrações do Entity Framework

O projeto utiliza **Entity Framework Core Migrations** para gerenciar o esquema do banco de dados.

### Migrações Incluídas:

-   ✅ **InitialCreate**: Cria a tabela `Customers`
-   ✅ **SeedInitialData**: Insere 5 clientes de exemplo

### Comandos de Migração:

```bash
# Instalar ferramentas do EF Core (já configurado no projeto)
dotnet tool install --global dotnet-ef

# Navegar para o diretório do projeto
cd MauiCustomerApp/MauiCustomerApp

# Listar migrações
dotnet ef migrations list

# Criar nova migração
dotnet ef migrations add NomeDaMigracao

# Aplicar migrações manualmente (opcional - o projeto faz automaticamente)
dotnet ef database update

# Remover última migração (se necessário)
dotnet ef migrations remove
```

## 📝 Configuração da String de Conexão

No arquivo `MauiProgram.cs`, ajuste a string de conexão conforme sua configuração:

```csharp
// Configuração atual no projeto
var connectionString = "Host=localhost;Port=5432;Database=maui_customer_app;Username=postgres;Password=postgres";

// Exemplos de outras configurações:

// Servidor remoto
var connectionString = "Host=seu-servidor.com;Port=5432;Database=maui_customer_app;Username=seu-usuario;Password=sua-senha";

// Com SSL
var connectionString = "Host=localhost;Port=5432;Database=maui_customer_app;Username=postgres;Password=postgres;SSL Mode=Require";

// Pool de conexões customizado
var connectionString = "Host=localhost;Port=5432;Database=maui_customer_app;Username=postgres;Password=postgres;Pooling=true;MinPoolSize=1;MaxPoolSize=20";
```

## 📊 Dados de Exemplo

O projeto inclui **5 clientes de exemplo** criados automaticamente:

| ID  | Nome   | Sobrenome | Idade | Endereço             |
| --- | ------ | --------- | ----- | -------------------- |
| 1   | João   | Silva     | 30    | Rua das Flores, 123  |
| 2   | Maria  | Santos    | 25    | Av. Brasil, 456      |
| 3   | Pedro  | Oliveira  | 35    | Rua da Paz, 789      |
| 4   | Ana    | Costa     | 28    | Av. Central, 321     |
| 5   | Carlos | Ferreira  | 42    | Rua do Comércio, 654 |

## 🛠️ Troubleshooting

### Erro de Conexão

-   Verifique se o PostgreSQL está rodando
-   Confirme as credenciais na string de conexão
-   Teste a conexão com um cliente PostgreSQL (pgAdmin, DBeaver, etc.)

### Erro de Dependências

-   Execute `dotnet restore` na pasta do projeto
-   Verifique se tem o .NET 9 SDK instalado

### Erro de Build

-   Limpe o projeto: `dotnet clean`
-   Rebuild: `dotnet build`
