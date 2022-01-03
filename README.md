#  Brazil\# Lib

![BrazilSharp](BrazilSharpVector.svg)


**🇧🇷️ Português 🇧🇷️**

Biblioteca de classes para validações dos vários documentos emitidos no Brasil (Como o CPF, CNPJ, PIS, CNH, Título de Eleitor, Renavam, etc).<br>
 Assim como também, para cálculos trabalhistas (No momento tem somente o cálculo de 13º proporcional, em breve terá mais), etc.

Para ler a documentação, localize o arquivo LEIAME-BR.md (no repositório do GitHub), pois as informações completas encontram-se lá!<br>
Se estiver procurando pelo LEIAME-BR.html, ele foi removido e substituído pelo **LEIAME-BR.md**.
 		
Se quiser reportar um bug ou sugerir melhorias, fique à vontade para abrir uma issue!<br>
 Se quiser contribuir com alguma melhoria mandando pull requests, fique à vontade!

No final deste README encontram-se os comandos para adicionar a biblioteca ao seu projeto.

Este projeto está sob a licensa MIT.
		
---

**🇺🇸️ English 🇬🇧**

Class library for validations of the various documents issued in Brazil (such as CPF, CNPJ, PIS, CNH, Voter Title number, Renavan, etc).<br>
 As well as for labor calculations (At the moment there is only the calculation of proportional 13th, soon there will be more), etc.

To read the documentation, find the README-EN.md file (on the GitHub Repository), all the info can be found there.<br>
 If you looking for README-EN.html, it was removed and replaced by **README-EN.md**.

 If you want to report a bug or suggest improvements, feel free to open an issue!<br>
 If you want to contribute with some improvement by sending pull requests, feel free!

In the end of this README, there's the command to add the lib to your project.

This project is under the MIT license.


---

[BrazilSharpLib - Nuget Package Manager](https://www.nuget.org/packages/BrazilSharpLib/)

🇧🇷 Deixar de informar a versão com o --version (Ou -v) na CLI do dotnet (ou então, o argumento "Version" da tag PackageReference no arquivo de projeto, como o csproj, fsproj e vbproj), será instalada a última versão lançada.

🇺🇸 Missing inform the version with --version (or -v) option on dotnet CLI (Or else, "Version" argument of PackageReference's tag of project file, like csproj, fsproj and vbproj), it'll install the latest released version.

Via DotNET CLI / Visual Studio
```PowerShell
PS> #Terminal - Windows PowerShell
PS> dotnet add package BrazilSharpLib
```
```sh
$ #Terminal - Linux (++distros) & mac OSX
$ dotnet add package BrazilSharpLib
```
```PowerShell
PM> #Visual Studio - Developer PowerShell
PM> Install-Package BrazilSharpLib
```

Manual
```xml
<!-- Via PackageReference 
    EN: Add to your project file (csproj/fsproj/vbproj) and save. After, run command "dotnet restore" on terminal
    PT: Adicione ao seu arquivo de projeto (csproj/fsproj/vbproj) e salve. Depois, rode o comando "dotnet restore" no terminal.
-->
<ItemGroup>
    <PackageReference Include="BrazilSharpLib" />
</ItemGroup>
```

