# FIAP Cloud Games 🚀

API RESTful para gerenciar usuários e biblioteca de jogos digitais, desenvolvida em **.NET 8** como MVP para a plataforma FIAP Cloud Games. Este repositório reúne todo o código-fonte, documentação e instruções de uso.

---

## Índice 📑

1. [🎯 Objetivos](#objetivos)
2. [🖼️ Visão Geral do Projeto](#visão-geral-do-projeto)
3. [🛠️ Tecnologias](#tecnologias)
4. [📋 Pré-requisitos](#prérequisitos)
5. [📂 Estrutura do Repositório](#estrutura-do-repositório)
6. [⚙️ Configuração Inicial](#configuração-inicial)

   - [1. Clonar o Repositório](#1-clonar-o-repositório)
   - [2. Ajustar ConnectionString](#2-ajustar-connectionstring)
   - [3. Aplicar Migrations](#3-aplicar-migrations)

7. [▶️ Como Executar a API](#como-executar-a-api)

   - [🔍 Acesse o Swagger](#acesse-o-swagger)

8. [🔗 Endpoints Principais](#endpoints-principais)

   - [🔑 Autenticação](#autenticação)
   - [👤 Usuários](#usuários)
   - [🎮 Jogos](#jogos)
   - [📚 Biblioteca de Jogos](#biblioteca-de-jogos)

9. [✅ Testes Unitários](#testes-unitários)
10. [🤝 Contribuindo](#contribuindo)
11. [📄 Licença](#licença)

---

## 🎯 Objetivos

- **Cadastro de Usuários**

  - Persistir informações de clientes (nome, e-mail e senha).
  - Validar formato de e-mail e exigir senha segura (mín. 8 caracteres, incluindo letras, números e caracteres especiais).

- **Autenticação e Autorização**

  - Autenticação via JWT (JSON Web Token).
  - Dois perfis de acesso:

    - **Usuário**: consulta catálogo e biblioteca de jogos.
    - **Administrador**: cadastra jogos, gerencia usuários e cria promoções.

- **Gerenciamento de Jogos**

  - CRUD completo de jogos (somente administradores).
  - Controle da biblioteca de jogos adquiridos por cada usuário.

- **Qualidade de Software**

  - Segregação em camadas (Domain, Infrastructure, Application, API e Tests).
  - Testes unitários cobrindo regras principais de negócio.
  - EF Core Migrations para criação e atualização do banco de dados.
  - Documentação de endpoints via Swagger.

---

## 🖼️ Visão Geral do Projeto

O FIAP Cloud Games é um MVP que permite:

1. Cadastrar novos usuários (com perfis “Usuário” ou “Administrador”).
2. Autenticar-se via token JWT.
3. Listar, criar, editar e remover jogos (restrito a administradores).
4. Consultar catálogo de jogos por qualquer usuário.
5. Adicionar/remover jogos na biblioteca de um usuário (simulação de compra/devolução).

Boa parte da lógica está isolada em projetos de **Application** e **Domain** para facilitar testes e manutenções, seguindo boas práticas de Clean Architecture e SOLID.

---

## 🛠️ Tecnologias

<p align="">
  <a href="https://skillicons.dev">
    <img src="https://skillicons.dev/icons?i=cs,dotnet,git,github" />
  </a>
</p>

- **Linguagem**: C# 10.0 (.NET 8)
- **Framework Web**: ASP.NET Core (Minimal API + Controllers MVC)
- **ORM**: Entity Framework Core
- **Banco de Dados**: SQL Server (configurável em `appsettings.json`)
- **Autenticação**: JWT (Bearer)
- **Testes**: xUnit
- **Documentação**: Swagger UI
- **Versionamento**: GitHub (Git flow)

---

## 📋 Pré-requisitos

Antes de começar, você precisa ter instalado:

1. **.NET 8 SDK** (8.0.x)
2. **SQL Server** (local ou remoto; versão 2019 ou superior recomendada)
3. **IDE ou Editor**: Visual Studio 2022 / Visual Studio Code / Rider
4. **Git** instalado e configurado para clonar repositórios

---

## 📂 Estrutura do Repositório

```
FIAP-Cloud-Games/
├── Application/              ← Casos de uso, DTOs e serviços de aplicação
├── Domain/                   ← Entidades, Value Objects e regras de negócio
├── Infrastructure/           ← Contexto EF Core, Migrations e implementações de repositórios
├── FIAP-Cloud-Games/         ← Projeto ASP.NET Core (Program.cs, Controllers, Middlewares)
├── FIAP-Cloud-GamesTest/     ← Testes de unidade (xUnit)
├── .gitignore
└── README.md                 ← Este arquivo
```

- **Application**: contém pastas como `DTOs/`, `Services/`, `Interfaces/` e `Validators/`.
- **Domain**: define entidades principais (`User`, `Game`, `Library`), value objects e exceptions customizadas.
- **Infrastructure**: configura o `DbContext`, Migrations e repositórios concretos.
- **FIAP-Cloud-Games**: projeto principal que expõe endpoints HTTP, middleware de autenticação e configurações de DI.
- **FIAP-Cloud-GamesTest**: contém casos de teste para serviços de negócio, validação de dados e fluxos de compra de jogo.

---

## ⚙️ Configuração Inicial

### 1. Clonar o Repositório

```bash
git clone https://github.com/LucasLosano/FIAP-Cloud-Games.git
cd FIAP-Cloud-Games
```

### 2. Ajustar ConnectionString

Abra o arquivo `FIAP-Cloud-Games/appsettings.json` e localize a seção `ConnectionStrings`. Substitua pelo seu servidor, nome de banco e credenciais:

```jsonc
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=SEU_SERVIDOR;Database=FIAPCloudGames;User Id=SEU_USUARIO;Password=SUA_SENHA;"
  },
  "Jwt": {
    "Key": "ChaveSuperSecretaParaJWT",
    "Issuer": "FIAPCloudGamesAPI",
    "Audience": "FIAPCloudGamesClient",
    "ExpiresInMinutes": 60
  }
}
```

- **Server**: pode ser `localhost` ou `.`.
- **Database**: nome do banco (ex.: `FIAPCloudGames`).
- **User Id / Password**: credenciais de acesso ao SQL Server.

> Se preferir usar autenticação do Windows, ajuste para `"DefaultConnection": "Server=localhost;Database=FIAPCloudGames;Trusted_Connection=True;"`.

### 3. Aplicar Migrations

No terminal, dentro da pasta raiz do repositório, navegue até `Infrastructure/` e execute:

```bash
cd Infrastructure
dotnet ef database update
```

Isso criará o banco de dados com todas as tabelas necessárias para usuários, jogos, biblioteca, tokens e logs.

---

## ▶️ Como Executar a API

1. **Via CLI**

   ```bash
   cd FIAP-Cloud-Games
   dotnet run
   ```

   - Por padrão, o servidor iniciará em `https://localhost:5001` (HTTPS) e `http://localhost:5000` (HTTP).

2. **Via IDE**

   - Abra a solução no Visual Studio / Rider / VS Code.
   - Defina `FIAP-Cloud-Games` como projeto de inicialização.
   - Pressione F5 (ou equivalente) para executar em modo Debug.

### 🔍 Acesse o Swagger

Depois que a API estiver rodando, abra no navegador:

```
https://localhost:5001/swagger
```

A interface Swagger permitirá visualizar e testar todos os endpoints protegidos e públicos.

---

## ✅ Testes Unitários

Toda a lógica de negócio crítica possui cobertura de testes em **FIAP-Cloud-GamesTest**. Para executar:

1. Abra um terminal na pasta do projeto de testes:

   ```bash
   cd FIAP-Cloud-GamesTest
   ```

2. Execute:

   ```bash
   dotnet test
   ```

   - Você verá um relatório informando quais testes passaram/falharam.
   - Cenários cobertos incluem: validação de senha, fluxo de cadastro, regras de negócio de biblioteca e permissões de administrador.

---
