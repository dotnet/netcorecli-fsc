# F\# and .NET Core SDK

F# and .NET Core SDK working together

This repo contains the sources for:

- `FSharp.NET.Sdk`
- `FSharp.Sdk`

History also contains:

- `dotnet-compile-fsc` for integration `project.json` based sdk (`preview2`, `preview2.1`)


| Windows x64 | Ubuntu x64 | OS X | RHEL 7.2 |
|-------------|------------|------|----------|
| [![Build Status][win-x64-badge]](https://ci2.dot.net/job/dotnet_netcorecli-fsc/job/master/job/release_windows_nt_x64/) | [![Build Status][ubuntu-x64-badge]](https://ci2.dot.net/job/dotnet_netcorecli-fsc/job/master/job/release_ubuntu_x64/) | [![Build Status][osx-x64-badge]](https://ci2.dot.net/job/dotnet_netcorecli-fsc/job/master/job/release_osx_x64/) | [![Build Status](https://ci2.dot.net/buildStatus/icon?job=dotnet_netcorecli-fsc/master/release_rhel7.2_x64)](https://ci2.dot.net/job/dotnet_netcorecli-fsc/job/master/job/release_rhel7.2_x64/) |

## Build

To build a package and run the tests:

```
build
```

The build script will download .NET Core Sdk if needed (installed is not the exact version required)

## Test

the `build` will also run test suite.

See [[test/README.md]] for more info about test suite


[win-x64-badge]: https://ci2.dot.net/buildStatus/icon?job=dotnet_netcorecli-fsc/master/release_windows_nt_x64
[ubuntu-x64-badge]: https://ci2.dot.net/buildStatus/icon?job=dotnet_netcorecli-fsc/master/release_ubuntu_x64
[osx-x64-badge]: https://ci2.dot.net/buildStatus/icon?job=dotnet_netcorecli-fsc/master/release_osx_x64
