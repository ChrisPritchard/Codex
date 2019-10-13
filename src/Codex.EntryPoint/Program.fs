open System
open Elmish
open Elmish.WPF

open MainWindow

let init _ = 
    { 
        sceneEditor = None
        tableOfContents = None
    }, Cmd.none

[<EntryPoint; STAThread>]
let main _ =
  Program.mkProgramWpf init MainWindow.update MainWindow.bindings
  |> Program.withConsoleTrace
  |> Program.withDebugTrace
  |> Program.runWindowWithConfig ElmConfig.Default (Codex.Views.MainWindow ())
