module TableOfContents

open Elmish
open Elmish.WPF

type Model = {
    title: string
    }

type Message =
    | CloseTableOfContents

let update message model =
    match message with
    | _ ->
        // messages not covered above are probably used by parent windows (e.g. main for close)
        model, Cmd.none
               
let bindings _ = [
        "Title" |> Binding.oneWay (fun  m -> m.title)
        "CloseWindow" |> Binding.cmd CloseTableOfContents
    ]