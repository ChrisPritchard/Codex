open System
open Elmish
open Elmish.WPF

open Codex.Model
open Codex.Views

let init _ = { title = None; synopsis = ""; acts = [] }, Cmd.none

let bindings _ = [
        Binding.cmd Quit "Quit"
    ]

[<EntryPoint; STAThread>]
let main _ =
  Program.mkProgramWpf init Update.update bindings
  |> Program.runWindowWithConfig ElmConfig.Default (MainWindow())
