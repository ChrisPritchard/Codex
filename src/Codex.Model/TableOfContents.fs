module TableOfContents

open Elmish
open Elmish.WPF

type Novel = {
    title: string
    acts: Act list
    }
and Act = {
    title: string
    chapters: Chapter list
    }
and Chapter = {
    title: string
    scenes: Scene list
    }
and Scene = {
    wordCount: int
    }

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

let sceneBindings _ : Binding<(((Novel * Act) * Chapter) * Scene), Message> list = [
    "wordCount" |> Binding.oneWay (fun (_, m) -> m.wordCount)
    ]

let chapterBindings _ : Binding<((Novel * Act) * Chapter), Message> list = [
    "Title" |> Binding.oneWay (fun (_, m) -> m.title)
    "Scenes" |> Binding.subModelSeq(
         (fun (_, m) -> m.scenes),
         id,
         sceneBindings)
    ]
     
let actBindings _ : Binding<(Novel * Act), Message> list = [
        "Title" |> Binding.oneWay (fun (_, m) -> m.title)
        "Scenes" |> Binding.subModelSeq(
             (fun (_, m) -> m.chapters),
             id,
             chapterBindings)
    ]

let novelBindings _ : Binding<Novel, Message> list = [
        "Title" |> Binding.oneWay (fun m -> m.title)
        "CloseWindow" |> Binding.cmd CloseTableOfContents
        "Acts" |> Binding.subModelSeq(
             (fun m -> m.acts),
             id,
             actBindings)
    ]