# AS-Back-End

API de Gerenciamento de Usuários
Descrição
Esta API foi desenvolvida para gerenciar usuários, incluindo criação, listagem, atualização e exclusão. Foi implementada utilizando padrões de projeto que facilitam manutenção. E boas práticas de desenvolvimento.

O projeto também inclui validações de negócio, como email único, idade mínima e normalização de dados.

Tecnologias Utilizadas
.NET 8.0

Entity Framework Core

SQLite

FluentValidation

Outros: Swagger/OpenAPI, Postman

Padrões de Projeto Implementados
Repository Pattern

Service Pattern

DTO Pattern

Dependency Injection

Pré-requisitos
.NET SDK 8.0 ou superior
Passos
Clone o repositório

Execute as migrations

Execute a aplicação

Exemplos de Requisições
POST que cria um novo usuario.

{ "nome": "Pedro Augusto", "email": "pedro@example.com", "senha": "123456", "dataNascimento": "2000-05-15", "telefone": "(11) 98765-4321" }

PUT que atualiza um usuario por completo.

"nome": "Pedro Augusto", "email": "pedro@example.com", "senha": "123456", "dataNascimento": "2000-05-15", "telefone": "(11) 98765-4321"

        |    |
        |    |
       \      /
        \    /
         \  /
          \/

{ "nome": "Pedro A. Silva", "email": "pedro.silva@example.com", "dataNascimento": "2002-11-25", "telefone": "(11) 91265-3341", "ativo": true }

Estrutura do Projeto
Dentro da pasta Aplications ficam as pastas:
        *DTO onde foram mexidos nos DTOs;
        *Interfaces onde criei e alterei as interfaces do service e repository;
        *Service onde foi alterado a parte do service;
        *Validators para validar o create e update dos DTOs.

Pasta Domain/Entites onde fica a entidade Usuario.

Infastructure tem dus pastas: 
        *Persistence com AppDbContext;
        *Repositories onde fica o repositorio.

Temos tambem o Program.cs que é a parte para construir e rodar a aplicação.

E o ReadMe que estamos agora.

Autor:
Nome completo: João Pedro Matos da Silva

RA: 2025000438

Curso: [Analise e Desenvolvimento de Sistemas]
