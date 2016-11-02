
Tested with .NET Core sdk version `1.0.0-preview3-004007`

## On restore

The targets and props file are auto imported if the package is referenced.

The auto-import is from `obj\{ProjectName}.fsproj.nuget.g.targets` and `obj\{ProjectName}.fsproj.nuget.g.props` 
generated on `dotnet restore`


## The `CoreCompile` task

- create the `obj\{Configuration}\dotnet-compile-fsc.rsp` response file
- invoke the `dotnet compile-fsc` (it's the `dotnet-compile-fsc` tool app).
  - like `preview2`, the `dotnet-compile-fsc` invoke the `fsc` passing `obj\{Configuration}\dotnet-compile-fsc.rsp` response 
    file who contains the `fsc` arguments

## Notes

The `Authors` property is required for `dotnet pack`
Because the `obj\{Configuration}\dotnet-compile-fsc.rsp` is created (like preview2) the intellisense in VS Code already works using Ionide.

## TODO

Next version should use the an MSBuild task instead of invoking the `dotnet-compile-fsc`
