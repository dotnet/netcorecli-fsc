namespace TestApp

open System
open System.Reflection
open System.Resources

type T () = class end

module Program =

    [<EntryPoint>]
    let main args =
        try
            let res = ResourceManager("Messages", (typeof<T>.GetTypeInfo().Assembly))
            Console.WriteLine(res.GetString("Ciao"))
            0
        with ex ->
            printfn "%A" ex
            1
