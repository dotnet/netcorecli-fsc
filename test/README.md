
## Info

Tests are built in temporary directories under `%TEMP%/NetcoreCliFscTests` directory

## Configuration

Use env var `DOTNET_TEST_PRESERVE_TEMP` to preserve temporary test directories.

From `test/dotnet-new.Tests` :

- `dotnet test` to run all tests
- `dotnet test -t` to show all tests (without run)
- `dotnet test --filter "FullyQualifiedName=NetcoreCliFsc.Tests.CommonScenario.TestAppWithArgs451"` to run a specific test (see `-t` for list)
