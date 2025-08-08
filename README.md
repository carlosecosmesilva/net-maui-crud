# 📱 MauiCustomerApp - CRUD de Clientes

Um aplicativo .NET MAUI para gerenciamento de clientes utilizando o padrão MVVM e Entity Framework Core com PostgreSQL.

## 🎯 Funcionalidades

-   ✅ Listar todos os clientes
-   ✅ Adicionar novos clientes
-   ✅ Editar clientes existentes
-   ✅ Excluir clientes com confirmação
-   ✅ Interface responsiva e moderna

## 🏗️ Arquitetura

O projeto segue os princípios de Clean Architecture e utiliza:

### Padrão MVVM (Model-View-ViewModel)

-   **Models**: Entidades de dados (`Customer`)
-   **Views**: Interfaces de usuário (XAML)
-   **ViewModels**: Lógica de apresentação e binding de dados
-   **Services**: Camada de acesso a dados

### Injeção de Dependência

O projeto utiliza o container de DI nativo do .NET MAUI configurado em `MauiProgram.cs`:

```csharp
// Registra Services
builder.Services.AddScoped<CustomerService>();

// Registra ViewModels
builder.Services.AddSingleton<MainViewModel>();

// Registra Views (páginas)
builder.Services.AddSingleton<MainPage>();
builder.Services.AddTransient<AddCustomerPage>();
builder.Services.AddTransient<EditCustomerPage>();
```

## 📋 Modelo de Dados

### Classe Customer

```csharp
public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }        // Nome
    public string Lastname { get; set; }    // Sobrenome
    public int Age { get; set; }            // Idade
    public string Address { get; set; }     // Endereço
}
```

## 🗂️ Estrutura do Projeto

```
MauiCustomerApp/
├── Data/
│   └── AppDbContext.cs                 # Contexto do Entity Framework
├── Models/
│   └── Customer.cs                     # Modelo de dados do Cliente
├── Services/
│   └── CustomerService.cs              # Serviço de acesso a dados
├── ViewModels/
│   └── MainViewModel.cs                # ViewModel principal
├── Views/
│   ├── MainPage.xaml                   # Página principal (lista)
│   ├── AddCustomerPage.xaml            # Página de adição
│   └── EditCustomerPage.xaml           # Página de edição
└── MauiProgram.cs                      # Configuração de DI e startup
```

## 🛠️ Tecnologias Utilizadas

-   **.NET 9.0**
-   **.NET MAUI** - Framework multiplataforma
-   **Entity Framework Core** - ORM para acesso a dados
-   **PostgreSQL** - Banco de dados
-   **Npgsql** - Provider PostgreSQL para .NET
-   **MVVM Pattern** - Padrão de arquitetura
-   **Dependency Injection** - Container nativo do .NET

## ⚙️ Configuração e Instalação

### Pré-requisitos

-   Visual Studio 2022 (17.8 ou superior) com workload .NET MAUI
-   .NET 9.0 SDK
-   PostgreSQL Server

### Configuração do Banco de Dados

1. **Instale o PostgreSQL** em sua máquina local
2. **Configure a string de conexão** em `MauiProgram.cs`:

```csharp
var connectionString = "Host=localhost;Port=5432;Database=btg_customer;Username=postgres;Password=123456";
```

3. **Ajuste as credenciais** conforme sua configuração local

> 📖 **Documentação completa**: Para instruções detalhadas sobre configuração do banco de dados, scripts SQL e troubleshooting, consulte [`docs/DATABASE_SETUP.md`](docs/DATABASE_SETUP.md)

### Executando o Projeto

1. **Clone o repositório**:

```bash
git clone <url-do-repositorio>
cd MauiCustomerApp
```

2. **Restaure os pacotes NuGet**:

```bash
dotnet restore
```

3. **Execute o projeto**:

```bash
dotnet run
```

Ou execute diretamente pelo Visual Studio (F5).

## 📱 Funcionalidades Detalhadas

### Tela Principal

-   **Lista de clientes** em `CollectionView` com binding para `ObservableCollection<Customer>`
-   **Botões de ação**: Add, Edit, Delete
-   **Seleção de item** para habilitar/desabilitar botões Edit e Delete

### Operações CRUD

#### ➕ Adicionar Cliente

-   **Nova janela modal** para inserção de dados
-   **Validação de campos** obrigatórios
-   **Botões Save/Cancel** para confirmar ou cancelar

#### ✏️ Editar Cliente

-   **Nova janela modal** com dados pré-preenchidos
-   **Atualização em tempo real** na lista principal
-   **Botões Save/Cancel** para confirmar ou cancelar

#### 🗑️ Excluir Cliente

-   **Confirmação via DisplayAlert** nativo do .NET MAUI
-   **Exclusão segura** com confirmação obrigatória
-   **Atualização automática** da lista após exclusão

## 🔧 Detalhes Técnicos

### Binding e Commands

-   Uso de `ICommand` para operações assíncronas
-   `ObservableCollection` para atualização automática da UI
-   Two-way binding para formulários

### Navegação

-   `PushModalAsync` para abrir telas de Add/Edit
-   Fechamento automático ao salvar ou cancelar
-   Navegação type-safe com injeção de dependência

### Tratamento de Dados

-   Operações assíncronas com `async/await`
-   `MainThread.BeginInvokeOnMainThread` para updates na UI
-   Entity Framework com tracking automático

## 🎨 Interface de Usuário

-   **Border** (substitui Frame obsoleto no .NET 9)
-   **CollectionView** para performance otimizada
-   **VerticalStackLayout** e **HorizontalStackLayout** para layouts
-   **Entry** controls com placeholders e validação de tipos

## 📝 Padrões Implementados

### ✅ Requisitos Atendidos

1. **✅ MVVM Pattern**: Implementado com ViewModels, Services e binding
2. **✅ Classe Customer**: Contém Name, Lastname, Age, Address
3. **✅ Tela inicial com lista**: CollectionView com operações CRUD
4. **✅ Novas janelas**: Modal pages para Add/Edit com botões Save/Cancel
5. **✅ Confirmação de exclusão**: DisplayAlert nativo do .NET MAUI
6. **✅ Injeção de dependência**: Container nativo configurado

### 🔄 Fluxo de Dados

```
View (XAML) ↔ ViewModel ↔ Service ↔ DbContext ↔ PostgreSQL
```

## 🚀 Futuras Melhorias

-   [ ] Validação de formulários mais robusta
-   [ ] Paginação para grandes volumes de dados
-   [ ] Busca e filtros
-   [ ] Temas claro/escuro
-   [ ] Cache local para offline
-   [ ] Testes unitários e de integração

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo `LICENSE.txt` para mais detalhes.

---

**Desenvolvido usando .NET MAUI**
