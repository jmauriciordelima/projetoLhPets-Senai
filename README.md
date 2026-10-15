# LH-Pets Web API

Web API desenvolvida em **ASP.NET Core (C#)** com o padrão de **Controllers**, para o cadastro e a listagem de **clientes** e seus **pets** da empresa fictícia LH-Pets.

Projeto avaliativo do curso SENAI (Desenvolvedor Full Stack), unidade de Codificação para Back-End.

## Sumário

- [Objetivo](#objetivo)
- [Tecnologias](#tecnologias)
- [Estrutura do projeto](#estrutura-do-projeto)
- [Endpoints](#endpoints)
- [Exemplos de requisição](#exemplos-de-requisição)
- [Como executar](#como-executar)
- [Como testar no Swagger](#como-testar-no-swagger)
- [Regras e decisões do projeto](#regras-e-decisões-do-projeto)
- [O que foi pedido x o que foi entregue](#o-que-foi-pedido-x-o-que-foi-entregue)
- [Autor](#autor)

## Objetivo

Criar o back-end da LH-Pets, permitindo cadastrar e listar **clientes** e **pets**, com o vínculo entre eles feito pelo campo `ClienteId`.

Nesta etapa de prototipagem **não há banco de dados**: todos os dados ficam em memória, em coleções `List<T>`. Isso significa que os dados são perdidos sempre que a aplicação é reiniciada.

## Tecnologias

- C#
- .NET 8
- ASP.NET Core Web API (Controllers)
- Swagger / Swashbuckle (documentação e testes interativos)
- Git e GitHub
- Visual Studio Code

## Estrutura do projeto

```
LhPets/
├── Controllers/
│   ├── ClientesController.cs
│   └── PetsController.cs
├── Models/
│   ├── Cliente.cs
│   └── Pet.cs
├── Program.cs
└── LhPets.csproj
```

### Models

**Cliente**

| Propriedade | Tipo   |
|-------------|--------|
| Id          | int    |
| Nome        | string |
| Cpf         | string |
| Email       | string |

**Pet**

| Propriedade | Tipo   | Observação                       |
|-------------|--------|----------------------------------|
| Id          | int    |                                  |
| Nome        | string |                                  |
| Especie     | string |                                  |
| Raca        | string |                                  |
| ClienteId   | int    | Id do cliente dono do pet        |

## Endpoints

| Método | Rota                              | Descrição                                      | Respostas        |
|--------|-----------------------------------|------------------------------------------------|------------------|
| GET    | `/api/clientes`                   | Retorna a lista completa de clientes           | 200              |
| POST   | `/api/clientes`                   | Adiciona um novo cliente                       | 201              |
| GET    | `/api/pets`                       | Retorna todos os pets cadastrados              | 200              |
| POST   | `/api/pets`                       | Adiciona um pet vinculado a um cliente         | 201, 400         |
| GET    | `/api/pets/cliente/{clienteId}`   | Retorna apenas os pets do cliente informado    | 200              |

## Exemplos de requisição

### POST /api/clientes

```json
{
  "nome": "Maria Silva",
  "cpf": "123.456.789-00",
  "email": "maria@email.com"
}
```

Resposta (201):

```json
{
  "id": 1,
  "nome": "Maria Silva",
  "cpf": "123.456.789-00",
  "email": "maria@email.com"
}
```

### POST /api/pets

```json
{
  "nome": "Thor",
  "especie": "Cachorro",
  "raca": "Labrador",
  "clienteId": 1
}
```

Resposta (201):

```json
{
  "id": 1,
  "nome": "Thor",
  "especie": "Cachorro",
  "raca": "Labrador",
  "clienteId": 1
}
```

Se o `clienteId` não existir, a API responde **400 Bad Request** com a mensagem `Cliente não encontrado.`

## Como executar

### Pré-requisitos

- [.NET SDK 8](https://dotnet.microsoft.com/download)
- [Git](https://git-scm.com/)
- [Visual Studio Code](https://code.visualstudio.com/) (opcional)

Para conferir a instalação do .NET:

```bash
dotnet --version
```

### Passo a passo

1. Clone o repositório:

   ```bash
   git clone https://github.com/SEU_USUARIO/LhPets.git
   ```

2. Entre na pasta do projeto (a mesma que contém o arquivo `LhPets.csproj`):

   ```bash
   cd LhPets
   ```

3. Execute a aplicação:

   ```bash
   dotnet run
   ```

4. Observe no terminal a URL em que a aplicação está escutando (por exemplo, `http://localhost:5119`).

5. Abra o Swagger no navegador:

   ```
   http://localhost:5119/swagger
   ```

   A porta pode variar. Use a que aparecer no seu terminal.

6. Para encerrar a aplicação, pressione `Ctrl+C` no terminal.

> Se aparecer o erro `address already in use`, há outra instância da aplicação rodando. Encerre-a com `Ctrl+C` antes de executar novamente.

## Como testar no Swagger

Sequência sugerida para validar todos os endpoints:

1. `POST /api/clientes` com um cliente. Esperado: **201**.
2. `GET /api/clientes`. Esperado: **200** com o cliente cadastrado.
3. `POST /api/pets` com `clienteId: 1`. Esperado: **201**.
4. `POST /api/pets` com `clienteId: 99` (inexistente). Esperado: **400**.
5. `GET /api/pets`. Esperado: **200** com todos os pets.
6. `GET /api/pets/cliente/1`. Esperado: **200** apenas com os pets do cliente 1.

Para cada endpoint, clique em **Try it out**, preencha os dados e clique em **Execute**.

## Regras e decisões do projeto

- **Armazenamento em memória:** as listas `Clientes` e `Pets` são `static`, para que os dados sobrevivam entre requisições. Os controllers são criados a cada requisição, então uma lista não estática seria recriada vazia toda vez.
- **Geração de Id:** o Id é gerado automaticamente pela API (maior Id da lista + 1, ou 1 se a lista estiver vazia).
- **Vínculo entre entidades:** ao cadastrar um pet, a API confere se o `ClienteId` informado existe. Se não existir, retorna **400 Bad Request**, pois o dado inválido está no corpo da requisição.
- **Swagger:** habilitado no ambiente de desenvolvimento.

## O que foi pedido x o que foi entregue

| Requisito do enunciado                                          | Situação  |
|-----------------------------------------------------------------|-----------|
| Web API em ASP.NET Core (C#) com padrão de Controllers         | Entregue  |
| Model `Cliente` (Id, Nome, Cpf, Email)                          | Entregue  |
| Model `Pet` (Id, Nome, Especie, Raca, ClienteId)                | Entregue  |
| `GET /api/clientes`                                             | Entregue  |
| `POST /api/clientes`                                            | Entregue  |
| `GET /api/pets`                                                 | Entregue  |
| `POST /api/pets` vinculado a um cliente                         | Entregue  |
| `GET /api/pets/cliente/{clienteId}`                             | Entregue  |
| Dados em memória com `List<T>` (sem banco de dados)             | Entregue  |
| Testes e validação dos endpoints pelo Swagger                   | Entregue  |
| Repositório público no GitHub                                   | Entregue  |

## Autor

José Maurício
