[SuperStore] - Aplicação de Ecommerce simples com MVC e API RESTful
1. Apresentação
Bem-vindo ao repositório do projeto [SuperStore]. Este projeto é uma entrega do MBA DevXpert Full Stack .NET e é referente ao módulo Introdução ao Desenvolvimento ASP.NET Core. O objetivo principal desenvolver uma aplicação de ecommerce que permite aos usuários criar, editar, visualizar e excluir produtos e categorias, tanto através de uma interface web utilizando MVC quanto através de uma API RESTful. O projeto é uma versão simplificada de um ecommerce, com um CRUD simples e validação de autenticação e autorização.

Autor(es)

Tiago Henrique de Castilhos

2. Proposta do Projeto

O projeto consiste em:

Aplicação MVC: Interface web para interação com o ecommerce.

API RESTful: Exposição dos recursos do ecommerce para integração com outras aplicações ou desenvolvimento de front-ends alternativos.

Autenticação e Autorização: Implementação de controle de acesso, diferenciando administradores e usuários comuns.

Acesso a Dados: Implementação de acesso ao banco de dados através de ORM.

3. Tecnologias Utilizadas

Linguagem de Programação: C#

Frameworks:

ASP.NET Core MVC

ASP.NET Core Web API

Entity Framework Core

Banco de Dados: SQLite e SQL Server

Autenticação e Autorização:

ASP.NET Core Identity

JWT (JSON Web Token) para autenticação na API

Front-end:

Razor Pages/Views

HTML/CSS para estilização básica

Documentação da API: Swagger

4. Estrutura do Projeto

A estrutura do projeto é organizada da seguinte forma:

src/

SuperStore.MVC/ - Projeto MVC

SuperStore.Api/ - API RESTful

SuperStore.Data/ - Modelos de Dados e Configuração do EF Core

SuperStore.Core/ - Serviços responsáveis pela validação e orquestração da 
parte lógica da aplicação

README.md - Arquivo de Documentação do Projeto

FEEDBACK.md - Arquivo para Consolidação dos Feedbacks

.gitignore - Arquivo de para ignorar arquivos e pastas do Git

5. Funcionalidades Implementadas

CRUD para Produtos e Categorias: Permite criar, editar, visualizar e excluir produtos e categorias.

Autenticação e Autorização: Diferenciação entre usuários comuns e administradores.

API RESTful: Exposição de endpoints para operações CRUD via API.
Documentação da API: Documentação automática dos endpoints da API utilizando Swagger.

6. Como Executar o Projeto

Pré-requisitos

.NET SDK 8.0 ou superior

SQL Server (Opcional)

Visual Studio 2022 ou superior (ou qualquer IDE de sua preferência)

Git

Passos para Execução

Clone o Repositório:

git clone https://github.com/seu-usuario/nome-do-repositorio.git
cd nome-do-repositorio

Caso deseje utilizar o SQLite como banco de dados, não se faz necessário configurar o SQL server.

6.1 (Opcional - Uso do SQL server)
Configuração do Banco de Dados:

No arquivo appsettings.json, configure a string de conexão do SQL Server.
Rode o projeto para que a configuração do Seed crie o banco e popule com os dados básicos.

Executar a Aplicação MVC:

cd src/SuperStore.Mvc/
dotnet run
Acesse a aplicação em: http://localhost:5151
Executar a API:

cd src/SuperStore.Api/
dotnet run
Acesse a documentação da API em: http://localhost:5047/swagger

A aplicação possui algumas categorias, produtos e um usuário pré configurados. As credenciais para esse usuário são:

E-mail: test@test.com

Senha: Senha123@

7. Instruções de Configuração
JWT para API: As chaves de configuração do JWT estão no appsettings.json.
Migrações do Banco de Dados: As migrações são gerenciadas pelo Entity Framework Core. Não é necessário aplicar devido a configuração do Seed de dados.

8. Documentação da API
A documentação da API está disponível através do Swagger. Após iniciar a API, acesse a documentação em:

http://localhost:5047/swagger

9. Avaliação

Este projeto é parte de um curso acadêmico e não aceita contribuições externas.

Para feedbacks ou dúvidas utilize o recurso de Issues

O arquivo FEEDBACK.md é um resumo das avaliações do instrutor e deverá ser modificado apenas por ele.