module Codex.Model.Update

open Elmish

let update message model =
    match message with
    | ExamineModel -> 
        model, Cmd.none
    | UpdateXamlContent s ->
        { model with xamlContent = s }, Cmd.none
    | UpdateWordCount n ->
        { model with wordCount = n }, Cmd.none