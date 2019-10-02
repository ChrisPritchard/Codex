open System
open Elmish
open Elmish.WPF

open Codex.Model
open Codex.Views

let init _ = { content = "" }, Cmd.none

let bindings _ : Binding<CurrentScene, Messages> list = []

[<EntryPoint; STAThread>]
let main _ =
  Program.mkProgramWpf init Update.update bindings
  |> Program.runWindowWithConfig ElmConfig.Default (MainWindow())
