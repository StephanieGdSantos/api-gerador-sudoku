# API Gerador de Sudoku

API REST desenvolvida em C# para geração aleatória de jogos Sudoku utilizando padrões de projeto (Design Patterns) e algoritmos especializados.

## 📋 Sobre o Projeto

Esta API permite a criação de puzzles Sudoku com diferentes níveis de dificuldade. O projeto foi desenvolvido utilizando uma arquitetura bem estruturada com diversos padrões de design para garantir código limpo, manutenível e escalável.

## 🏗️ Arquitetura e Padrões de Projeto

O projeto implementa os seguintes padrões de design:

- **Builder Pattern** (`Builders/`): Construção incremental de jogos Sudoku
- **Composite Pattern** (`Composites/`): Gerenciamento de quadrantes e células do tabuleiro
- **Decorator Pattern** (`Decorators/`): Adição de funcionalidades extras aos puzzles
- **Iterator Pattern** (`Iterators/`): Navegação pelos elementos do tabuleiro

## 🚀 Tecnologias Utilizadas

- .NET (ASP.NET Core)
- C#
- Docker

## 📁 Estrutura do Projeto

```
api-gerador-sudoku/
├── Builders/          # Construção de tabuleiros Sudoku
├── Composites/        # Estrutura de quadrados e células
├── Controllers/       # Endpoints da API
├── Decorators/        # Decoradores para funcionalidades extras
├── DTOs/             # Data Transfer Objects
├── Entities/         # Entidades do domínio
├── Iterators/        # Iteradores para navegação no tabuleiro
├── Mappers/          # Mapeamento entre entidades e DTOs
├── Solvers/          # Algoritmos de resolução de Sudoku
└── Program.cs        # Configuração da aplicação
```

## 🐳 Docker

O projeto inclui suporte a Docker. Para executar:

```bash
docker build -t api-gerador-sudoku .
docker run -p 8080:80 api-gerador-sudoku
```

## 🔧 Configuração

As configurações da aplicação estão nos arquivos:
- `appsettings.json`
- `appsettings.Development.json`

## 📝 Licença

Este projeto é de código aberto e está disponível para fins educacionais.
