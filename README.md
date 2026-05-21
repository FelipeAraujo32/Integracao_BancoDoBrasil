# Integração Banco do Brasil SDK (.NET)

SDK .NET para integração com a API de boletos do Banco do Brasil, com foco em validação de payload, regras de negócio e serialização consistente para o endpoint de registro de boletos.

## Visão geral técnica

O projeto é organizado em duas camadas principais:

- `BancoDoBrasil`: biblioteca principal de integração.
- `BancoDoBrasil.Tests`: suíte de testes automatizados de contrato, composição, formatação e validações.

O fluxo de registro de boleto realiza:

1. Validação estrutural do DTO.
2. Validações de negócio.
3. Normalização/formatação para padrões aceitos pela API.
4. Serialização JSON com convenções definidas.
5. Envio autenticado via OAuth2 e tratamento de erro de integração.

## Stack

- .NET 8
- C#
- xUnit
- FluentAssertions

## Arquitetura e organização

```text
BancoDoBrasil/
  Auth/                 # Autenticação OAuth2 e caching de token
  Configuration/        # Opções de configuração da integração
  Dtos/                 # Contratos de entrada e saída
  Exceptions/           # Exceções de validação e integração
  Formatting/           # Normalizadores e formatadores de campos
  Http/                 # Cliente HTTP autenticado da integração
  Serialization/        # Conversores JSON específicos
  Services/             # Serviços de domínio (registro de boleto)
  Validation/           # Validações estruturais, de negócio e payload
BancoDoBrasil.Tests/
  Builders/             # Builders de payload para cenários de teste
  Fakes/                # Dublês de infraestrutura HTTP
  Formatting/           # Testes unitários de formatação
  Services/             # Testes de contrato e composição do serviço
  Validation/           # Testes de validação
```

## Configuração de ambiente

Use o arquivo `.env.example` como referência para configuração local/CI.

Variáveis esperadas:

- `BB_API_BASE_URL`
- `BB_OAUTH_BASE_URL`
- `BB_CLIENT_ID`
- `BB_CLIENT_SECRET`
- `BB_DEVELOPER_APPLICATION_KEY`

## Execução local

### Pré-requisitos

- SDK do .NET 8 instalado.

### Restaurar e compilar

```bash
dotnet restore
dotnet build
```

### Executar testes

```bash
dotnet test
```

## Boas práticas adotadas

- Separação clara entre autenticação, transporte HTTP, serviço de domínio e validações.
- Exceções específicas para diferenciação entre falha de integração e falha de validação.
- Testes focados em contrato e comportamento de validação.
- Configuração externa de credenciais e endpoints.

## Segurança

- Não versionar credenciais reais ou endpoints internos.
- Utilizar segredos em variáveis de ambiente no pipeline de CI/CD.
- Revisar payloads de teste para evitar dados sensíveis antes de publicar.
