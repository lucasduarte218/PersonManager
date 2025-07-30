# 🚀 PersonManager API - Sistema de Gerenciamento de Pessoas

> **Uma API REST robusta, escalável e moderna construída com .NET 8, seguindo as melhores práticas de Clean Architecture e padrões de desenvolvimento enterprise.**

Acesse agora mesmo em NUVEM em :  https://www.PersonManager.somee.com
---

## 📋 Visão Geral

O **PersonManager API** é uma solução completa e profissional para gerenciamento de pessoas, desenvolvida com foco em **qualidade**, **performance**, **segurança** e **manutenibilidade**. A aplicação implementa um conjunto abrangente de funcionalidades utilizando tecnologias de ponta e padrões consagrados da indústria.

---

## 🏗️ Arquitetura Clean Architecture

### Estrutura em Camadas

```
PersonManager/
├── 🎯 PersonManager.API/           # Camada de Apresentação
├── 🧠 PersonManager.Application/   # Camada de Aplicação (Use Cases)
├── 📦 PersonManager.Domain/        # Camada de Domínio (Entidades)
├── 🔧 PersonManager.Infrastructure/ # Camada de Infraestrutura
├── 🔗 PersonManager.CrossCutting/  # Cross-Cutting Concerns
└── ✅ PersonManager.Tests/         # Testes Automatizados
```

**Benefícios da Arquitetura:**
- **Separação de Responsabilidades:** Cada camada tem uma responsabilidade específica
- **Testabilidade:** Isolamento de dependências facilita testes unitários
- **Manutenibilidade:** Código organizado e fácil de modificar
- **Escalabilidade:** Estrutura preparada para crescimento
- **Flexibilidade:** Fácil substituição de tecnologias

---

## 🎯 Funcionalidades Principais

### 🔐 Sistema de Autenticação e Autorização
- JWT (JSON Web Tokens) para autenticação stateless
- Registro de usuários com hash seguro de senhas (BCrypt)
- Login com validação de credenciais
- Autorização baseada em roles (Admin/User)
- Middleware de autenticação global

### 👥 Gerenciamento Completo de Pessoas
- CRUD Completo: Create, Read, Update, Delete
- Versionamento de API (v1 e v2) com contratos diferentes
- Validações robustas: CPF, email, campos obrigatórios
- Soft Delete: Exclusão lógica mantendo histórico
- Auditoria automática: CreatedAt, UpdatedAt, DeletedAt

### 📊 Sistema de Logs e Monitoramento
- Request/Response Logging: Captura completa de requisições
- Exception Handling: Tratamento centralizado de erros
- Correlation ID: Rastreabilidade de erros
- Logs detalhados: IP, headers, payloads, timestamps

### 🔄 Versionamento de API
- v1: Endereço opcional para pessoas
- v2: Endereço obrigatório para pessoas
- Backward compatibility: Versões anteriores mantidas funcionais

---

## 🛠️ Tecnologias e Padrões Implementados

### Core Technologies
- .NET 8: Framework mais recente e performático
- Entity Framework Core: ORM para acesso a dados
- SQL Server: Banco de dados relacional robusto
- ASP.NET Core Web API: Framework para APIs REST

### Design Patterns
- 🏛️ Clean Architecture: Arquitetura em camadas
- 📋 Use Case Pattern: Lógica de negócio encapsulada
- 🗄️ Repository Pattern: Abstração de acesso a dados
- 🎯 Dependency Injection: Inversão de controle
- ✅ Result Pattern: Tratamento elegante de erros
- 🏭 Builder Pattern: Construção flexível de objetos (testes)

### Security & Authentication
- 🔐 JWT Bearer Authentication: Tokens seguros
- 🔒 BCrypt: Hash seguro de senhas
- 🛡️ CORS: Configuração para cross-origin
- 🔑 Role-based Authorization: Controle de acesso

### Validation & Data Transfer
- 📦 DTOs (Data Transfer Objects): Contratos de API
- 🔄 Request/Response Models: Separação de camadas
- ✔️ Validation Helper: Validações centralizadas
- 🎯 Model Mapping: Conversão manual otimizada

### Testing & Quality
- 🧪 xUnit: Framework de testes
- 🎭 Moq: Mocking para testes unitários
- ✨ FluentAssertions: Assertions legíveis
- 🏗️ Builder Pattern: Criação de dados de teste
- 📊 Code Coverage: Cobertura de código

---

## 📚 API Endpoints

### 🔐 Autenticação
```
POST /api/v1/auth/login     # Login de usuário
POST /api/v1/auth/register  # Registro de usuário
```

