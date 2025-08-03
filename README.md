# 🎮 FIAP Cloud Games (FCG)

## 📚 Sobre o Projeto

Fiap Cloud Games (FCG) é uma plataforma inovadora de jogos na nuvem desenvolvida dentro do ecossistema educacional da FIAP (Faculdade de Informática e Administração Paulista). O projeto tem como objetivo oferecer aos alunos uma experiência prática e integrada no desenvolvimento, deployment e consumo de jogos hospedados em ambientes cloud.

[Documentação](https://www.notion.so/Fiap-Cloud-Games-FCG-1dea50ade75480e78653c05e2cca2193?pvs=4)

[API](https://fcgapi.proudcoast-ea66d6f7.brazilsouth.azurecontainerapps.io/)

## 🚀 Metas
- [X] Gerenciamento de usuários.
- [X] Gerenciamento de jogos.
- [X] Autenticação de usuários com JWT.
- [X] Níveis de acesso (Admin e Usuário comum).
- [X] Documentação da API com swagger.
- [ ] Biblioteca de jogos.
- [ ] Integração com plataformas de jogos (Steam, Epic Games, etc).
- [ ] Compra de jogos (com pagamento).
- [X] Gerenciamento e aplicação de promoções.
- [ ] Hospedagem da aplicação em um ambiente cloud (Azure, AWS ou GCP).
- [X] Pipeline CI/CD com deploy automatizado.
- [X] Monitoramento de logs e desempenho com uma ferramenta de observabilidade (ex: Application Insights, Grafana).
      
## ⚙️ Tecnologias Utilizadas

- [.NET 8](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio](https://visualstudio.microsoft.com/pt-br/)
- [EF Core](https://learn.microsoft.com/pt-br/ef/core/)
- [ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/)
- [XUnit](https://xunit.net/)
- [Swagger](https://swagger.io/)
- [Docker](https://www.docker.com/)

## 🛠️ Como Executar

### Usando Docker

1. Certifique-se de ter o [Docker](https://www.docker.com/get-started/) instalado em sua máquina.
2. No terminal, navegue até a raiz do projeto.
3. Execute o comando abaixo para construir e iniciar os containers:

```bash
docker-compose up -d --build
```

4. O serviço estará disponível em `http://localhost:5001/`.

5. Para se autenticar, vá para o endpoint '/api/auth/login' e use as credenciais abaixo: 
```json
{
  "email": "admin@fiap.com.br",
  "password": "Admin1234!"
}
```
Obs: Essas credenciais são criadas automaticamente por motivos academicos

## 🧪 Testes

- Para rodar os testes, utilize o **Test Explorer** do Visual Studio.
- Ou execute via terminal:

```bash
dotnet test
```

## 🤝 Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues ou pull requests.

## 📄 Licença

Este projeto está licenciado sob a licença MIT.

---

Feito com ❤️!
