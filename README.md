# API Gerador de Sudoku

API REST desenvolvida em C# para geração aleatória de jogos Sudoku utilizando padrões de projeto (Design Patterns) e algoritmos especializados.

## 📋 Sobre o Projeto

Esta API permite a criação de puzzles Sudoku com diferentes níveis de dificuldade (Fácil, Médio e Difícil). O projeto foi desenvolvido utilizando uma arquitetura bem estruturada com diversos padrões de design para garantir código limpo, manutenível e escalável.

### Funcionalidades

- ✅ Geração aleatória de tabuleiros Sudoku válidos
- ✅ Três níveis de dificuldade (Fácil, Médio, Difícil)
- ✅ Validação de solução de Sudoku
- ✅ Algoritmo de verificação de unicidade de solução
- ✅ API RESTful documentada com Swagger

## 🏗️ Arquitetura e Padrões de Projeto

O projeto implementa os seguintes padrões de design:

- **Builder Pattern** ([`Builders/`](APIGeradorSudoku/Builders/)): Construção incremental de jogos Sudoku através do [`ISudokuBuilder`](APIGeradorSudoku/Builders/ISudokuBuilder.cs) e [`SudokuBuilderImpl`](APIGeradorSudoku/Builders/Impl/SudokuBuilderImpl.cs)
- **Composite Pattern** ([`Composites/`](APIGeradorSudoku/Composites/)): Gerenciamento de quadrantes do tabuleiro através do [`IQuadradoComposite`](APIGeradorSudoku/Composites/IQuadradoComposite.cs)
- **Iterator Pattern** ([`Iterators/`](APIGeradorSudoku/Iterators/)): Navegação pelos elementos do tabuleiro para verificação de números já utilizados
- **Strategy Pattern** ([`Solvers/`](APIGeradorSudoku/Solvers/)): Algoritmos de resolução e validação de Sudoku
- **Provider Pattern** ([`Providers/`](APIGeradorSudoku/Providers/)): Abstração para geração de números aleatórios

## 🚀 Tecnologias Utilizadas

- .NET 9.0
- ASP.NET Core
- C# 12
- Swagger/OpenAPI
- Docker
- xUnit (Testes)
- Moq (Mocks para testes)

## 📁 Estrutura do Projeto

```
api-gerador-sudoku/
├── APIGeradorSudoku/
│   ├── Builders/          # Construção de tabuleiros Sudoku
│   │   ├── ISudokuBuilder.cs
│   │   └── Impl/
│   │       └── SudokuBuilderImpl.cs
│   ├── Composites/        # Estrutura de quadrados e células
│   │   ├── IQuadradoComposite.cs
│   │   └── Impl/
│   │       └── QuadradoCompositeImpl.cs
│   ├── Controllers/       # Endpoints da API
│   │   └── SudokuController.cs
│   ├── DTOs/             # Data Transfer Objects
│   ├── Entities/         # Entidades do domínio
│   │   ├── Sudoku.cs
│   │   ├── BlocoDeQuadrado.cs
│   │   └── Options/
│   ├── Iterators/        # Iteradores para navegação no tabuleiro
│   │   └── SudokuIterator.cs
│   ├── Mappers/          # Mapeamento entre entidades e DTOs
│   ├── Providers/        # Provedores de serviços
│   │   ├── IRandomProvider.cs
│   │   └── Impl/
│   ├── Services/         # Lógica de negócio
│   │   ├── ISudokuService.cs
│   │   └── Impl/
│   │       └── SudokuServiceImpl.cs
│   ├── Solvers/          # Algoritmos de resolução de Sudoku
│   │   ├── ISudokuSolver.cs
│   │   └── Impl/
│   │       └── SudokuSolverImpl.cs
│   ├── Program.cs        # Configuração da aplicação
│   ├── appsettings.json  # Configurações
│   └── Dockerfile        # Configuração Docker
└── APIGeradorSudoku.Tests/ # Testes unitários
    ├── Builders/
    ├── Composites/
    ├── Controllers/
    ├── Iterators/
    ├── Services/
    └── Solvers/
```

## 🐳 Docker

### Executar via Docker Hub

A imagem Docker está disponível publicamente no Docker Hub:

```bash
# Pull da imagem
docker pull stephaniegomes/apigeradorsudoku:latest

# Executar o container
docker run -d -p 8080:8080 --name sudoku-api stephaniegomes/apigeradorsudoku:latest
```

### Build local

Para construir a imagem localmente:

```bash
# Navegar até o diretório do projeto
cd APIGeradorSudoku

# Build da imagem
docker build -t api-gerador-sudoku .

# Executar o container
docker run -d -p 8080:8080 --name sudoku-api api-gerador-sudoku
```

### Acessar a aplicação

Após iniciar o container:

- **Swagger UI**: http://localhost:8080/swagger
- **API Base URL**: http://localhost:8080

## 🔧 Configuração

As configurações da aplicação estão em [`appsettings.json`](APIGeradorSudoku/appsettings.json):

