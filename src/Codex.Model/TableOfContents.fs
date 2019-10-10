module TableOfContents

open Elmish
open Elmish.WPF
open Codex.Model.Core

type Message =
    | CloseTableOfContents

let update message model =
    match message with
    | _ ->
        // messages not covered above are probably used by parent windows (e.g. main for close)
        model, Cmd.none
     
let rec private partBindings _ : Binding<(obj * Part), Message> list = [
    // Grouping bindings
    "Title" |> Binding.oneWay (snd >> function :? Grouping as m -> m.title | _ -> "") // hardcore point notation ftw
    "Parts" |> Binding.subModelSeq(
         (snd >> function :? Grouping as m -> m.parts | _ -> []),
         id,
         partBindings)

    // Content bindings
    "WordCount" |> Binding.oneWay (snd >> function :? Content as m -> m.wordCount | _ -> 0)
    "IsPartOfStory" |> Binding.oneWay (snd >> function :? Content as m -> m.isPartOfStory | _ -> false)
    ]

let bindings _ : Binding<Grouping, Message> list = [
        "Title" |> Binding.oneWay (fun m -> m.title)
        "CloseWindow" |> Binding.cmd CloseTableOfContents
        "Parts" |> Binding.subModelSeq(
             (fun m -> m.parts),
             id,
             partBindings)
    ]