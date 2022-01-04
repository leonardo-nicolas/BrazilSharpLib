# Documentação de Brazil\#

## Prefácio
Com tantas informações que digitamos, alguma coisa sempre acabamos digitando errado, acidentalmente. E, quando se trata de documentos (por exemplo), não pode haver nenhuma informação diferente, como números de documentos errados (como número a mais ou faltando), palavras sem seus devidos acentos, etc. Pois isso pode causar inconsistência na hora de assinar um contrato e reconhecer firma em cartório (por exemplo).<br>
Pensando nisso, o autor pensou neste projeto e pôs em prática, com a principal intenção de disponibilizar portfólio em seu repositório do GitHub para ter o quê mostrar aos recrutadores e aproveitou também para disponibilizar ao público para importá-lo em seus projetos (Seja clonando repositório ou importando via pacote NuGET).<br>
Afinal, se puder com que o usuário final evite algum problema por erro de digitação, como enviar uma NF-e com o CNPJ da empresa emissora, inválido, por causa de 1 dígito a mais que saiu acidentalmente na pressa de digitar (ou até mesmo "esbarrou" acidentalmente na tecla em que não deveria ter esbarrado), por exemplo, é poder evitar piores transtornos futuros.<br>
Essa é a intenção! Além dessa, também terá mais métodos/funções (Além do método que calcula o 13º salário), para cálculos trabalhistas e ajudar aqueles que trabalha com Departamento Pessoal ou Recursos Humanos. Para isso, é só manter a biblioteca atualizada na última versão sempre, enquanto o projeto ainda estiver em desenvolvimento!


* * *

### Sumário