### 👥 Pessoas v1 (Endereço opcional)
```
GET    /api/v1/person       # Listar pessoas
GET    /api/v1/person/{id}  # Obter pessoa por ID
POST   /api/v1/person       # Criar pessoa
PUT    /api/v1/person/{id}  # Atualizar pessoa
DELETE /api/v1/person/{id}  # Deletar pessoa (soft delete)
```

### 👥 Pessoas v2 (Endereço obrigatório)
```
GET    /api/v2/person       # Listar pessoas
GET    /api/v2/person/{id}  # Obter pessoa por ID
POST   /api/v2/person       # Criar pessoa (endereço obrigatório)
PUT    /api/v2/person/{id}  # Atualizar pessoa (endereço obrigatório)
DELETE /api/v2/person/{id}  # Deletar pessoa (soft delete)
```

---

## 🎯 Campos de Pessoa

**Obrigatórios:**
- Nome: String (obrigatório)
- CPF: String (obrigatório, validado, único)
- Data de Nascimento: DateTime (obrigatório)

**Opcionais (v1) / Obrigatórios (v2):**
- Endereço: String

**Sempre Opcionais:**
- Sexo: String
- Email: String (validado se preenchido)
- Naturalidade: String
- Nacionalidade: String

**Auditoria Automática:**
- CreatedAt: DateTime (preenchido automaticamente)
- UpdatedAt: DateTime (atualizado automaticamente)
- DeletedAt: DateTime? (soft delete)

---

## 🧪 Testes Automatizados

**Tipos de Testes:**
- ✅ Testes Unitários: Use Cases, Helpers, Validações
- 🏗️ Testes de Repositório: Com InMemory Database
- 🎯 Mocks e Stubs: Isolamento de dependências
- 📊 Cobertura de Código: Métricas de qualidade

**Ferramentas de Teste:**
- xUnit: Framework principal
- Moq: Criação de mocks
- FluentAssertions: Assertions expressivas
- AutoFixture: Geração de dados de teste
- InMemory Database: Testes de integração

---

## 🚀 Como Executar

**Pré-requisitos:**
- .NET 8 SDK
- SQL Server
- Visual Studio 2022 ou VS Code

**Configuração:**

1. Clone o repositório
2. Configure a connection string no `appsettings.json`
3. Execute as migrations:
    ```
    dotnet ef database update --project PersonManager.Infrastructure --startup-project PersonManager
    ```
4. Execute a API:
    ```
    dotnet run --project PersonManager
    ```
**Acesso:**
- API: https://localhost:5001
- Swagger: https://localhost:5001/swagger
- Nuvem: https://www.PersonManager.somee.com

---

## 📈 Vantagens Competitivas

### 🏆 Qualidade de Código
- Clean Code: Código limpo e legível
- SOLID Principles: Princípios de design seguidos
- DRY (Don't Repeat Yourself): Reutilização de código
- Separation of Concerns: Responsabilidades bem definidas

### 🔒 Segurança
- JWT Stateless: Autenticação sem estado
- Password Hashing: Senhas criptografadas
- Input Validation: Validação rigorosa de entrada
- Error Handling: Tratamento seguro de erros

### 📊 Performance
- .NET 8: Framework de alta performance
- Async/Await: Operações assíncronas
- Entity Framework: ORM otimizado
- Dependency Injection: Gerenciamento eficiente de objetos

### 🔧 Manutenibilidade
- Clean Architecture: Estrutura organizada
- Unit Tests: Testes automatizados
- Documentation: Código autodocumentado
- Swagger: Documentação automática da API

### 📈 Escalabilidade
- Stateless Design: Fácil escalabilidade horizontal
- Layered Architecture: Separação de responsabilidades
- Interface Segregation: Baixo acoplamento
- Configuration: Configuração externalizável

---

## 🎉 Por que Escolher PersonManager API?

### ✨ Pronto para Produção
- Arquitetura enterprise robusta
- Testes automatizados abrangentes
- Logging e monitoramento completos
- Tratamento de erros centralizado

### 🚀 Tecnologias Modernas
- .NET 8 (última versão)
- Entity Framework Core
- JWT Authentication
- Clean Architecture

### 🔧 Facilmente Extensível
- Estrutura modular
- Interfaces bem definidas
- Padrões consagrados
- Código bem documentado

### 📊 Observabilidade
- Logs detalhados de requests/responses
- Correlation IDs para rastreamento
- Exception handling robusto
- Swagger para documentação

---

## 📞 Suporte e Contato

Esta solução foi desenvolvida seguindo as melhores práticas da indústria, proporcionando uma base sólida e confiável para sistemas de gerenciamento de pessoas em ambientes corporativos.

**PersonManager API - Sua solução completa para gerenciamento de pessoas! 🚀**

---

Desenvolvido com ❤️ usando .NET 8 e Clean Architecture
