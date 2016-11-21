
Tested with .NET Core sdk version `1.0.0-preview3-004056`

See also `src\FSharp.NET.Sdk\README.md`

To run tests, do `dotnet msbuild run-tests.targets /t:Test`
or to run a specific test `dotnet msbuild run-tests.targets /t:TestName`

To show the list of all avaiable tests run `dotnet msbuild run-tests.targets /t:TestSuiteInfo`

Under test:

- `dotnet restore`
- `dotnet build`
- `dotnet run`
- `dotnet pack`
