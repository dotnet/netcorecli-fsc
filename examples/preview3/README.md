
Require .NET Core sdk preview3
Tested with .NET Core sdk version `1.0.0-preview3-004056`

# NOTES

For following projects the normal commands (`restore`,`build`,`run`,`publish`,`test`,`pack`) have issues

## console-crossgen

- `dotnet restore`
- ~~`dotnet build`~~ doenst work atm
    - use `dotnet build -r win7-x64` instead as workaround
- `dotnet run -f netcoreapp1.0`
- ~~`dotnet run -f net451`~~ doesnt work atm
    - use `bin\Debug\net451\ExampleConsoleApp.exe` instead as workaround
- ~~`dotnet pack`~~
    - use `dotnet pack /p:RuntimeIdentifier=win7-x64` instead as workaround

