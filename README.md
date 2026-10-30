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
- [Requisitos](#requisitos)
- [Matriz de rastreabilidade](#matriz-de-rastreabilidade)
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
- Data Annotations (validação dos Models)
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

| Propriedade | Tipo   | Validação                                 |
|-------------|--------|-------------------------------------------|
| Id          | int    | Gerado pela API                           |
| Nome        | string | Obrigatório, de 3 a 100 caracteres        |
| Cpf         | string | Obrigatório (não valida dígitos do CPF)   |
| Email       | string | Obrigatório, em formato de e-mail válido  |

**Pet**

| Propriedade | Tipo   | Validação                                 |
|-------------|--------|-------------------------------------------|
| Id          | int    | Gerado pela API                           |
| Nome        | string | Obrigatório, de 3 a 100 caracteres        |
| Especie     | string | Obrigatório                               |
| Raca        | string | Obrigatório                               |
| ClienteId   | int    | Maior que zero; deve ser de um cliente existente |

## Endpoints

| Método | Rota                              | Descrição                                      | Respostas   |
|--------|-----------------------------------|------------------------------------------------|-------------|
| GET    | `/api/clientes`                   | Retorna a lista completa de clientes           | 200         |
| POST   | `/api/clientes`                   | Adiciona um novo cliente                       | 201, 400    |
| GET    | `/api/pets`                       | Retorna todos os pets cadastrados              | 200         |
| POST   | `/api/pets`                       | Adiciona um pet vinculado a um cliente         | 201, 400    |
| GET    | `/api/pets/cliente/{clienteId}`   | Retorna apenas os pets do cliente informado    | 200         |

O **400** ocorre quando o corpo da requisição não passa nas validações dos Models e, no `POST /api/pets`, também quando o `ClienteId` não existe.

## Exemplos de requisição

### POST /api/clientes

```json
{
  "nome": "Ana Souza",
  "cpf": "12345678900",
  "email": "ana@email.com"
}
```

Resposta (201):

```json
{
  "id": 1,
  "nome": "Ana Souza",
  "cpf": "12345678900",
  "email": "ana@email.com"
}
```

### POST /api/pets

```json
{
  "nome": "Bob",
  "especie": "Cão",
  "raca": "Labrador",
  "clienteId": 1
}
```

Resposta (201):

```json
{
  "id": 1,
  "nome": "Bob",
  "especie": "Cão",
  "raca": "Labrador",
  "clienteId": 1
}
```

Se o `clienteId` não existir, a API responde **400 Bad Request**:

```json
{
  "mensagem": "O cliente informado não existe."
}
```

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
   git clone https://github.com/jmauriciordelima/projetoLhPets-Senai.git
   ```

2. Entre na pasta do projeto (a mesma que contém o arquivo `LhPets.csproj`):

   ```bash
   cd projetoLhPets-Senai
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

Mantenha a aplicação rodando durante os testes: os dados ficam em memória e desaparecem quando o processo reinicia.

Para cada endpoint, clique em **Try it out**, preencha os dados e clique em **Execute**.

| Teste | Ação                                                      | Esperado                                   |
|-------|-----------------------------------------------------------|--------------------------------------------|
| CT001 | `POST /api/clientes` com um cliente válido                | 201 Created                                |
| CT002 | `GET /api/clientes`                                       | 200 OK com o cliente cadastrado            |
| CT003 | `POST /api/pets` com `clienteId` de um cliente existente  | 201 Created                                |
| CT004 | `POST /api/pets` com `clienteId: 9999`                    | 400 e mensagem "O cliente informado não existe." |
| CT005 | `GET /api/pets`                                           | 200 OK com o pet cadastrado                |
| CT006 | `GET /api/pets/cliente/1`                                 | 200 OK apenas com os pets do cliente 1     |
| CT007 | `POST /api/clientes` com e-mail inválido (ex.: `abc`)     | 400 (validação do Model)                   |
| CT008 | `POST /api/clientes` com corpo `{}`                       | 400 (campos obrigatórios)                  |

Se um cliente não tiver pets, o `GET /api/pets/cliente/{clienteId}` retorna 200 com lista vazia.

## Regras e decisões do projeto

- **Armazenamento em memória:** as listas `Clientes` e `Pets` são `static`, para que os dados sobrevivam entre requisições. Os controllers são criados a cada requisição, então uma lista não estática seria recriada vazia toda vez.
- **Geração de Id:** o Id é gerado automaticamente pela API (maior Id da lista + 1, ou 1 se a lista estiver vazia). Usar o maior Id, e não a quantidade de itens, evita repetição caso a API venha a permitir remoções.
- **Validação dos Models:** feita com Data Annotations (`[Required]`, `[StringLength]`, `[EmailAddress]`, `[Range]`). Com `[ApiController]`, a API responde 400 automaticamente, antes de executar o método do controller. O `[Required]` do CPF só exige que o campo seja informado; não valida os dígitos verificadores.
- **Vínculo entre entidades:** ao cadastrar um pet, a API confere se o `ClienteId` informado existe. Se não existir, retorna **400 Bad Request**, pois o dado inválido está no corpo da requisição.
- **Filtro por cliente:** um cliente sem pets recebe 200 com lista vazia, e não 404.
- **Swagger:** habilitado no ambiente de desenvolvimento.

## Requisitos

### Requisitos funcionais

| ID    | Requisito                                          | Implementação prevista       |
|-------|----------------------------------------------------|------------------------------|
| RF001 | Permitir cadastrar clientes                        | `POST /api/clientes`         |
| RF002 | Permitir listar todos os clientes                  | `GET /api/clientes`          |
| RF003 | Permitir cadastrar pets vinculados a clientes existentes | `POST /api/pets`       |
| RF004 | Permitir listar todos os pets                      | `GET /api/pets`              |
| RF005 | Permitir consultar os pets de um cliente específico | `GET /api/pets/cliente/{clienteId}` |

### Requisitos não funcionais e restrições

| ID     | Condição / qualidade                                       | Origem                              |
|--------|------------------------------------------------------------|-------------------------------------|
| RNF001 | Disponibilizar interface interativa para testar endpoints  | A atividade solicita Swagger        |
| RNF002 | Manipular dados em memória, sem banco relacional           | Restrição explícita da atividade    |
| RNF003 | Organizar endpoints e models para facilitar identificação  | Proposta de clareza/usabilidade     |

## Matriz de rastreabilidade

| ID    | Requisito                         | Implementação                          | Teste        | Status       |
|-------|-----------------------------------|----------------------------------------|--------------|--------------|
| RF001 | Cadastrar clientes                | `ClientesController.Post`              | CT001, CT007, CT008 | Atendido |
| RF002 | Listar clientes                   | `ClientesController.Get`               | CT002        | Atendido |
| RF003 | Cadastrar pet com cliente existente | `PetsController.Post`                | CT003        | Atendido |
| RF003 | Rejeitar cliente inexistente      | `PetsController.Post`                  | CT004        | Atendido |
| RF004 | Listar pets                       | `PetsController.Get`                   | CT005        | Atendido |
| RF005 | Filtrar pets por cliente          | `PetsController.GetPorCliente`         | CT006        | Atendido |
| RNF001 | Swagger interativo               | `Program.cs` / Swagger UI              | Abrir o Swagger e executar uma requisição | Atendido |
| RNF002 | Dados em memória                 | Listas `Clientes` e `Pets`             | Cadastrar e consultar na mesma execução | Atendido |

Status possíveis: **Atendido**, **Falhou** ou **Não testado**.

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
| Validação dos dados de entrada                                  | Entregue  |
| Testes e validação dos endpoints pelo Swagger                   | Entregue  |
| Repositório público no GitHub                                   | Entregue  |

## Autor

José Maurício