* [1 ⇒ Introdução](#main)
* [2 ⇒ Validação de Documentos:](#validate) \(Observação Importante: **NENHUM** desses métodos precisam de acesso a internet para realizar suas tarefas\)
    * [2.1 ⇒ CPF - Validar Cadastro de Pessoa Física](#validate_cpf)
    * [2.2 ⇒ CNPJ - Validar Cadastro Nacional de Pessoa Jurídica](#validate_cnpj)
    * [2.3 ⇒ DriverLicense - Validar CNH \(Carteira \(ou Carta\) Nacional de Habilitação\)](#validate_driverlicense)
    * [2.4 ⇒ VoterTitle - Validar Título de Eleitor](#validate_votertitle)
    * [2.5 ⇒ PIS - Validar NIS, o que inclui PIS e PASEP](#validate_pis)
    * [2.6 ⇒ RENAVAM - Validar Registro Nacional de Veículos Automotores](#validate_renavam)
    * [2.7 ⇒ StateRegistration - Validar IE \(Inscrição Estadual\)](#validate_stateregistration)
    * [2.8 ⇒ Email - Validar Endereço de correio eletrônico](#validate_email)
    * [2.9 ⇒ Numeric - Validar Qualquer número](#validate_numeric)
    * [2.10 ⇒ DateOrTime - Validar Data ou Hora](#validate_dateortime)
    * [Modos de uso e exemplos de código de validações](#validate_samplecode)

* [3 ⇒ Utilidades...](#utilities) \(Exemplos de código com cada um dos itens, desta categoria.\)
    * [3.1 ⇒ Descobrir o estado em que um título de eleitor foi emitido.](#utilities_findstatevotertitle)
    * [3.2 ⇒ Nome do estado por extenso](#utilities_fullstatename)
    * [3.3 ⇒ Código dos estados brasileiros definidos pelo TRE.](#utilities_brazilianstatestre)

* [4 ⇒ Cálculos](#calculations)
    * [4.1 ⇒ Cálculos Trabalhistas](#calculations_worker)
        * [4.1.1 ⇒ Thirteenth - Calcular um 13º proporcional](#calculations_worker_13th)
    * [Exemplos de códigos](#calculations_samples)

---

<a id="main"></a>

## 1 - INTRODUÇÃO

BrazilSharp é uma biblioteca do DotNet Standard 2.0, com as seguintes compatibilidades de biblioteca abaixo (segundo a documentação da Microsoft):

|Bibliotecas|Versões suportadas|
|:-------------|:------------------|
|.NET Framework|4.6.1*, 4.6.2, 4.7, 4.7.1, 4.7.2, 4.8 e as últimas versões.|
|.NET Core|2.0, 2.1, 2.2, 3.0 e 3.1.|
|.NET|5.0, 6.0 (Testes de unidade feitos nesta versão) e as últimas versões.|
|Mono|5.4, 6.4 e as últimas versões.|
|Plataforma universal do Windows|10.0.16299 e TBD, até as versões mais diante.|
|Xamarin.Android|8.0, 10.0 e as últimas versões.|
|Xamarin.iOS|10.14, 12.16, até as versões por diante.|
|Xamarin.Mac|À partir da versão 3.8 por diante|

\* Embora NuGet considere o .NET Framework 4.6.1 como suporte ao .NET Standard 1.5 a 2.0, há vários problemas com o consumo de bibliotecas .NET Standard que foram criadas para essas versões do .NET Framework 4.6.1. Para projetos que precisam usar essas bibliotecas no .NET Framework, recomendamos que você atualize o projeto para o .NET Framework 4.7.2 ou superior. <br>
Informação dita pela própria Microsoft.


Abaixo os _namespaces_, _classes_, _estruturas_, _interfaces_, _enum_, _delegates_, _métodos_, _propriedades_, _campos ou parâmetros_ e suas descrições:

* `BrazilSharp`: O _namespace_ principal.
    * `Validate`: Uma _classe_ estática com um conjunto de _métodos_ estáticos para validação de documentos ou outros dados. Mais abaixo os _métodos_ desta _classe_, modo de se usar e códigos de exemplo.
    * `Utilities`: Uma _classe_ estática com um conjunto de _métodos_ estáticos para ser utilizado em algumas situações diversas.
    * `BrazilianStates`: Um _enum_ com todos os estados de todo território brasileiro, cujo cada um de seus valores é o [código do estado IBGE](https://www.ibge.gov.br/explica/codigos-dos-municipios.php), conforme cada estado.
    * `Calculations`: _Namespace_ com um conjunto de _classes_ para realização de cálculos diversos.
        * `WorkersCalculation`: Uma _classe_ estática com um conjunto de _métodos_ estáticos para cálculos de Recursos Humanos (ou até de Departamento Pessoal).
        * `PeriodType`: Um _enum_ para auxiliação do _método_ `BrazilSharp.Calculations.WorkersCalculation`.***`Thirteenth`***, na hora de usá-lo para fazer cálculo de 13º proporcional.
    * `Exceptions`: O _namespace_ com todas as _classes_ de excessões (_classes_ herdadas de `System`.**`Exception`**).
        * `StateNotFoundException`: A _classe_ de erro que será usada por alguns _métodos_ em que o valor for diferente dos valores do _enum_ `BrazilSharp`.**`BrazilianStates`**.

***Atenção Importante***: Quaisquer dados sensíveis (documentos, e-mail, etc) aqui expostos, todos foram gerados aleatóriamente no site **[4devs](www.4devs.com.br)**.

---
<a id="validate"></a>

## 2 - Validações

Os métodos de validação de documentos e de outros dados, se encontram na _classe_ `Validate` (dentro do _namespace_ `BrazilSharp`) e são métodos estáticos.

Seu uso consiste apenas em importar o _namespace_ `BrazilSharp` no arquivo em que se usará os métodos (Ou utilizar o global using no caso do DotNet 6 com o C# 10, ambos por diante) e chamar pelo método desejado diretamente pela _classe_, sem instanciá-la; uma vez que a _classe_ `Validate` é uma _classe estática_.

<a id="validate_cpf"></a>

#### 2.1 - Validação de CPF
Este método é utilizado para validar se alguma informação qualquer no parâmetro, se é um CPF verdadeiro ou falso, através do dígito.

`BrazilSharp.Validate`.***`CPF`*** ⇒  valida qualquer informação/dado, pra determinar se é um CPF válido ou inválido.<br>
Este método retorna **`True`** ou **`False`**. Confira os _parâmetros_ deste método na tabela abaixo:

|Nome do parâmetro|Tipo|Descrição|
|:----|:----:|:-----|
|Expression|object|Alguma informação qualquer, podendo até ser arrays.|

<a id="validate_cnpj"></a>

#### 2.2 - Validação de CNPJ
Este método é utilizado para validar se alguma informação qualquer no parâmetro, se é um CNPJ verdadeiro ou falso.

`BrazilSharp.Validate`.***`CNPJ`*** ⇒ valida qualquer informação/dado, pra determinar se é um CNPJ válido ou inválido. <br>
Este método retorna **`True`** ou **`False`**. Confira os _parâmetros_ deste método na tabela abaixo:

|Nome do parâmetro|Tipo|Descrição|
|:----|:----:|:-----|
|Expression|object|Alguma informação qualquer, podendo até ser arrays.|

<a id="validate_driverlicense"></a>

#### 2.3 - Validação de CNH
Este método é utilizado para validar se alguma informação qualquer no parâmetro, se é uma CNH verdadeira ou não.

`BrazilSharp.Validate`.***`DriverLicense`*** ⇒ valida qualquer informação/dado, pra determinar se é uma CNH (Carteira Nacional de Habilitação, conhecida como carteira de motorista ou carta de motorista) válida ou não. <br>
Este método retorna **`True`** ou **`False`**. Confira os parâmetros deste método na tabela abaixo:

|Nome do parâmetro|Tipo|Descrição|
|:----|:----:|:-----|
|Expression|object|Alguma informação qualquer, podendo até ser arrays.|

<a id="validate_votertitle"></a>

#### 2.4 - Validação de Título de Eleitor
Este método é utilizado para validar se alguma informação qualquer no parâmetro, se o número do título de eleitor é válido ou inválido.

`BrazilSharp.Validate`.***`VoterTitle`*** ⇒ valida qualquer informação/dado, pra determinar se é um número de inscrição do título de eleitor válido ou inválido. <br>
Este método retorna **`True`** ou **`False`**. Confira os _parâmetros_ deste método na tabela abaixo:

|Nome do parâmetro|Tipo|Descrição|
|:----|:----:|:-----|
|Expression|object|Alguma informação qualquer, podendo até ser arrays.|

<a id="validate_pis"></a>

#### 2.5 - Validação do NIS (Número do PIS ou PASEP)
Este método é utilizado para validar se alguma informação qualquer no parâmetro, é um número de PIS ou PASEP, verdadeiro ou falso.

`BrazilSharp.Validate`.***`PIS`*** ⇒ valida qualquer informação/dado, pra determinar se é um número de PIS ou PASEP, válido ou inválido. <br>
Este método retorna **`True`** ou **`False`**. Confira os _parâmetros_ deste método na tabela abaixo:

|Nome do parâmetro|Tipo|Descrição|
|:----|:----:|:-----|
|Expression|object|Alguma informação qualquer, podendo até ser arrays.|

<a id="validate_renavam"></a>

#### 2.6 - Validação de número do RENAVAM
Este método é utilizado para validar se alguma informação qualquer no parâmetro, é um número de RENAVAM verdadeiro ou falso.

`BrazilSharp.Validate`.***`Renavam`*** ⇒ valida qualquer informação/dado, pra determinar se é um número de RENAVAM válido ou não. <br>
Este método retorna **`True`** ou **`False`**. Confira os _parâmetros_ deste método na tabela abaixo:

|Nome do parâmetro|Tipo|Descrição|
|:----|:----:|:-----|
|Expression|object|Alguma informação qualquer, podendo até ser arrays.|

<a id="validate_stateregistration"></a>

#### 2.7 - Validação de número de Inscrição Estadual
Este método é utilizado para validar se alguma informação qualquer no parâmetro, é uma inscrição estadual (do estado em que especificar no parâmetro) for verdadeiro ou falso.

`BrazilSharp.Validate`.***`StateRegistration`*** ⇒ valida qualquer informação/dado, para determinar se é uma inscrição estadual é válida ou inválida, de acordo com o estado informado no _parâmetro_ `state`.
Este método retorna **`True`** ou **`False`**. Confira os _parâmetros_ deste método na tabela abaixo:

|Nome do parâmetro|Tipo|Descrição|
|:----|:----:|:-----|
|Expression|object|Alguma informação qualquer, podendo até ser arrays.|
|state|BrazilSharp.BrazilianStates|Algum estado listado no _enum_...<br>ATENÇÃO: este parâmetro aceita somente uma única opção do referido _enum_, que é o tipo deste parâmetro!|

<a id="validate_email"></a>

#### 2.8 - Validação de e-mails
Este método é utilizado para validar se alguma informação qualquer no parâmetro, é um endereço de E-mail válido ou inválido.

`BrazilSharp.Validate`.***`Email`*** ⇒ valida qualquer informação/dado, converte-o em `string` e após a usa pra determinar se é um endereço de E-mail válido ou inválido.<br>
Este método retorna **`True`** ou **`False`**. Confira os _parâmetros_ deste método na tabela abaixo:

|Nome do parâmetro|Tipo|Descrição|
|:----|:----:|:-----|
|Expression|object|Alguma informação qualquer, podendo até ser arrays.|

<a id="validate_numeric"></a>

#### 2.9 - Validação de diversos numéros
Este método é utilizado para validar se alguma informação qualquer no parâmetro, é de fato um número qualquer ou não.<br>
Este método é familiar pra quem programa em VBA ou VB.NET. Só que com a diferenda de que, funciona em qualquer plataforma que rode o DotNET (Seja pelo .NET, .NET Framework, .NET Core, Xamarin, Mono, etc).<br>
    
    Para quem tiver problemas com certos métodos da biblioteca do Visual Basic no C# ou no F# e rodar em plataformas não-windows (ou simplismente não conseguir importar as bibliotecas no projeto em que estiver trabalhando), talvez esta biblioteca poderia ser uma alternativa. Em breve (nas próximas atualizações), surgirão os métodos para cálculos, etc.

`BrazilSharp.Validate`.***`Numeric`*** ⇒ valida qualquer informação/dado, pra determinar se é de fato um número qualquer ou não.<br>
Inclusive até números de diversas bases (Até da base 16, o Hexadecimal) e de diversas linguagens, etc.<br>
Este método retorna **`True`** ou **`False`**. Confira os _parâmetros_ deste método na tabela abaixo:

|Nome do parâmetro|Tipo|Descrição|
|:----|:----:|:-----|
|Expression|object|Alguma informação qualquer, podendo até ser arrays.|

<a id="validate_dateortime"></a>

#### 2.10 - Validação de Datas ou Horas
Este método é utilizado para validar se alguma informação passada no parâmetro, é data/hora ou não.<br>
Este método é familiar pra quem programa em VBA ou VB.NET. Só que com a diferenda de que, funciona em qualquer plataforma que rode o DotNET (Seja pelo .NET, .NET Framework, .NET Core, Xamarin, Mono, etc).<br>

    Para quem tiver problemas com certos métodos da biblioteca do Visual Basic no C# ou no F# e rodar em plataformas não-windows (ou simplismente não conseguir importar as bibliotecas no projeto em que estiver trabalhando), talvez esta biblioteca poderia ser uma alternativa. Em breve (nas próximas atualizações), surgirão os métodos para cálculos, etc.

`BrazilSharp.Validate`.***`DateOrTime`*** ⇒ valida qualquer informação/dado, pra determinar se é um valor válido ou então um objeto de data/hora ou não.<br>
Este método retorna **`True`** ou **`False`**. Confira os _parâmetros_ deste método na tabela abaixo:

|Nome do parâmetro|Tipo|Descrição|
|:----|:----:|:-----|
|Expression|object|Alguma informação qualquer, podendo até ser arrays.|

<a id="validate_samplecode"></a>

### Exemplos de código de validação de documentos, em linguagens diferentes do DotNET.
OBSERVAÇÃO: Estes exemplos de código abaixo demonstra como é seu uso...

```C#
// OBS.: Quaisquer dados aqui expostos, foram gerados aleatóriamente no site 4devs.

// Modo de como se usa a validação de Inscrição Estadual com C#
// O primeiro parâmetro (Expression) aceita qualquer argumento de qualquer tipagem (Não existe tipagem específica).
bool isStateRegistrateValid = BrazilSharp.Validate.StateRegistration(new int[]{34,792,62,3}, BrazilSharp.BrazilianStates.RJ);

// Modo de como se usa outro método qualquer de validação, com um único parâmetro chamado 'Expression' do tipo 'object':
bool isValidCnh = BrazilSharp.Validate.DriverLicense(92366290549)
bool isValidCpf = BrazilSharp.Validate.CPF(new object[]{0b11111010,'.',135,'.',0x2E4,'-',"05"});
```
```VB
'OBS.: Quaisquer dados aqui expostos, foram gerados aleatóriamente no site 4devs.

' Modo de como se usa a validação de Inscrição Estadual com Visual Basic .NET
Dim isStateRegistrationValid As Boolean = BrazilSharp.Validate.StateRegistration("178.326.260/1360", BrazilSharp.BrazilianStates.MG)

' Modo de como se usa outro método qualquer de validação, com um único parâmetro chamado 'Expression' do tipo 'Object':
Dim isValidEmail As Boolean = BrazilSharp.Validate.Email("teste.em.massa@senhordostestes.com")
Dim isNumeric As Boolean = BrazilSharp.Validate.Numeric("R$ 550.228.825,49");
```
OBSERVAÇÃO: Este código abaixo é apenas uma demonstração, um exemplo de um programinha em que o usuário escolhe qual documento ou algum dado em que deseja validar...
```C#
// Demonstração usando C#
using System;
using BrazilSharp;

namespace  ConsoleApp1 
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Selecione apenas UMA das opções abaixo e em seguida pressione enter...");
            Console.WriteLine("1 - Validar CPF");
            Console.WriteLine("2 - Validar CNPJ");
            Console.WriteLine("3 - Validar CNH");
            Console.WriteLine("4 - Validar Título de Eleitor");
            Console.WriteLine("5 - Validar PIS/PASEP");
            Console.WriteLine("6 - Validar RENAVAM");
            Console.WriteLine("7 - Validar Inscrição Estadual");
            Console.WriteLine("8 - Validar E-Mail");
            Console.WriteLine("9 - Validar Data/Hora");
            Console.WriteLine("10 - Validar Números");
            string strOptionChoosed = Console.ReadLine();
            int optChoosed = Convert.ToInt32(strOptionChoosed);
            Console.WriteLine("Agora digite alguma informação para validar...");
            string documentOrData = Console.ReadLine();
            bool isDocumentValid;
            string typeOfDocument;
            switch(optChoosed)
            {
                case 1: // O usuário escolheu validar CPF
                    isDocumentValid = Validate.CPF(documentOrData);
                    typeOfDocument = "CPF (Cadastro de Pessoa Física)";
                    break;
                case 2: // O usuário escolheu validar CNPJ
                    isDocumentValid = Validate.CNPJ(documentOrData);
                    typeOfDocument = "CNPJ (Cadastro Nacional de Pessoa Jurídica)";
                    break;
                case 3: // O usuário escolheu validar CNH
                    isDocumentValid = Validate.DriverLicense(documentOrData);
                    typeOfDocument = "CNH (Carteira (ou carta) Nacional de Habilitação)";
                    break;
                case 4: // O usuário escolheu validar Título de Eleitor
                    isDocumentValid = Validate.VoterTitle(documentOrData);
                    typeOfDocument = "Título de Eleitor";
                    break;
                case 5: // O usuário escolheu validar PIS/PASEP
                    isDocumentValid = Validate.PIS(documentOrData);
                    typeOfDocument = "NIS (Número do PIS/PASEP)";
                    break;
                case 6: // O usuário escolheu validar RENAVAM
                    isDocumentValid = Validate.Renavam(documentOrData);
                    typeOfDocument = "RENAVAM (Registro Nacional de Veículos Automotores";
                    break;
                case 7: // O usuário escolheu validar Inscrição Estadual
                    Console.WriteLine("Agora informe a sigla do estado em que deseja validar esta Inscrição Estadual");
                    string sigla = Console.ReadLine();
                    BrazilianStates state = Enum.Parse(typeof(BrazilianStates),sigla,true);
                    isDocumentValid = Validate.StateRegistration(documentOrData, state);
                    typeOfDocument = "IE (Inscrição Estadual)";
                    break;
                case 8: // O usuário escolheu validar E-mail
                    isDocumentValid = Validate.Email(documentOrData);
                    typeOfDocument = "e-mail";
                    break;
                case 9: // O usuário escolheu validar Data ou Hora
                    isDocumentValid = Validate.DateOrTime(documentOrData);
                    typeOfDocument = "Data ou Hora";
                    break;
                case 10: // O usuário escolheu validar Números
                    isDocumentValid = Validate.Numeric(documentOrData);
                    typeOfDocument = "Número";
                    break;
                default: // O usuário escolheu uma opção inválida ou nenhuma opção
                    documentOrData = "???";
                    typeOfDocument = "Desconhecido";
                    isDocumentValid = false;
                    break;
            }
            
            if (isDocumentValid)
            {
                Console.WriteLine("OK, passou no teste! Este(a) {0} é Válido(a)!", typeOfDocument);
            }
            else if (!isDocumentValid && typeOfDocument == "Desconhecido")
            {
                Console.WriteLine("Você informou a opção errada, tente novamente!");
            }
            else
            {
                Console.WriteLine("{0} {1}, não passou no teste! Este(a) {0} é inválido(a)!", typeOfDocument, documentOrData);
            }
        }
    }
}

```
```VB
' Demonstração usando o Visual Basic DotNet
' Geralmente não há necessidade do 'Imports System' em VB.NET,
' pois o compilador importa automaticamente.
Imports BrazilSharp

Module Program
    Public Sub Main(ByVal args As String())
        Console.WriteLine("Selecione apenas UMA das opções abaixo e em seguida pressione enter...")
        Console.WriteLine("1 - Validar CPF")
        Console.WriteLine("2 - Validar CNPJ")
        Console.WriteLine("3 - Validar CNH")
        Console.WriteLine("4 - Validar Título de Eleitor")
        Console.WriteLine("5 - Validar PIS/PASEP")
        Console.WriteLine("6 - Validar RENAVAM")
        Console.WriteLine("7 - Validar Inscrição Estadual")
        Console.WriteLine("8 - Validar E-Mail")
        Console.WriteLine("9 - Validar Data/Hora")
        Console.WriteLine("10 - Validar Números")
        Dim strOptionChoosed As String = Console.ReadLine()
        Dim optChoosed As Integer = Convert.ToInt32(strOptionChoosed)
        Console.WriteLine("Agora digite alguma informação para validar...")
        Dim documentOrData AS String = Console.ReadLine()
        Dim isDocumentValid As Boolean
        Dim typeOfDocument As String

        Select Case optChoosed
            Case 1 ' O usuário escolheu validar CPF
                isDocumentValid = Validate.CPF(documentOrData)
                typeOfDocument = "CPF (Cadastro de Pessoa Física)"
            Case 2 ' O usuário escolheu validar CNPJ
                isDocumentValid = Validate.CNPJ(documentOrData)
                typeOfDocument = "CNPJ (Cadastro Nacional de Pessoa Jurídica)"
            Case 3 ' O usuário escolheu validar CNH
                isDocumentValid = Validate.DriverLicense(documentOrData)
                typeOfDocument = "CNH (Carteira (ou carta) Nacional de Habilitação)"
            Case 4 ' O usuário escolheu validar Título de Eleitor
                isDocumentValid = Validate.VoterTitle(documentOrData)
                typeOfDocument = "Título de Eleitor"
            Case 5 ' O usuário escolheu validar PIS/PASEP
                isDocumentValid = Validate.PIS(documentOrData)
                typeOfDocument = "NIS (Número do PIS/PASEP)"
            Case 6 ' O usuário escolheu validar RENAVAM
                isDocumentValid = Validate.Renavam(documentOrData)
                typeOfDocument = "RENAVAM (Registro Nacional de Veículos Automotores"
            Case 7 ' O usuário escolheu validar Inscrição Estadual
                Console.WriteLine("Agora informe a sigla do estado em que deseja validar esta Inscrição Estadual")
                Dim sigla As String = Console.ReadLine
                Dim state As BrazilianStates = [Enum].Parse(GetType(BrazilianStates), sigla, True)
                isDocumentValid = Validate.StateRegistration(documentOrData, state)
                typeOfDocument = "IE (Inscrição Estadual)"
            Case 8 ' O usuário escolheu validar E-mail
                isDocumentValid = Validate.Email(documentOrData)
                typeOfDocument = "e-mail"
            Case 9 ' O usuário escolheu validar Data ou Hora
                isDocumentValid = Validate.DateOrTime(documentOrData)
                typeOfDocument = "Data ou Hora"
            Case 10 ' O usuário escolheu validar Números
                isDocumentValid = Validate.Numeric(documentOrData)
                typeOfDocument = "Número"
            Case 0 Or Else ' O usuário escolheu uma opção inválida ou nenhuma opção
                documentOrData = "???"
                typeOfDocument = "Desconhecido"
                isDocumentValid = False
        End Select
        
        If isDocumentValid Then
            Console.WriteLine("OK, passou no teste! Este(a) {0} é Válido!", typeOfDocument)
        ElseIf Not isDocumentValid AndAlso typeOfDocument = "Desconhecido" Then
            Console.WriteLine("Você informou a opção errada, tente novamente!")
        Else
            Console.WriteLine("{0} {1}, não passou no teste! Este(a) {0} é inválido(a)!", typeOfDocument, documentOrData)
        End If
    End Sub
End Module
```

---
<a id="utilities"></a>

## 3 - Utilidades
Os _métodos_ de utilidades (como descobrir o estado em que um título de eleitor foi emitido), se encontram na _classe_ `Utilities` (dentro do _namespace_ `BrazilSharp`) e são _métodos_ estáticos.<br>
Seu uso consiste apenas em importar o _namespace_ `BrazilSharp` no arquivo em que se usará os _métodos_ (Ou utilizar o global using no caso do DotNet 6 com o C# 10, ambos por diante) e chamar pelo método desejado diretamente pela _classe_, sem instanciá-la; uma vez que a _classe_ `Utilities` é uma _classe_ estática.

<a id="utilities_findstatevotertitle"></a>

#### 3.1 - Descobrir o estado em que o título de eleitor foi emitido
Este método é utilizado para "revelar" o estado em que um título de eleitor foi emitido...

`BrazilSharp.Utilities`.***`FindStateVoterTitle`*** ⇒ valida qualquer dado informado, pra determinar se é um número de inscrição do título de eleitor válido ou não.

Este método retorna algum valor do _enum_ `BrazilSharp`.**`BrazilianStates`**, se o título de eleitor informado for verdadeiro. Caso contrário, retorna **`null`** (Ou **`Nothing`** em Visual Basic). Confira os _parâmetros_ deste método na tabela abaixo:

|Nome do parâmetro|Tipo|Descrição|
|:----|:----:|:-----|
|Expression|object|Alguma informação qualquer, podendo até ser arrays.|

ATENÇÃO IMPORTANTE: Estes números de título de eleitor expostos, foram gerados aleatóriamente no site 4devs.
```C#
// Exemplo uso em C#
BazilSharp.BrazilianStates? issuerState = BrazilSharp.Utilities.FindStateVoterTitle(552667570396);
if(issuerState != null && issuerState != BrazilSharp.BrazilianStates.ZZ)
    Console.WriteLine("Este título de eleitor foi emitido no(a) {0} - {1}", issuerState, issuerState.Value.GetFullName());
    // O Método (de extensão) GetGullName() será abordado no próximo tópico.
else if(issuerState != null && issuerState == BrazilSharp.BrazilianStates.ZZ)
    Console.WriteLine("Este título de eleitor foi emitido no exterior, mais provável no consulado do Brasil, em algum país...");
else
    Console.WriteLine("Título de eleitor inválido!");
```
```VB
' Exemplo uso em Visual Basic .NET
Dim issuerState As BazilSharp.BrazilianStates? = BrazilSharp.Utilities.FindStateVoterTitle("586612712780")
If issuerState <> Nothing AndAlso issuerState <> BrazilSharp.BrazilianStates.ZZ Then
    Console.WriteLine("Este título de eleitor foi emitido no(a) {0} - {1}", issuerState, issuerState.Value.GetFullName)
    ' O Método (de extensão) GetGullName() será abordado no próximo tópico.
ElseIf issuerState <> Nothing AndAlso issuerState = BrazilSharp.BrazilianStates.ZZ Then
    Console.WriteLine("Este título de eleitor foi emitido no exterior, mais provável no consulado do Brasil, em algum país...")
Else
    Console.WriteLine("Título de eleitor inválido!")
End If
```


<a id="utilities_fullstatename"></a>

#### 3.2 - Nome do estado por extenso
Este é um _metodo_ de extensão em que é usado em conjunto com o _enum_ `BrazilSharp`.**`BrazilianStates`**, onde, ao chamar pelo método, ele retornará uma string com o nome do estado em que equivale àquela instância do enum...

```C#
// Exemplo em C#, para monstrar como é o uso
BrazilSharp.BrazilianStates stateWhereAuthorLives = BrazilSharp.BrazilianStates.RJ;
string fullStateName = stateWhereAuthorLives.GetFullName();
System.Console.WriteLine($"O autor deste projeto mora no estado do {fullStateName}, conhecida como cidade maravilhosa, mas na verdade é cidade perigosa. kkkk.");
```
```VB
' Exemplo de código em Visual Basic, para monstrar como é o uso
Dim stateWhereAuthorLives As BrazilSharp.BrazilianStates = BrazilSharp.BrazilianStates.RJ
Dim fullStateName As String = stateWhereAuthorLives.GetFullName()
Console.WriteLine($"O autor deste projeto mora no estado do {fullStateName}, conhecida como cidade maravilhosa, mas na verdade é cidade perigosa. kkkk.")
```

<a id="utilities_brazilianstatestre"></a>

#### 3.3 - Código dos estados brasileiros definidos pelo TRE, cujo pode ser encontrado também no título de eleitor.
Este campo contém todos os estados brasileiros, cada um com seus respectivos códigos de identificação do estado emissor, definido pelo TRE.<br>
Aqui encontra-se um dos "segredos" do título de eleitor. Este campo é mais para fins de curiosidade, mas pode ser usado para outras coisas também, conforme a necessidade.

C# `System.Collections.Generic`.**`IReadOnlyDictionary`**_`<`_`BrazilSharp`.**`BrazilianStates`**,**`short`**_`>`_`BrazilSharp.Utilities`.***`TREBrazilianStatesCode`*** 

VB `BrazilSharp.Utilities`.***`TREBrazilianStatesCode`*****`As`**`System.Collections.Generic`.**`IReadOnlyDictionary`**`(`**`Of`**`BrazilSharp`.**`BrazilianStates`**,**`Short`**`)`

Este campo é somente leitura. Ele contém todos os estados brasileiros, cada um com seu código definido pelo TRE.

```C#
// Exemplo de código em C#
using System;
using BrazilSharp;

namespace ConsoleApp1 
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Informe uma sigla de um estado para saber o código definido pelo TRE...");
            Console.WriteLine("Se caso for fora do Brasil, informe 'ZZ'.");
            string argument = Console.ReadLine();
            BrazilianStates gotState = Enum.Parse(typeof(BrazilianStates), argument, true);
            short codeStateFromTRE = Utilities.TREBrazilianStatesCode[gotState];
            
            Console.WriteLine("O código do estado {0} definido pelo TRE é {1} e pelo IBGE é {2}", argument, codeStateFromTRE, gotState);
        }
    }
}
```
```VB
' Exemplo de código em Visual Basic
Imports BrazilSharp

Module Program
    Public Sub Main(ByVal args As String())
        Console.WriteLine("Informe uma sigla de um estado para saber o código definido pelo TRE...")
        Console.WriteLine("Se caso for fora do Brasil, informe 'ZZ'.")
        string argument = Console.ReadLine()
        Dim gotState As BrazilianStates = [Enum].Parse(GetType(BrazilianStates), argument, True)
        Dim codeStateFromTRE As Short = Utilities.TREBrazilianStatesCode(gotState)
        
        Console.WriteLine("O código do estado {0} definido pelo TRE é {1} e pelo IBGE é {2}", argument, codeStateFromTRE, gotState)
    End Sub
End Module
```

---
<a id="calculations"></a>

## 4 - Cálculos
Conjunto de classes, estruturas, métodos, etc, para diversos tipos de cálculos, como cálculos de Departamento Pessoal (No momento disponível somente 13º, mas nas próximas atualizações terá os diversos tipos de cálculos), financeiros, etc. 
###### Caso tenha mais alguma sugestão de cálculo, abra uma Issue ou faça um Pull Request.

<a id="calculations_worker"></a>

#### 4.1 - Cálculos de Departamento Pessoal ou Recursos Humanos
Conjunto de métodos estáticos para cálculos utilizados no Departamento Pessoal ou até mesmo nos Recursos Humanos. Como Férias (Ainda EM BREVE), 13º proporcional, etc.

Seu uso consiste apenas em importar o _namespace_ `BrazilSharp.Calculations` no arquivo em que se usará os métodos (Ou utilizar o global using no caso do DotNet 6 com o C# 10, ambos por diante) e chamar pelo método desejado diretamente pela classe, sem instanciá-la; uma vez que a _classe_ **`WorkersCalculation`** é uma classe estática.

<a id="calculations_worker_13th"></a>

##### 4.1.1 - Cálculo de 13º
Este método é utilizado para fazer cálculo de 13º proporcional, com base nas informações necessárias para ser feito o cálculo.

`BrazilSharp.Calculations.WorkersCalculation`.***`Thirteenth`*** ⇒ Calcula o 13º proporcional de um empregado. Este método retorna um Double, já com o 13º calculado.<br>
Abaixo a tabela com os detalhes e instruções dos _parâmetros_ deste método:

|Nome do parâmetro|Tipagem do parâmetro|Descrição|
|:----|:----:|:-----|
|calculationBasis|double|A base de cálculo para o 13º...|
|EarlyWarning|bool|Defina true se o empregado estiver de aviso prévio. Se não for o caso, false!|
|Regime|BrazilSharp.Calculations.PeriodType|O calculo será feito em cima de DIAS ou MESES?<br>Informe `BrazilSharp.Calculations.PeriodType.Day` para calcular em DIAS<br>Ou, Informe `BrazilSharp.Calculations.PeriodType.Month` para calcular em MESES|
|HowLongWorked|short|Se o calculo for feito em cima de **DIAS**, **informe entre 1 e 31**, quantos dias o empregado trabalhou! <br> Se o calculo for feito em cima de **MESES**, **informe entre 1 e 12**, quantos meses o empredado trabalhou!|

Caso esteja fora do intervalo citado acima, um erro do tipo `System`.**`ArgumentOutOfRangeException`** será lançado!

<a id="calculations_samples"></a>

## Exemplos de código de diversos tipos de cálculos, em linguagens diferentes do DotNET.
OBSERVAÇÃO: Estes códigos abaixo são apenas para fins de demonstração. Um pequeno programinha para o usuário interagir e fazer algum cálculo...
```C#
// Exemplo de código demonstrativo em C#
using System;
using BrazilSharp;
using BrazilSharp.Calculations;

namespace  ConsoleApp1 
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Escolha uma das opções abaixo e em seguida pressione ENTER:")
            Console.WriteLine("1 - Calcular 13o")
            Console.WriteLine("0 - Sair")
            string strOptionChoosed = Console.ReadLine();
            int optChoosed = Convert.ToInt32(strOptionChoosed);
            switch(optChoosed)
            {
                case 1:
                    Calculate13th();
                    break;
                case 0:
                default:
                    Console.WriteLine("Até logo...");
                    Environment.Exit(0);
                    break;
            }
        }

        private static void Calculate13th()
        {
            Console.WriteLine("Vamos Calcular o 13º Proporcional");

            Console.WriteLine("Digite o nome do empregado");
            string employeeName = Console.ReadLine();
            
            Console.WriteLine("Informe o salário + adicional noturno + insalubridade + outros ganhos previstos em lei...");
            string strCalcBase = Console.ReadLine();
            double calcBase = Convert.ToDouble(strCalcBase);
            
            Console.WriteLine("Aviso Prévio? Tecle 'S' ou 'N'.");
            char respnseEarlyWarning = Console.ReadKey();
            bool avisoPrevio = respnseEarlyWarning == 'S'||respnseEarlyWarning == 's';

            Console.WriteLine("Informe 'Day' se o cálculo será feito EM DIAS:");
            Console.WriteLine("OU");
            Console.WriteLine("Informe 'Month' se o calculo será feito EM MESES:");
            string responseCalcRegime = Console.ReadLine();
            PeriodType calculationRegime = Enum.Parse(typeof(PeriodType), responseCalcRegime, false);

            Console.WriteLine("Quantos \"{0}'s\" que {1} trabalhou?", responseCalcRegime, employeeName);
            string strPeriodWorked = Console.ReadLine();
            short periodWorked = Convert.ToInt16(strPeriodWorked);

            double ResultadoDoCaculo = WorkersCalculation.Thirteenth(calcBase, avisoPrevio, calculationRegime, periodWorked);
            
            Console.WriteLine("O décimo terceiro de {0} é de {1}.", employeeName, ResultadoDoCalculo);
            Console.WriteLine("Se somar ao salário e o restante da base de cálculo, vai dar {0}", employeeName);
            Console.WriteLine("Parabéns {0}! Tá com muita da grana em!!!", employeeName);
            Console.WriteLine("Me dá uma garoupa ou um lobo-guará? Brincadeira! :D :D");
        }
    }
}
```
```VB
' Exemplo de código em Visual Basic
Imports BrazilSharp;
Imports BrazilSharp.Calculations;

Module Program
    Public Sub Main(ByVal args As String())
        Console.WriteLine("Escolha uma das opções abaixo e em seguida pressione ENTER:")
        Console.WriteLine("1 - Calcular 13o")
        Console.WriteLine("0 - Sair")
        Dim strOptionChoosed As String = Console.ReadLine
        Dim optChoosed As Integer = Convert.ToInt32(strOptionChoosed)
        Select Case optChoosed
            case 1
                Calculate13th()
            Case 0 Or Else
                Console.WriteLine("Até logo...")
                Environment.Exit(0)
        End Select
    End Sub

    Public Sub Calculate13th()
        Console.WriteLine("Vamos Calcular o 13º Proporcional")

        Console.WriteLine("Digite o nome do empregado")
        Dim employeeName As String = Console.ReadLine
        
        Console.WriteLine("Informe o salário + adicional noturno + insalubridade + outros ganhos previstos em lei...")
        Dim strCalcBase As String = Console.ReadLine
        Dim calcBase As Double = Convert.ToDouble(strCalcBase)
        
        Console.WriteLine("Aviso Prévio? Tecle 'S' ou 'N'.")
        Dim respnseEarlyWarning As Char = Console.ReadKey
        Dim avisoPrevio As Boolean = respnseEarlyWarning = "S"c Or respnseEarlyWarning = "s"c

        Console.WriteLine("Informe 'Day' se o cálculo será feito EM DIAS:")
        Console.WriteLine("OU")
        Console.WriteLine("Informe 'Month' se o calculo será feito EM MESES:")
        Dim responseCalcRegime As String = Console.ReadLine
        Dim calculationRegime As PeriodType = [Enum].Parse(GetType(PeriodType), responseCalcRegime, False)

        Console.WriteLine("Quantos ""{0}'s"" que {1} trabalhou?", responseCalcRegime, employeeName)
        Dim strPeriodWorked As String = Console.ReadLine
        Dim periodWorked As Short = Convert.ToInt16(strPeriodWorked)

        Dim ResultadoDoCaculo As Double = WorkersCalculation.Thirteenth(calcBase, avisoPrevio, calculationRegime, periodWorked)
        
        Console.WriteLine("O décimo terceiro de {0} é de {1}.", employeeName, ResultadoDoCalculo)
        Console.WriteLine("Se somar ao salário e o restante da base de cálculo, vai dar {0}", employeeName)
        Console.WriteLine("Parabéns {0}! Tá com muita da grana em!!!", employeeName)
        Console.WriteLine("Me dá uma garoupa ou um lobo-guará? Brincadeira! :D :D")
    End Sub
End Module

```