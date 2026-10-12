# Inscrição Estadual — refatoração de validação

## Escopo

- Algoritmos locais de formato e dígitos verificadores das **27 UFs**.
- Preserva `Validate.StateRegistration(object, BrazilianStates)` para consumidores existentes.
- Novas sobrecargas `string` e `ReadOnlySpan<char>`, sem alocação por validação no caminho tipado.
- `Span<char>` de 14 posições para normalizar uma vez; sem Reflection, LINQ, Regex, substrings ou dicionário de municípios no caminho de IE.
- Regras especiais: BA 8/9 dígitos e módulos 10/11, SP produtor rural `P`, DF `07`/`08`, GO exceção `11094402`, AP faixas especiais, MT 9/11, PE 9/14, RN 9/10 e TO 9/11.
- Todos os valores de `BrazilianStates` foram preservados, inclusive `ZZ = 99` como valor inválido.

## Notas de compatibilidade

A validação agora é **estrita quanto ao comprimento canônico** (salvo formatos alternativos explicitamente suportados) e ao conteúdo:
- Rejeita letras não reconhecidas em vez de apagar silenciosamente qualquer caractere.
- Rejeita `ISENTO` como número: isenção é um estado cadastral e deve ser modelada separadamente.
- Preserva zeros à esquerda em strings; não reconstrói zeros perdidos automaticamente, exceto para MT 9/11. **Essa é uma alteração de comportamento em relação ao código antigo, que preenchia diversos estados automaticamente.**
- Para SP rural, exige o `P` e os 12 algarismos restantes.
- Entradas `object` que já são `string` ou `char[]` seguem caminho tipado. Outros tipos podem precisar de conversão para string na camada compatível.
- `ReadOnlySpan<char>` em `netstandard2.0` exige `System.Memory` (adicionado ao projeto).

**Importante:** `true` indica somente consistência matemática/formato. Não confirma existência, titularidade, regularidade ou habilitação no SINTEGRA/SEFAZ.

## Exemplos

```csharp
bool sp = Validate.StateRegistration("110.042.490.114", BrazilianStates.SP);
bool rural = Validate.StateRegistration("P-01100424.3/002", BrazilianStates.SP);
bool tocantins = Validate.StateRegistration("29.031.998-6", BrazilianStates.TO);

ReadOnlySpan<char> number = "06.540.48-1".AsSpan();
bool rj = Validate.StateRegistration(number, BrazilianStates.RJ);
```

## Testes e validação

```powershell
dotnet test ./tests/BrazilSharp.Tests/BrazilSharp.Tests.csproj --configuration Release
```

Há vetores de referência para as 27 UFs, variantes excepcionais e casos negativos. Os 27 casos de referência foram também conferidos com uma implementação aritmética independente, fora do .NET; **ainda é necessário compilar e executar a suíte C#**.

Não foi executado nenhum GitHub Actions nem CI. O repositório tem um workflow em `.github/workflows/dotnet.yaml` que dispara em PR para `main`, então a branch foi preparada sem abrir PR.

## Fontes de regras e vetores

- [SINTEGRA — referência nacional](https://www.sintegra.gov.br/insc_est.html) (alerta de obsolescência da DLL)
- [SEFAZ-BA — cálculo de DV](https://www.sefaz.ba.gov.br/inspetoria-eletronica/icms/cadastro/calculo-dv/)
- [SEFAZ-PE — cálculo de DV](https://www.sefaz.pe.gov.br/Servicos/sintegra/Paginas/calculo-do-digito-verificador.aspx)
- [SEFAZ-PR — cálculo de DV](https://www.fazenda.pr.gov.br/Pagina/calculo-digito-verificador)
- [Economia GO — roteiro de crítica](https://goias.gov.br/economia/roteiro-de-critica-da-inscricao-estadual-de-goias/)
- [Índice de fontes por UF e vetores](https://github.com/open-data-brazil/br-validators/blob/main/docs/OFFICIAL-SOURCES.md)

**Nota para publicação:** confirmar detalhes ainda não cobertos por testes de fronteira específicos para cada SEFAZ, especialmente formatos históricos e inscrições tributárias excepcionais, e executar a suíte .NET antes de lançar nova versão.
