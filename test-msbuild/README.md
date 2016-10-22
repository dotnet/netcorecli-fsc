
Tested with .NET Core sdk v1.0.0-preview3-003884

See also `src\FSharp.NET.Sdk\README.md`

To use msbuild based fsproj, use the same sdk commands of `preview2`, but with `3` suffix.
So `dotnet build3` instead of `dotnet build`, etc.

The normal commands like `dotnet build`, `dotnet restore` atm, expect the `project.json` so 
like `preview2`.

Tested:

- `dotnet restore3`
- `dotnet build3`
- `dotnet run3`
- `dotnet pack3`

