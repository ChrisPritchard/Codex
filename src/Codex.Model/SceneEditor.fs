module SceneEditor

open Elmish

type Model = {
    visible: bool
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