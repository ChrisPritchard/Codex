open System
open Elmish
open Elmish.WPF

open Codex.Model
open Codex.Views

let init _ = { xamlContent = ""; wordCount = 20 }, Cmd.none

let bindings _ = [
        "XamlContent" |> Binding.twoWay ((fun m -> m.xamlContent), UpdateXamlContent) 
        "GetWordCount" |> Binding.twoWay ((fun m -> m.wordCount), UpdateWordCount)
        "ReadWordCount" |> Binding.oneWay ((fun m -> m.wordCount))
    ]

[<EntryPoint; STAThread>]
let main _ =
  Program.mkProgramWpf init Update.update bindings
  |> Program.runWindowWithConfig ElmConfig.Default (MainWindow())
