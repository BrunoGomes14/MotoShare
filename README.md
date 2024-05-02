# MotoShare

Este projeto possui a finalidade de gerenciar o cadastro e aluguel de motos para entregadores.

## Requisitos

- **Internet:** Este projeto requer uma conexão com a internet, pois possui dependências na nuvem, incluindo MongoDB e RabbitMQ.
- **.NET 7:** O projeto é desenvolvido utilizando o framework .NET 7. Certifique-se de ter o .NET 7 instalado antes de executar o projeto.

## Arquitetura e Design Patterns

- **Arquitetura DDD (Domain-Driven Design):** O projeto segue uma arquitetura baseada em Domain-Driven Design, organizando o código em camadas distintas, como Domínio, Aplicação e Infraestrutura.
- **Design Patterns:**
  - **Mediator:** O projeto utiliza o padrão Mediator para facilitar a comunicação entre componentes, reduzindo o acoplamento e tornando o código mais modular.
  - **Injeção de Dependências:** A Injeção de Dependências é amplamente utilizada para facilitar a manutenção e testabilidade do código.
  - **Scoped:** Alguns serviços são registrados como Scoped para garantir que as instâncias sejam criadas uma vez por solicitação HTTP.

## Banco de Dados

- **MongoDB (NoSQL):** O projeto utiliza o banco de dados NoSQL.

## Executando o Projeto

1. Certifique-se de ter o .NET 7 instalado na sua máquina.
2. Clone este repositório.
3. Navegue até o diretório do projeto
4. Inicie o projeto ```MotoShare.Api```
5. Uma vez iniciado, você pode acessar a documentação Swagger para explorar a API no seguinte endpoint:
```http://localhost:PORT/swagger/index.html```

Substitua `PORT` pela porta em que o servidor está sendo executado.


