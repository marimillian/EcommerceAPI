
# Introdução
Esta API foi desenvolvida durante o projeto de capacitação da empresa Cleasale chamado Cleartech, onde foi possível aplicar todo o conhecimento adquirido ao longo de 9 meses. O intuito da API é simular um E-commerce com requisições de cadastrar, editar, excluir e pesquisar Categorias, Subcategorias, Produtos, Centro de Distribuição, Carrinho de Compras e Usuários. 

# Como usar
É possível utilizar o Postman e Swagger para realizar as requisições. Há particularidades de acessos para algumas das requisições no E-commerce, pois é necessário o login de Administrador ou Lojista. Para cadastrar o usuário deve realizar a requisição através de UsuariosApi. O login do administrador já é configurado:

E-mail
```csharp
admin@admin.com
```
Senha
```csharp
Admin123!
```
Além disso, a API consome uma API externa do ViaCEP para cadastrar informações sobre endereços, como por exemplo cadastrar o Centro de Distribuição.

Para utilizar esta API, basta clonar o repositório e excutar alguns comandos no Gerenciador de Pacote:

Migration
```csharp
Add-Migration "Migration"
```
Update
```csharp
Update-Dabate
```

Após as configurações é só executar a API que o Swagger abre automaticamente para realizar as requisições desejadas. 

# Tecnologias
- C#
- ASP .NET Core 6
- Entity Framework Core
- Identity Framework
- AutoMapper
- Sqlite
- MySQL
- Dapper
- FluentResults
- Serilog
- Xunit
- HATEOAS

