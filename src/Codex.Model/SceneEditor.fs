module SceneEditor

open Elmish
open Elmish.WPF

type Model = {
    title: string
    xamlContent: string
    wordCount: int
    }

type Message =
    | CloseSceneEditor
    | UpdateXamlContent of string
    | UpdateWordCount of int

let update message model =
    match message with
    | UpdateXamlContent s ->
        { model with xamlContent = s }, Cmd.none
    | UpdateWordCount n ->
        { model with wordCount = n }, Cmd.none
    | _ ->
        // messages not covered above are probably used by parent windows (e.g. main for close)
        model, Cmd.none
               
let bindings _ = [
        "Title" |> Binding.oneWay (fun  m -> m.title)
        "CloseWindow" |> Binding.cmd CloseSceneEditor
        "XamlContent" |> Binding.twoWay ((fun m -> m.xamlContent), UpdateXamlContent) 
        "WordCount" |> Binding.twoWay ((fun m -> m.wordCount), UpdateWordCount)
    ]