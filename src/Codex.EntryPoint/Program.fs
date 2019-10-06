open System
open Elmish
open Elmish.WPF

open MainWindow

let init _ = 
    { 
        sceneEditor = Some { title = "Current Scene"; xamlContent = ""; wordCount = 0 } 
    }, Cmd.none
       
let mainWindowBindings _ = [
    "SceneEditor" |> Binding.subModelWin (
        (fun m -> m.sceneEditor |> WindowState.ofOption), 
        snd, 
        MainWindow.SceneEditorMessage,
        SceneEditor.bindings,
        (Codex.Views.SceneEditor))
    ]

[<EntryPoint; STAThread>]
let main _ =
  Program.mkProgramWpf init MainWindow.update mainWindowBindings
  |> Program.withConsoleTrace
  |> Program.runWindowWithConfig { ElmConfig.Default with LogConsole = true } (Codex.Views.MainWindow ())
