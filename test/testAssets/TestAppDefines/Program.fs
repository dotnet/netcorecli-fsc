// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace TestApp

open System
open System.Diagnostics

module Program =

    let configurationName =
#if RELEASE
        "RELEASE"
#else
    #if DEBUG
        "DEBUG"
    #else
        "UNKNOWN"
    #endif
#endif

    let frameworkDefine =
#if NETCOREAPP1_0
        "NETCOREAPP1_0"
#else
        "UNKNOWN"
#endif

    [<EntryPoint>]
    let main _ =

        printfn "CONF: '%s'" configurationName
        printfn "TFM: '%s'" frameworkDefine

        0
