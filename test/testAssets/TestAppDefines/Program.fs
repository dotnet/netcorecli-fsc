// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace TestApp

open System
open System.Diagnostics

module Program =

    [<EntryPoint>]
    let main _ =
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

        printfn "CONF: '%s'" configurationName

        0
