open System
open Elmish
open Elmish.WPF

open Codex.Model
open Codex.Views

let init _ = 
    { 
        sceneEditorState = Some { xamlContent = ""; wordCount = 0 } 
    }, Cmd.none

[<EntryPoint; STAThread>]
let main _ =
  Program.mkProgramWpf init Update.update bindings
  |> Program.withConsoleTrace
  |> Program.runWindowWithConfig { ElmConfig.Default with LogConsole = true } (MainWindow())
