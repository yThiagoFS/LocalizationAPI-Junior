# Desafio balta.io - Web API

Este repositório contém o código-fonte de uma Web API desenvolvida como parte de um desafio criado pelo <a href="https://www.linkedin.com/in/andrebaltieri/">André Baltieri (<a href="https://balta.io/">balta.io</a>)</a>. A API foi construída usando ASP.NET Core 7 e possui funcionalidades de gerenciamento de localizações e autenticação de usuários.

## Conteúdo

- [Recursos](#recursos)
- [Requisitos](#requisitos)
- [Configuração](#configuração)
- [Uso](#uso)

## Recursos

A Web API oferece as seguintes funcionalidades:

- Registro de usuários.
- Autenticação de usuários.
- Gerenciamento de localizações por estado, cidade e código.
- Criação, atualização e exclusão de localizações.
- Tratamento de erros e validações.

## Requisitos

Antes de iniciar o projeto, certifique-se de atender aos seguintes requisitos:

- [Visual Studio](https://visualstudio.microsoft.com/) ou [Visual Studio Code](https://code.visualstudio.com/).
- [.NET Core SDK](https://dotnet.microsoft.com/download) instalado.
- Um banco de dados compatível (como o SQL Server) configurado.
- Pacotes e dependências listados no arquivo `.csproj` instalados.

## Configuração

Siga as etapas abaixo para configurar e executar o projeto:

1. Clone este repositório:

   ```sh
   git clone https://github.com/yThiagoFS/balta.io_challenge.git
   
2. Acesse a pasta do projeto:
   ```sh
   cd BaltaIoChallenge.WebApi
   
3. Configure a conexão com o banco de dados no arquivo `appsettings.json`.
   
4. Execute o seguinte comando para aplicar as migrações e criar o banco de dados:
   ```sh
   dotnet ef database update
   
5. Inicie o projeto:
   ```sh
   dotnet run

A API estará disponível em `http://localhost:5000`.

## Uso
A API possui as seguintes rotas:

- `POST /api/v1/auth/register`: Registra um novo usuário.
- `POST /api/v1/auth/login`: Autentica um usuário e gera um token de acesso.
- `GET /api/v1/localization/getByState?state={state}`: Obtém localizações por estado.
- `GET /api/v1/localization/getByCity?city={city}`: Obtém localizações por cidade.
- `GET /api/v1/localization/getById/{id}`: Obtém uma localização por código.
- `POST /api/v1/localization`: Cria uma nova localização (requer autenticação).
- `PUT /api/v1/localization/updateById/{id}`: Atualiza uma localização existente (requer autenticação).
- `DELETE /api/v1/localization/deleteById/{id}`: Exclui uma localização existente (requer autenticação).
  
*Lembre-se de autenticar-se antes de usar as rotas protegidas.*

© 2023 Thiago Ferreira
