open System
open Elmish
open Elmish.WPF

open Codex.Model
open Codex.Views

let init _ = { xamlContent = ""; wordCount = 20 }, Cmd.none

let bindings _ = [
        "ExamineModel" |> Binding.cmd ExamineModel
        "XamlContent" |> Binding.twoWay ((fun m -> m.xamlContent), UpdateXamlContent) 
        "WordCount" |> Binding.twoWay ((fun m -> m.wordCount), UpdateWordCount)
    ]

[<EntryPoint; STAThread>]
let main _ =
  Program.mkProgramWpf init Update.update bindings
  |> Program.withConsoleTrace
  |> Program.runWindowWithConfig { ElmConfig.Default with LogConsole = true } (MainWindow())
