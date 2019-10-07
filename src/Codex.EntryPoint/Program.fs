open System
open Elmish
open Elmish.WPF

open MainWindow

let init _ = 
    { 
        sceneEditor = None//Some { title = "Current Scene"; xamlContent = ""; wordCount = 0 } 
        tableOfContents = None
    }, Cmd.none
       
let mainWindowBindings _ = [

    "ShowSceneEditor" |> Binding.cmd ShowSceneEditor
    "SceneEditor" |> Binding.subModelWin (
        (fun m -> m.sceneEditor |> WindowState.ofOption), 
        snd, 
        MainWindow.SceneEditorMessage,
        SceneEditor.bindings,
        (Codex.Views.SceneEditor))

    "ShowTableOfContents" |> Binding.cmd ShowTableOfContents
    "TableOfContents" |> Binding.subModelWin (
           (fun m -> m.tableOfContents |> WindowState.ofOption), 
           snd, 
           MainWindow.TableOfContentsMessage,
           TableOfContents.novelBindings,
           (Codex.Views.TableOfContents))

    ]

[<EntryPoint; STAThread>]
let main _ =
  Program.mkProgramWpf init MainWindow.update mainWindowBindings
  |> Program.withConsoleTrace
  |> Program.runWindowWithConfig { ElmConfig.Default with LogConsole = true } (Codex.Views.MainWindow ())