```json
{
  "ConfiguracoesConstrucaoSudoku": {
    "NumeroMaximoTentativas": 100,
    "NumerosPossiveisPorQuadrado": [1, 2, 3, 4, 5, 6, 7, 8, 9],
    "OrdemGradePadrao": 9
  },
  "QuantidadeMaximaQuadradosEmBrancoPorNivel": {
    "Facil": 30,
    "Medio": 40,
    "Dificil": 50
  }
}
```

### Parâmetros configuráveis:

- **NumeroMaximoTentativas**: Número máximo de tentativas para preencher um quadrado
- **NumerosPossiveisPorQuadrado**: Array de números possíveis (1-9 para Sudoku padrão)
- **OrdemGradePadrao**: Ordem da grade Sudoku (9 para 9x9)
- **Facil/Medio/Dificil**: Quantidade máxima de células em branco por nível

## 📖 API Endpoints

### Gerar Sudoku

```http
POST /api/Sudoku/gerar?nivel={nivel}
```

**Parâmetros:**
- `nivel` (query): Nível de dificuldade - valores aceitos: `Facil`, `Medio`, `Dificil`

**Resposta (200 OK):**
```json
{
  "ordemGradeSudoku": 9,
  "ordemQuadradoSudoku": 3,
  "grade": [
    [5, 3, 0, 0, 7, 0, 0, 0, 0],
    [6, 0, 0, 1, 9, 5, 0, 0, 0],
    // ... mais linhas
  ]
}
```

## 🧪 Testes

O projeto inclui testes unitários abrangentes usando xUnit e Moq:

```bash
# Executar testes
dotnet test

# Executar testes com cobertura
dotnet test /p:CollectCoverage=true
```

### Áreas testadas:

- ✅ Builders: Construção de tabuleiros Sudoku
- ✅ Composites: Preenchimento de quadrados
- ✅ Controllers: Endpoints da API
- ✅ Iterators: Navegação no tabuleiro
- ✅ Services: Lógica de negócio
- ✅ Solvers: Algoritmos de resolução

Exemplos de testes podem ser encontrados em:
- [`SudokuBuilderImplTests.cs`](APIGeradorSudoku.Tests/Builders/SudokuBuilderImplTests.cs)
- [`QuadradoCompositeImplTests.cs`](APIGeradorSudoku.Tests/Composites/QuadradoCompositeImplTests.cs)
- [`SudokuServiceImplTests.cs`](APIGeradorSudoku.Tests/Services/SudokuServiceImplTests.cs)

## 🎯 Exemplos de Uso

### Gerar Sudoku Fácil

```bash
curl -X POST "http://localhost:8080/api/Sudoku/gerar?nivel=Facil"
```

### Gerar Sudoku Médio

```bash
curl -X POST "http://localhost:8080/api/Sudoku/gerar?nivel=Medio"
```

### Gerar Sudoku Difícil

```bash
curl -X POST "http://localhost:8080/api/Sudoku/gerar?nivel=Dificil"
```

### Usando PowerShell

```powershell
Invoke-RestMethod -Uri "http://localhost:8080/api/Sudoku/gerar?nivel=Facil" -Method Post
```

## 🔍 Algoritmos Implementados

### Geração de Sudoku

1. **Preenchimento de Quadrados**: Utiliza backtracking para preencher quadrados 3x3 garantindo que cada número apareça apenas uma vez
2. **Validação de Linha/Coluna**: O [`SudokuIterator`](APIGeradorSudoku/Iterators/SudokuIterator.cs) verifica números já utilizados em linhas e colunas
3. **Remoção Estratégica**: Remove números de forma aleatória respeitando o nível de dificuldade
4. **Verificação de Unicidade**: Garante que o puzzle gerado possui solução única através do [`SudokuSolverImpl`](APIGeradorSudoku/Solvers/Impl/SudokuSolverImpl.cs)

### Resolução de Sudoku

O solver implementa um algoritmo de backtracking eficiente que:
- Encontra células vazias
- Tenta números de 1 a 9
- Valida regras de linha, coluna e quadrado
- Retrocede quando não há solução
- Conta o número de soluções possíveis

## 🛠️ Desenvolvimento

### Pré-requisitos

- .NET 9.0 SDK
- Docker (opcional)
- Visual Studio 2022 ou VS Code

### Executar localmente

```bash
# Restaurar dependências
dotnet restore

# Compilar
dotnet build

# Executar
dotnet run --project APIGeradorSudoku
```

### Variáveis de Ambiente

Você pode sobrescrever configurações via variáveis de ambiente:

```bash
docker run -d \
  -p 8080:8080 \
  -e ConfiguracoesConstrucaoSudoku__NumeroMaximoTentativas=150 \
  -e QuantidadeMaximaQuadradosEmBrancoPorNivel__Facil=25 \
  stephaniegomes/apigeradorsudoku:latest
```

## 📝 Licença

Este projeto é de código aberto e está disponível para fins educacionais.

## 👩‍💻 Autora

**Stephanie Gomes**
- Docker Hub: [stephaniegomes](https://hub.docker.com/u/stephaniegomes)
- Linkedin: [Stephanie Gomes](https://www.linkedin.com/in/stephanie-gomes-842a192a7/)

---

⭐ Se este projeto foi útil para você, considere dar uma estrela no repositório!