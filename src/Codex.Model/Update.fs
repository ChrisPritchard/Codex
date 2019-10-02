module Codex.Model.Update

open Elmish

let update message model =
    match message with
    | Save ->
        model, Cmd.none    
    | Load ->
        model, Cmd.none
    | Quit ->
        model, Cmd.none