module TableOfContents

open Elmish
open Elmish.WPF
open Core

type Message =
    | CloseTableOfContents
    | AddAct
    | AddChapter of parent: Act
    | AddScene of parent: Chapter
    | EditScene of Scene

let update message model =
    match message with
    | _ ->
        // messages not covered above are probably used by parent windows (e.g. main for close)
        model, Cmd.none

let private sceneBindings _ : Binding<(((Novel * Act) * Chapter) * Scene), Message> list = [
    "WordCount" |> Binding.oneWay (fun (_, m) -> m.wordCount)
    ]

let private chapterBindings _ : Binding<((Novel * Act) * Chapter), Message> list = [
    "Title" |> Binding.oneWay (fun (_, m) -> m.title)
    "Scenes" |> Binding.subModelSeq(
         (fun (_, m) -> m.scenes),
         id,
         sceneBindings)
    ]
     
let private actBindings _ : Binding<(Novel * Act), Message> list = [
        "Title" |> Binding.oneWay (fun (_, m) -> m.title)
        "Chapters" |> Binding.subModelSeq(
             (fun (_, m) -> m.chapters),
             id,
             chapterBindings)
    ]

let bindings _ : Binding<Novel, Message> list = [
        "Title" |> Binding.oneWay (fun m -> m.title)
        "CloseWindow" |> Binding.cmd CloseTableOfContents
        "Acts" |> Binding.subModelSeq(
             (fun m -> m.acts),
             id,
             actBindings)
    ]