# F\# and .NET Core SDK

F# and .NET Core SDK working together

This repo contains the sources for:

- `dotnet-compile-fsc`

for .NET Core SDK preview2.

| Windows x64 | Ubuntu x64 | OS X | RHEL 7.2 |
|-------------|------------|------|----------|
| [![Build Status][win-x64-badge]](https://ci2.dot.net/job/dotnet_netcorecli-fsc/job/preview2.1/job/release_windows_nt_x64/) | [![Build Status][ubuntu-x64-badge]](https://ci2.dot.net/job/dotnet_netcorecli-fsc/job/preview2.1/job/release_ubuntu_x64/) | [![Build Status][osx-x64-badge]](https://ci2.dot.net/job/dotnet_netcorecli-fsc/job/preview2.1/job/release_osx_x64/) | [![Build Status](https://ci2.dot.net/buildStatus/icon?job=dotnet_netcorecli-fsc/preview2.1/release_rhel7.2_x64)](https://ci2.dot.net/job/dotnet_netcorecli-fsc/job/preview2.1/job/release_rhel7.2_x64/) |

## Build

To build a package and run the tests:

```
build
```

See `build -Help` for more options.

**NOTE** The build script by default creates a package with a fixed version and 
it's saved in the local nuget cache. 
To correctly test the package, each build run needs to increment the package version, otherwise
the cached package is used.
Use the `-VersionSuffix 013076` argument (for example `build -VersionSuffix 013076`) 
to create the package `dotnet-compile-fsc.1.0.0-preview2-013076`.


## The .NET Core SDK preview3 and msbuild based project system

Work in progress.

Require .NET Core SDK preview3 ( http://github.com/dotnet/cli ), tested with `1.0.0-preview3-004007`.

Example apps can be found in `examples/preview3`.
See also `src/FSharp.Net.Sdk` and `test-msbuild` directories.


[win-x64-badge]: https://ci2.dot.net/buildStatus/icon?job=dotnet_netcorecli-fsc/preview2.1/release_windows_nt_x64
[ubuntu-x64-badge]: https://ci2.dot.net/buildStatus/icon?job=dotnet_netcorecli-fsc/preview2.1/release_ubuntu_x64
[osx-x64-badge]: https://ci2.dot.net/buildStatus/icon?job=dotnet_netcorecli-fsc/preview2.1/release_osx_x64
