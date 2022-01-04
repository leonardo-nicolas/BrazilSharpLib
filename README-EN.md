# Documentation of Brazil\#


## Foreword
With so much information we typed, something we always end up typing wrong, accidentally. And when it comes to documents (for example), there can be no different information, such as wrong document numbers (such as too many numbers or missing), words without their proper accents, etc. Because this can cause inconsistency when signing a contract and recognizing a notary firm (for example).<br>
Thinking about it, the author thought about this project and put it into practice, with the main intention of making available portfolio in his GitHub repository to have what to show to recruiters and also took the opportunity to make it available to the public to import it in their projects (Whether cloning repository or importing via NuGET package).<br>
After all, if you can with the end user avoid some problem by typo, such as sending an NF-e with the CNPJ of the issuing company, invalid, because of 1 digit more that accidentally left in the rush to type (or even accidentally "bumped" the key in which it should not have bumped), for example, is to be able to avoid worse future disorders.<br>
That's the point! In addition to this, it will also have more methods /functions (In addition to the method that calculates the 13th salary), for labor calculations and help those who work with Personal Department or Human Resources. For this, just keep the library updated in the latest version always, while the project is still under development!

* * *

### Summary
* [1 ⇒ Introduction](#main)
* [2 ⇒ Document Validation:](#Validate) \(Important Note: NONE of these methods require internet access to accomplish their tasks\)
    * [2.1 ⇒ CPF - Validate Individual Registration](#validate_cpf)
    * [2.2 ⇒ CNPJ - Validate Brazilian Registry of Legal Entity](#validate_cnpj)
    * [2.3 ⇒ DriverLicense - Validate CNH (Brazilian Driver's License)](#validate_driverlicense)
    * [2.4 ⇒ VoterTitle - Validate Voter Title](#validate_votertitle)
    * [2.5 ⇒ PIS - Validate NIS, which includes PIS and PASEP](#validate_pis)
    * [2.6 ⇒ RENAVAM - Validate Brazilian Registration of Motor Vehicles](#validate_renavam)
    * [2.7 ⇒ StateRegistration - Validate IE (State Registration)](#validate_stateregistration)
    * [2.8 ⇒ Email - Validate Email Address](#validate_email)
    * [2.9 ⇒ Numeric - Validate Any Number](#validate_numeric)
    * [2.10 ⇒ DateOrTime - Validate Date or Time](#validate_dateortime)
    * [Usage modes and validation code examples](#validate_samplecode)
* [3 ⇒ Utilities...](#utilities) \(Code examples with each of the items in this category.\)

    * [3.1 ⇒ Find out the state in which a voter id was issued.](#utilities_findstatevotertitle)
    * [3.2 ⇒ State Name in full](#utilities_fullstatename)
    * [3.3 ⇒ Code of Brazilian states defined by the TRE.](#utilities_brazilianstatestre)
* [4 ⇒ Calculations](#calculations)
    * [4.1 ⇒ Labor Calculations](#calculations_worker)
        * [4.1.1 ⇒ Thirteenth - Calculate a 13th proportional](#calculations_worker_13th)
    * [Samples code](#calculations_samples)

---


<a id="main"></a>

## 1 - INTRODUCTION
BrazilSharp is a DotNet Standard 2.0 library, with the following library compatibilities below (according to Microsoft documentation):

|Libraries|Supported versions|
|:-------------|:------------------|
|.NET Framework|4.6.1*, 4.6.2, 4.7, 4.7.1, 4.7.2, 4.8 and the latest versions.|
|.NET Core|2.0, 2.1, 2.2, 3.0 and 3.1.|
|.NET|5.0, 6.0 (Unit tests done in this version) and the latest versions.|
|Mono|5.4, 6.4 and the latest versions.|
|Universal Windows Platform|10.0.16299 and TBD, up to the latest versions.|
|Xamarin.Android|8.0, 10.0 and the latest versions.|
|Xamarin.iOS|10.14, 12.16, until versions on.|
|Xamarin.Mac|From version 3.8 onwards|
* Although NuGet considers .NET Framework 4.6.1 to support .NET Standard 1.5 through 2.0, there are several issues with the consumption of .NET Standard libraries that were created for these versions of .NET Framework 4.6.1. For projects that need to use these libraries in the .NET Framework, we recommend that you update the project to .NET Framework 4.7.2 or higher.<br>
Information dictated by Microsoft itself.

Below are the _namespaces_, _classes_, _structures_, _interfaces_, _enum_, _delegates_, _methods_, _properties_, _fields or parameters_ and their descriptions:

* `BrazilSharp`: The main namespace.
    * `Validate`: A static _class_ with a set of static _methods_ for validating documents or other data. Below are the _methods_ of this _class_, how to use and sample codes.
    * `Utilities`: A static _class_ with a set of static _methods_ to be used in some diverse situations.
    * `BrazilianStates`: An _enum_ with all states of all Brazilian territory, whose each of its values is the [IBGE state code](https://www.ibge.gov.br/explica/codigos-dos-municipios.php) \(This link is in brazilian portuguese and, there isn't support for english language at the moment. At least until this README-EN.md was published.\),according to each state.
    * `Calculations`: _Namespace_ with a set of _classes_ for performing various calculations.
    * `WorkersCalculation`: A static _class_ with a set of static _methods_ for Human Resources (or even Personnel Department) calculations.
    * `PeriodType`: An _enum_ to help the `BrazilSharp.Calculations.WorkersCalculation`.***`Thirteenth`*** _method_, when using it to calculate the proportional 13th.
    * `Exceptions`: The _namespace_ with all exception _classes_ (inherited classes from `System`.**`Exception`**).
        * `StateNotFoundException`: The error `class` that will be used by some _methods_ where the value is different from the values of the `BrazilSharp`.**`BrazilianStates`**.

***Important Warning***: Any sensitive data (documents, email, etc.) exposed here, all were generated randomly by [4devs](www.4devs.com.br).


---

<a id="validate"></a>

## 2 - Validations
Document and other data validation _methods_ are found in the `Validate` static `class` (From `BrazilSharp`_namespace_).

Its use consists only of importing the `BrazilSharp` _namespace_ into the file in which the methods will be used (or using the **global using** in the case of DotNet 6 with C# 10, both onwards) and calling by the method desired directly bythe class, without instantiating it; since the _class_ is a static class.


<a id="validate_cpf"></a>

#### 2.1 - CPF Validation
This method is used to validate whether any information in the parameter, whether it is a true or false CPF, through the digit.

`BrazilSharp.Validate`.***`CPF`*** ⇒ validates any data to check whether it is a valid or invalid CPF.
This _method_ returns **`True`** or **`False`**. Check out the parameters of this method in the table below:

|Parameter name | Data Type | Description |
|:----|:----:|:-----|
| Expression | object | Some information whatsoever, and it may even be arrays.|

<a id="validate_cnpj"></a>

#### 2.2 - CNPJ Validation
This method is used to validate whether any information in the parameter, whether it is a true or false CNPJ.

`BrazilSharp.Validate`.***`CNPJ`*** ⇒ validates any data to check whether it is a valid or invalid CNPJ.
This _method_ returns **`True`** or **`False`**. Check out the parameters of this _method_ in the table below:

|Parameter name | Data Type | Description |
|:----|:----:|:-----|
| Expression | object | Some information whatsoever, and it may even be arrays.|

<a id="validate_driverlicense"></a>

#### 2.3 - CNH validation
This method is used to validate whether any information in the parameter, whether it is a true CNH or not.

`BrazilSharp.Validate`.***`DriverLicense`*** ⇒ validates any data to check whether it is a valid Brazilian driver's license (CNH, that known as "Carteira Nacional de Habilitação").
This _method_ returns **`True`** or **`False`**. Check out the parameters of this _method_ in the table below:

|Parameter name | Data Type | Description |
|:----|:----:|:-----|
| Expression | object | Some information whatsoever, and it may even be arrays.|

<a id="validate_votertitle"></a>

#### 2.4 - Voter Title Validation
This method is used to validate whether any information in the parameter, whether the voter title number is valid or invalid.

`BrazilSharp.Validate`.***`VoterTitle`*** ⇒ validates any data to check whether it is a valid or invalid voter registration number.
This _method_ returns **`True`** or **`False`**. Check out the parameters of this _method_ in the table below:

|Parameter name | Data Type | Description |
|:----|:----:|:-----|
| Expression | object | Some information whatsoever, and it may even be arrays.|

<a id="validate_pis"></a>

#### 2.5 - NIS Validation (Brazilian PIS Or PASEP Number)
This method is used to validate whether any information in the parameter is a pis or pasep number, true or false.

`BrazilSharp.Validate`.***`PIS`*** ⇒ validates any data to check whether it is a valid or invalid PIS or PASEP number.
This _method_ returns **`True`** or **`False`**. Check out the parameters of this _method_ in the table below:

|Parameter name | Data Type | Description |
|:----|:----:|:-----|
| Expression | object | Some information whatsoever, and it may even be arrays.|

<a id="validate_renavam"></a>

#### 2.6 - RENAVAM number validation
This method is used to validate whether any information in the parameter is a true or false RENAVAM number.

`BrazilSharp.Validate`.***`Renavam`*** ⇒ validates any data to check whether it is a valid RENAVAM number or not.
This _method_ returns **`True`** or **`False`**. Check out the parameters of this _method_ in the table below:

|Parameter name | Data Type | Description |
|:----|:----:|:-----|
| Expression | object | Some information whatsoever, and it may even be arrays.|

<a id="validate_stateregistration"></a>

#### 2.7 - State Registration number validation
This method is used to validate whether any information in the parameter, is a state entry (of the state in which to specify in the parameter) is true or false.

`BrazilSharp.Validate`.***`StateRegistration`*** ⇒ validates any data, to check whether it is a state registration is valid or invalid, according to the state reported in the parameter. This _method_ returns **`True`** or **`False`**. Check out the parameters of this _method_ in the table below:state

|Parameter name | Data Type | Description |
|:----|:----:|:-----|
| Expression | object | Some information whatsoever, and it may even be arrays.|
| state | BrazilSharp.BrazilianStates | Some state listed in the enum... |

WARNING: The `state` parameter only accepts a single option of the referred enum, which's the type of this parameter!

<a id="validate_email"></a>

#### 2.8 - Email validation
This method is used to validate that any information in the parameter is a valid or invalid email address.

`BrazilSharp.Validate`.***`Email`*** ⇒ validates any data, converts it to and then uses it to check whether it is a valid or invalid email address.
This _method_ returns **`True`** or **`False`**. Check out the parameters of this _method_ in the table below:

|Parameter name | Data Type | Description |
|:----|:----:|:-----|
| Expression | object | Some information whatsoever, and it may even be arrays.|

<a id="validate_numeric"></a>

#### 2.9 - Validation of many type numbers
This method is used to validate whether any information in the parameter is in fact any number or not.
This method is familiar to those who program in VBA or VB.NET. Except with the difference that, it works on any platform that runs DotNET (Whether by .NET, .NET Framework, .NET Core, Xamarin, Mono, etc).

    For those who have problems with certain methods of the Visual Basic library in C# or F# and run on non-windows platforms (or simply can't import the libraries in the project you're working on), maybe this library could be an alternative. Soon (in future updates), methods for calculations, etc. will appear.

`BrazilSharp.Validate`.***`Numeric`*** ⇒ validates any data to check whether it is in fact any number or not.
Even numbers of various bases (Up to base 16, Hexadecimal) and
several languages, etc. This _method_ returns **`True`** or **`False`**. Check out the parameters of this _method_ in the table below:

|Parameter name | Data Type | Description |
|:----|:----:|:-----|
| Expression | object | Some information whatsoever, and it may even be arrays.|

<a id="validate_dateortime"></a>

#### 2.10 - Validation of Dates or Times
This method is used to validate whether any information passed in the parameter, is date/time or not.
This method is familiar to those who program in VBA or VB.NET. Except with the difference that, it works on any platform that runs DotNET (Whether by .NET, .NET Framework, .NET Core, Xamarin, Mono, etc).

Para quem tiver problemas com certos métodos da biblioteca do Visual Basic no C# ou no F# e rodar em plataformas não-windows (ou simplismente não conseguir importar as bibliotecas no projeto em que estiver trabalhando), talvez esta biblioteca poderia ser uma alternativa. Em breve (nas próximas atualizações), surgirão os métodos para cálculos, etc.
`BrazilSharp.Validate`.***`DateOrTime`*** ⇒ validates any data to check whether it is a valid value or a date/time object or not.
This _method_ returns **`True`** or **`False`**. Check out the parameters of this _method_ in the table below:

|Parameter name | Data Type | Description |
|:----|:----:|:-----|
| Expression | object | Some information whatsoever, and it may even be arrays.|

Examples of document validation code in languages other than DotNET.
NOTE: These code examples below demonstrate how it is used...

<a id="validate_samplecode"></a>

### Document validation code examples, in languages other than DotNET.
NOTE: These code examples below demonstrate how to use it...

```C#
// NOTE: Any data exposed here was randomly generated on the 4devs website.

// How to use State Enrollment validation with C# 
// The first parameter (Expression) accepts any argument of any type (There is no specific type). 
bool  isStateRegistrateValid = BrazilSharp.Validate.StateRegistration(new  int[]{34,792,62,3}, BrazilSharp.BrazilianStates.RJ);

// How to use any other validation method, with a single parameter called 'Expression' of type 'object': 
bool isValidCnh = BrazilSharp.Validate.DriverLicense(92366290549);
bool isValidCpf = BrazilSharp.Validate.CPF(new  object[]{0b11111010,'.',135,'.',0x2E4,'-',"05"});
```
```VB
' NOTE: Any data exposed here were randomly generated on the 4devs website.

' How to use State Registration Validation with Visual Basic .NET 
Dim isStateRegistrationValid As Boolean = BrazilSharp.Validate.StateRegistration("178.326.260/1360",BrazilSharp.BrazilianStates.MG)

' How to use any other validation method, with a single parameter called 'Expression' of type 'Object': 
Dim isValidEmail As Boolean = BrazilSharp.Validate.Email("massive.tests@testssir.com")
Dim isNumeric As Boolean = BrazilSharp.Validate.Numeric("R$ 550,228,825.49");
```
NOTE: This code below is just a demonstration, an example of a little program in which the user chooses which document or data he wants to validate...

```C#
// Demonstration using C# 
using System;
using BrazilSharp;

namespace ConsoleApp1
{
    public class program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Select only ONE of the options below and then press enter...");
            Console.WriteLine("1 - Validate CPF");
            Console.WriteLine("2 - Validate CNPJ");
            Console.WriteLine("3 - Validate CNH");
            Console.WriteLine("4 - Validate Voter Title");
            Console.WriteLine("5 - Validate PIS/PASEP");
            Console.WriteLine("6 - Validate RENAVAM");
            Console.WriteLine("7 - Validate State Registration");
            Console.WriteLine("8 - Validate E-Mail");
            Console.WriteLine("9 - Validate Date/Time");
            Console.WriteLine("10 - Validate Numbers");
            string strOptionChose = Console.ReadLine();
            int optChose = Convert.ToInt32(strOptionChose);
            Console.WriteLine("Now enter some information to validate...");
            string documentOrData = Console.ReadLine();
            bool isDocumentValid ;
            string typeOfDocument;
            switch(optChose)
            {
                case 1:// User chose to validate CPF 
                    isDocumentValid = Validate.CPF(documentOrData);
                    typeOfDocument = "CPF (Individual Taxpayer Registration)";
                    break;
                 case 2:// User chose to validate CNPJ 
                    isDocumentValid = Validate.CNPJ(documentOrData);
                    typeOfDocument = "CNPJ (Brazilian Register of Legal Entities";
                    break;
                 case 3:// User chose to validate CNH 
                    isDocumentValid = Validate.DriverLicense(documentOrData);
                    typeOfDocument = "CNH (Brazilian Driver License)";
                    break;
                 case 4:// User chose to validate Voter Title 
                    isDocumentValid = Validate.VoterTitle ( documentOrData);
                    typeOfDocument = "Voter Title";
                    break;
                 case 5:// User chose to validate PIS/PASEP 
                    isDocumentValid = Validate.PIS(documentOrData);
                    typeOfDocument = "NIS (PIS/PASEP Number)";
                    break;
                 case 6:// User chose to validate RENAVAM 
                    isDocumentValid  = Validate.Renavam(documentOrData);
                    typeOfDocument = "RENAVAM (Brazilian Register of Motor Vehicles";
                    break;
                 case 7:// The user chose to validate State Registration 
                    Console.WriteLine("Now enter the acronym of the Brazilian state in which you want to validate this State Registration");
                    string acronym = Console.ReadLine();
                    BrazilianStates  state  =  Enum.Parse(typeof(BrazilianStates), acronym , true );
                    isDocumentValid = Validate.StateRegistration ( documentOrData , state );
                    typeOfDocument = "IE (State Registration)";
                    break;
                 case 8:// User chose to validate Email 
                    isDocumentValid = Validate.Email(documentOrData);
                    typeOfDocument = "email";
                    break;
                 case 9:// User chose to validate Date or Time 
                    isDocumentValid = Validate.DateOrTime(documentOrData);
                    typeOfDocument = "Date or Time";
                    break;
                 case 10:// User chose to validate Numbers 
                    isDocumentValid = Validate.Numeric(documentOrData);
                    typeOfDocument = "Number";
                    break;
                 default:// User chose invalid option or no option 
                    documentOrData = "???";
                    typeOfDocument = "Unknown";
                    isDocumentValid = false;
                    break;
            }
            
            if(isDocumentValid )
            {
                Console.WriteLine("OK, passed the test! This {0} is Valid! " , typeOfDocument);
            }
            else if(! isDocumentValid  &&  typeOfDocument  ==  " Unknown " )
            {
                Console.WriteLine("You entered the wrong option, try again!");
            }
            else
            {
                Console.WriteLine("{0} {1}, failed the test! This {0} is invalid! ", typeOfDocument, documentOrData);
            }
        }
    }
}
```
```VB
' Demonstration using Visual Basic DotNet 
' There is usually no need for the 'Imports System' in VB.NET, 
' as the compiler imports automatically. 
Imports  BrazilSharp

Module  Program 
    Public  Sub  Main(ByVal  args As String ()) 
        Console.WriteLine("Select only ONE of the options below and then press enter...") 
        Console.WriteLine("1 - Validate CPF") 
        Console.WriteLine("2 - Validate CNPJ") 
        Console.WriteLine("3 - Validate CNH") 
        Console.WriteLine("4 - Validate Voter Title") 
        Console.WriteLine("5 - Validate PIS/PASEP") 
        Console.WriteLine("6 - Validate RENAVAM") 
        Console.WriteLine("7 - Validate State Registration") 
        Console.WriteLine("8 - Validate E-Mail") 
        Console.WriteLine("9 - Validate Date/Time") 
        Console.WriteLine("10 - Validate Numbers") 
        Dim strOptionChose As String = Console.ReadLine
        Dim optChose As Integer = Convert.ToInt32(strOptionChose) 
        Console.WriteLine("Now enter some information to validate...") 
        Dim documentOrData As String = Console.ReadLine
        Dim isDocumentValid As Boolean 
        Dim typeOfDocument As String

        Select Case optChose 
            Case 1 ' User chose the option to validate CPF 
                isDocumentValid = Validate.CPF(documentOrData) 
                typeOfDocument = "CPF (Individual Taxpayer Registration)" 
            Case 2 ' User chose the option to validate CNPJ 
                isDocumentValid = Validate.CNPJ(documentOrData) 
                typeOfDocument = "CNPJ (Brazilian Register of Legal Entities)" 
            Case 3 ' User chose the option to validate CNH 
                isDocumentValid = Validate.DriverLicense(documentOrData) 
                typeOfDocument = "CNH (Brazilian Driver's License)" 
            Case 4 ' User chose the option to validate Voter Title 
                isDocumentValid = Validate.VoterTitle(documentOrData) 
                typeOfDocument = "Voter Title" 
            Case 5 ' User chose the option to validate PIS/PASEP 
                isDocumentValid = Validate.PIS(documentOrData) 
                typeOfDocument = "NIS (PIS Number) /PASEP)" 
            Case 6 ' User chose the option to validate RENAVAM 
                isDocumentValid = Validate.Renavam(documentOrData) 
                typeOfDocument = "RENAVAM (Brazilian Register of Motor Vehicles" 
            Case 7 ' User chose the option to validate State Registration
                Console.WriteLine("Now enter the acronym of the Brazilian state in which you want to validate this State Registration") 
                Dim Acronym As String = Console.ReadLine 
                Dim state As BrazilianStates = [Enum].Parse(GetType (BrazilianStates),  Acronym,  True) 
                isDocumentValid = Validate.StateRegistration(documentOrData,  state) 
                typeOfDocument = "IE (State Registration)" 
            Case 8 ' User chose the option to validate Email 
                isDocumentValid = Validate.Email(documentOrData) 
                typeOfDocument =  "email" 
            Case 9 ' User chose the option to validate Date or Time 
                isDocumentValid = Validate.DateOrTime(documentOrData) 
                typeOfDocument = "Date or Time" 
            Case 10 ' User chose the option to validate Numbers 
                isDocumentValid = Validate.Numeric(documentOrData) 
                typeOfDocument = " Number" 
            Case 0 Or Else ' User chose an invalid option or no option 
                documentOrData = "???" 
                typeOfDocument = "Unknown" 
                isDocumentValid = False 
        End Select
        
        If isDocumentValid Then
            Console.WriteLine("OK, passed the test! This {0} is Valid!" ,  typeOfDocument) 
        ElseIf  Not  isDocumentValid AndAlso typeOfDocument = "Unknown" Then
            Console.WriteLine("You entered the wrong option, please try again!") 
        Else
            Console.WriteLine("{0} {1}, failed the test! This {0} is invalid!",typeOfDocument, documentOrData) 
        End If 
    End  Sub 
End  Module
```
---

<a id="utilities"></a>

## 3 - Utilities
The utility _methods_ (such as finding out the state in which a voter title card was issued) are found in the `Utilities` _class_ (inside the _namespace_ `BrazilSharp`) and all of them, are static _methods_.
To use, it's consists in importing the _namespace_ `BrazilSharp` in the file where the methods will be used (Or using the **global using** in the case of DotNet 6 with C# 10, both onwards) and calling the desired method directly by the _class_, without instantiating it; since the _class_ `Utilities` is a static _class_.

<a id="utilities_findstatevotertitle"></a>

#### 3.1 - Find out the state in which the voter registration card was issued
This method is used to "reveal" the status in which a voter registration card was issued...

`BrazilSharp.Utilities`.***`FindStateVoterTitle`*** ⇒ validates any data entered, to check if it's a valid voter title's registration number or or invalid.

This _method_ returns some value from `BrazilSharp`.**`BrazilianStates`** enum, if the informed voter registration is valid. Otherwise returns **null** (Or **`Nothing`** on Visual Basic). Check the parameters of this _method_ in the table below:

| Parameter name | Data Type | Description |
|:----|:----:|:-----|
| expression | object | Any information whatsoever, it could even be arrays. |

IMPORTANT ATTENTION: These displayed voter registration numbers were randomly generated on the [4devs](www.4devs.com.br) website.

```C#
// Example use in C# 
BrazilSharp.BrazilianStates? issuerState = BrazilSharp.Utilities.FindStateVoterTitle(552667570396);
if(issuerState != null && issuerState != BrazilSharp.BrazilianStates.ZZ)
    Console.WriteLine("This voter title was issued on {0} - {1}", issuerState, issuerState.Value.GetFullName());
    // The GetGullName()extension Method, will be covered in the next topic.

else if(issuerState != null && issuerState == BrazilSharp.BrazilianStates.ZZ)
    Console.WriteLine("This voter registration card was issued abroad, most likely at the Brazilian consulate, in some country...");
else 
    Console.WriteLine("Invalid voter registration card !");
```
```VB
' Example usage in Visual Basic .NET 
Dim issuerState As BazilSharp.BrazilianStates? = BrazilSharp.Utilities.FindStateVoterTitle("586612712780")
If issuerState <> Nothing AndAlso issuerState <> BrazilSharp.BrazilianStates.ZZ  Then 
    Console.WriteLine("This voter title was issued on {0} - {1}", issuerState, issuerState.Value.GetFullName)
    ' The GetGullName() extension Method, will be covered in the next topic. 
ElseIf issuerState <> Nothing AndAlso issuerState = BrazilSharp.BrazilianStates.ZZ Then
    Console.WriteLine("This voter registration card was issued abroad, most likely at the Brazilian consulate, in some country...")
Else 
    Console.WriteLine("Invalid voter registration card!")
End If
```


<a id="utilities_fullstatename"></a>

#### 3.2 - Brazilian State name in full
This is an extension _method_ that is used in conjunction with _enum_ `BrazilSharp`.**`BrazilianStates`**, where, when calling the _method_, it will return a string with the name of the state in which it is equivalent to that instance of this _enum_...

```C#
// Example in C#, show how to use
BrazilSharp.BrazilianStates stateWhereAuthorLives = BrazilSharp.BrazilianStates.RJ;
string fullStateName = stateWhereAuthorLives.GetFullName();
System.Console.WriteLine($"The author of this project lives in the state of {fullStateName}, known as wonderful city, but actually dangerous city. LoL.");
```
```VB
' Sample code in Visual Basic, to show how to use 
Dim stateWhereAuthorLives As BrazilSharp.BrazilianStates = BrazilSharp.BrazilianStates.RJ 
Dim fullStateName As String = stateWhereAuthorLives.GetFullName
Console.WriteLine($"The author of this project lives in the state of do {fullStateName}, known as wonderful city, but actually dangerous city. LoL")
```




<a id="utilities_brazilianstatestre"></a>

#### 3.3 - Code of Brazilian states defined by the TRE, which can also be found in the voter registration card.
This field contains all Brazilian states, each with their respective issuing state identification codes, defined by TRE.
Here is one of the "secrets" of voter registration. This field is more for curiosity purposes, but can be used for other things as well, as needed.

C#: `System.Collections.Generic`.**`IReadOnlyDictionary`**_`<`_`BrazilSharp`.**`BrazilianStates`**,**`short`**_`>`_`BrazilSharp.Utilities`.***`TREBrazilianStatesCode`*** 

VB: `BrazilSharp.Utilities`.***`TREBrazilianStatesCode`*****`As`**`System.Collections.Generic`.**`IReadOnlyDictionary`**`(`**`Of`**`BrazilSharp`.**`BrazilianStates`**,**`Short`**`)`

This field is read-only. It contains all Brazilian states, each with its code defined by Brazilian's Regional Electoral Court.

```C#
// C# code example 
using System;
using BrazilSharp;

namespace ConsoleApp1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter an acronym of a state to know the code defined by TRE...");
            Console.WriteLine("If outside Brazil, enter 'ZZ'.");
            string argument = Console.ReadLine();
            BrazilianStates gotState = Enum.Parse(typeof(BrazilianStates), argument, true);
            short codeStateFromTRE = Utilities.TREBrazilianStatesCode [ gotState ];            
            Console.WriteLine("State code {0} defined by TRE is {1} and by IBGE is {2} ", argument, codeStateFromTRE, gotState);
        }
    }
}
```
```VB
' Sample code in Visual Basic 
Imports BrazilSharp

Module Program 
    Public Sub Main(ByVal args As String()) 
        Console.WriteLine("Enter an acronym of a state to know the code defined by TRE...")
        Console.WriteLine("If outside Brazil, enter ' ZZ'.")
        string argument = Console.ReadLine()
        Dim gotState As BrazilianStates = [Enum].Parse(GetType(BrazilianStates), argument, True)
        Dim codeStateFromTRE As Short = Utilities.TREBrazilianStatesCode(gotState)
        
        Console.WriteLine("State code {0} defined by TRE is {1} and by IBGE is {2}", argument, codeStateFromTRE, gotState)
    End Sub
End Module
```

---
<a id="calculations"></a>

## 4 - Calculations
Set of classes, structures, methods, etc, for different types of calculations, such as Personnel Department calculations (At the moment only 13th available, but in future updates you will have the different types of calculations), financial, etc.

###### If you have any other calculation suggestions, open an Issue or make a Pull Request.


<a id="calculations_worker"></a>

#### 4.1 - Personnel Department or Human Resources Calculations
Set of static methods for calculations used in the Personnel Department or even in Human Resources. Like Holidays (Still COMING SOON), 13th proportional, etc.

Its use only consists in importing the _namespace_ `BrazilSharp.Calculations` in the file where the _methods_ will be used (Or using the **global using** in the case of DotNet 6 with C# 10, both onwards) and calling the desired method directly by the _class_, without instantiating it; since the _class_ WorkersCalculation is a static _class_.


<a id="calculations_worker_13th"></a>

##### 4.1.1 - Calculation of 13th
This method is used to calculate the proportional 13th, based on the information needed to make the calculation.

`BrazilSharp.Calculations.WorkersCalculation`.***`Thirteenth`*** ⇒ Calculates the 13th proportional of an employee. This _method_ returns a Double, with the 13th already calculated.
Below is the table with the details and instructions of the _parameters_ of this _method_:

| Parameter name | Parameter typing | Parameter Description|
|:----|:----:|:-----|
| calculationBasis | double | The calculation basis for the 13th...|
| EarlyWarning | bool | Set true if the employee is on notice. If not, false!
| Regime | BrazilSharp.Calculations.PeriodType | Will the calculation be done over DAYS or MONTHS?<br>Enter `BrazilSharp.Calculations.PeriodType.Day`, to calculate in DAYS.<br>Or,<br>Enter BrazilSharp.Calculations.PeriodType.Monthto calculate in MONTHS.|
| HowLongWorked | shorts | If the calculation is made over DAYS , enter between 1 and 31 , how many days the employee worked!<br>If the calculation is made over MONTHS , enter between 1 and 12 , how many months the employee worked!<br>Or,<br>If outside the range mentioned above, an error of type System. ArgumentOutOfRangeExceptionIt will be released!|


<a id="calculations_samples"></a>

## Code examples of different types of calculations, in languages ​​other than DotNET.
NOTE: These codes below are for demonstration purposes only. A little program for the user to interact and do some calculation...


```C#
// Sample demo code in C#
using System;
using BrazilSharp;
using BrazilSharp.Calculations;

namespace  ConsoleApp1 
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Choose one of options bellow and then, press ENTER:");
            Console.WriteLine("1 - Calculate 13th of employee");
            Console.WriteLine("0 - Exit");
        
            string strOptionChose = Console.ReadLine();
            int optChose = Convert.ToInt32(strOptionChose);
            switch(optChose)
            {
                case 1:
                    Calculate13th();
                    break;
                case 0:
                default:
                    Console.WriteLine("Bye Bye...");
                    Environment.Exit(0);
                    break;
            }
        }

        private static void Calculate13th()
        {
            Console.WriteLine("Let's Calculate the 13th Proportional");

            Console.WriteLine("Enter the employee's name");
            string employeeName = Console.ReadLine();
            
            Console.WriteLine("Inform the salary + night pay + unhealthy work + other gains provided for in the Brazilian labor laws...");
            string strCalcBase = Console.ReadLine();
            double calcBase = Convert.ToDouble(strCalcBase);
            
            Console.WriteLine("Early warning? Press 'Y' or 'N'.");
            char respnseEarlyWarning = Console.ReadKey();
            bool avisoPrevio = respnseEarlyWarning == 'Y'||respnseEarlyWarning == 'y';
            Console.WriteLine("Enter 'Day' if the calculation will be done IN DAYS:")
            Console.WriteLine("OR")
            Console.WriteLine("Type 'Month' if the calculation will be done IN MONTHS:")
            string responseCalcRegime = Console.ReadLine();
            PeriodType calculationRegime = Enum.Parse(typeof(PeriodType), responseCalcRegime, false);

            Console.WriteLine("How long ""{0}'s"" that {1} worked?", responseCalcRegime, employeeName);
            string strPeriodWorked = Console.ReadLine();
            short periodWorked = Convert.ToInt16(strPeriodWorked);

            double ResultadoDoCaculo = WorkersCalculation.Thirteenth(calcBase, avisoPrevio, calculationRegime, periodWorked);
            
            Console.WriteLine("The thirteenth of {0} is {1}.", employeeName, ResultadoDoCalculo)
            Console.WriteLine("If you add the salary and the rest of the calculation base, it will give {0}.", employeeName)
            Console.WriteLine("Congratulations {0}! You have a lot of money!!!", employeeName)
        }
    }
}
```
```VB
' Sample demo code in Visual Basic
Imports BrazilSharp;
Imports BrazilSharp.Calculations;

Module Program
    Public Sub Main(ByVal args As String())
        Console.WriteLine("Choose one of options bellow and then, press ENTER:")
        Console.WriteLine("1 - Calculate 13th of employee")
        Console.WriteLine("0 - Exit")
        Dim strOptionChose As String = Console.ReadLine
        Dim optChose As Integer = Convert.ToInt32(strOptionChose)
        Select Case optChose
            case 1
                Calculate13th()
            Case 0 Or Else
                Console.WriteLine("Bye bye...")
                Environment.Exit(0)
        End Select
    End Sub

    Public Sub Calculate13th()
        Console.WriteLine("Let's Calculate the 13th Proportional")

        Console.WriteLine("Enter the employee's name")
        Dim employeeName As String = Console.ReadLine
        
        Console.WriteLine("Inform the salary + night pay + unhealthy work + other gains provided for in the Brazilian labor laws...")
        Dim strCalcBase As String = Console.ReadLine
        Dim calcBase As Double = Convert.ToDouble(strCalcBase)
        
        Console.WriteLine("Early warning? Press 'Y' or 'N'.")
        Dim respnseEarlyWarning As Char = Console.ReadKey
        Dim avisoPrevio As Boolean = respnseEarlyWarning = "Y"c Or respnseEarlyWarning = "y"c

        Console.WriteLine("Enter 'Day' if the calculation will be done IN DAYS:")
        Console.WriteLine("OR")
        Console.WriteLine("Type 'Month' if the calculation will be done IN MONTHS:")
        Dim responseCalcRegime As String = Console.ReadLine
        Dim calculationRegime As PeriodType = [Enum].Parse(GetType(PeriodType), responseCalcRegime, False)

        Console.WriteLine("How long ""{0}'s"" that {1} worked?", responseCalcRegime, employeeName)
        Dim strPeriodWorked As String = Console.ReadLine
        Dim periodWorked As Short = Convert.ToInt16(strPeriodWorked)

        Dim ResultadoDoCaculo As Double = WorkersCalculation.Thirteenth(calcBase, avisoPrevio, calculationRegime, periodWorked)
        
        Console.WriteLine("The thirteenth of {0} is {1}.", employeeName, ResultadoDoCalculo)
        Console.WriteLine("If you add the salary and the rest of the calculation base, it will give {0}.", employeeName)
        Console.WriteLine("Congratulations {0}! You have a lot of money!!!", employeeName)
    End Sub
End Module
```