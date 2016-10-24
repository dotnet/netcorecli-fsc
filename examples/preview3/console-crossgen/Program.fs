// Learn more about F# at http://fsharp.org

open System

[<EntryPoint>]
let main argv = 

    let fromfw =
#if NET451
        "net451"
#else
#if NETCOREAPP1_0
        "netcoreapp1.0"
#else
        "Unknown framework"
#endif
#endif

    printfn "Hello World from F#! using %s" fromfw
    0 // return an integer exit code
