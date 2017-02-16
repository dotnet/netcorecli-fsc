# F\# and .NET Core SDK working together

[![FSharp.NET.Sdk](https://img.shields.io/nuget/vpre/FSharp.NET.Sdk.svg?maxAge=2592000&label=FSharp.NET.Sdk%20nuget)](https://www.nuget.org/packages/FSharp.NET.Sdk)

**NOTE** See [Wiki](https://github.com/dotnet/netcorecli-fsc/wiki) for a lot more info.

Need more help? [#dotnetcore channel in fsharp.org slack](https://fsharp.slack.com/messages/dotnetcore/), see [info about signup](http://fsharp.org/guides/slack/)

See `examples` directory like [examples/rc4](https://github.com/dotnet/netcorecli-fsc/tree/master/examples/rc4) for version specific example of common projects (lib,console.xunit,web)

This repo contains the sources for nuget packages:

- [FSharp.NET.Sdk](https://www.nuget.org/packages/FSharp.NET.Sdk/)
- [FSharp.Sdk](https://www.nuget.org/packages/FSharp.Sdk/)

History also contains:

- [dotnet-compile-fsc](https://www.nuget.org/packages/dotnet-compile-fsc/) for integration `project.json` based sdk (`preview2`, `preview2.1`)


| Windows x64 | Ubuntu x64 | OS X | RHEL 7.2 | Debian 8.2 | Fedora 23 | OpenSUSE 13.2 | CentOS 7.1 |
|-------------|------------|------|----------|------------|-----------|---------------|------------|
| [![Build Status][win-x64-badge]](https://ci2.dot.net/job/dotnet_netcorecli-fsc/job/master/job/release_windows_nt_x64/) | [![Build Status][ubuntu-x64-badge]](https://ci2.dot.net/job/dotnet_netcorecli-fsc/job/master/job/release_ubuntu_x64/) | [![Build Status][osx-x64-badge]](https://ci2.dot.net/job/dotnet_netcorecli-fsc/job/master/job/release_osx_x64/) | [![Build Status](https://ci2.dot.net/buildStatus/icon?job=dotnet_netcorecli-fsc/master/release_rhel7.2_x64)](https://ci2.dot.net/job/dotnet_netcorecli-fsc/job/master/job/release_rhel7.2_x64/) | [![Build Status](https://ci2.dot.net/buildStatus/icon?job=dotnet_netcorecli-fsc/master/debug_debian8.2_x64)](https://ci2.dot.net/job/dotnet_netcorecli-fsc/job/master/job/debug_debian8.2_x64/) | [![Build Status](https://ci2.dot.net/buildStatus/icon?job=dotnet_netcorecli-fsc/master/debug_fedora23_x64)](https://ci2.dot.net/job/dotnet_netcorecli-fsc/job/master/job/debug_fedora23_x64/) | [![Build Status](https://ci2.dot.net/buildStatus/icon?job=dotnet_netcorecli-fsc/master/debug_opensuse13.2_x64)](https://ci2.dot.net/job/dotnet_netcorecli-fsc/job/master/job/debug_opensuse13.2_x64/) | [![Build Status](https://ci2.dot.net/buildStatus/icon?job=dotnet_netcorecli-fsc/master/debug_centos7.1_x64)](https://ci2.dot.net/job/dotnet_netcorecli-fsc/job/master/job/debug_centos7.1_x64/) |

## Build

To build a package and run the tests:

```
build
```

The build script will download .NET Core Sdk if needed (installed is not the exact version required)

## Test

the `build` will also run test suite.

See [test/README.md](https://github.com/dotnet/netcorecli-fsc/blob/master/test/README.md) for more info about test suite


[win-x64-badge]: https://ci2.dot.net/buildStatus/icon?job=dotnet_netcorecli-fsc/master/release_windows_nt_x64
[ubuntu-x64-badge]: https://ci2.dot.net/buildStatus/icon?job=dotnet_netcorecli-fsc/master/release_ubuntu_x64
[osx-x64-badge]: https://ci2.dot.net/buildStatus/icon?job=dotnet_netcorecli-fsc/master/release_osx_x64
