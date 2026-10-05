# DeskFlow API

API REST para gerenciamento de chamados e suporte técnico de TI.

## 📋 Sobre o projeto

O DeskFlow API é um sistema de Helpdesk desenvolvido em .NET 10 para gerenciamento de chamados de suporte técnico.

O sistema permite:

- Cadastro e gerenciamento de categorias;
- Abertura de chamados;
- Controle do status dos chamados;
- Controle de prioridade;
- Início e encerramento de chamados;
- Registro de interações;
- Consulta de chamados com filtros;
- Relacionamento entre chamados, categorias e interações;
- Tratamento global de exceções.

## 🛠️ Tecnologias utilizadas

- .NET 10
- ASP.NET Core Web API
- Entity Framework Core 10
- SQL Server
- C#
- Git e GitHub

## 🏗️ Arquitetura

O projeto utiliza uma arquitetura em camadas:

```text
DeskFlow.API/
├── Controllers/
├── Services/
├── Repositories/
├── Models/
│   └── Entities/
├── Middlewares/
├── Data/
├── Database/
├── Migrations/
├── Program.cs
└── appsettings.json