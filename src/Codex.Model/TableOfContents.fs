module TableOfContents

open Elmish
open Elmish.WPF
open Codex.Model.Core

type Message =
    | CloseTableOfContents
    | PartMessage of id: Part * message: PartMessage
and PartMessage =
    // grouping messages
    | AddGrouping
    | AddContent
    | ChildMessage of id: Part * message: PartMessage
    // content messages
    | SetIsPartOfStory of bool

let update message model =
    match message with
    | _ ->
        // messages not covered above are probably used by parent windows (e.g. main for close)
        model, Cmd.none
     
let rec private partBindings _ : Binding<Part, PartMessage> list = [
    "IsContent" |> Binding.oneWay (function :? Content -> true | _ -> false)

    // Grouping bindings
    "Title" |> Binding.oneWay (function :? Grouping as m -> m.title | _ -> "") // hardcore point notation ftw
    "Parts" |> Binding.subModelSeq(
         (function :? Grouping as m -> m.parts | _ -> []),
         snd,
         id,
         ChildMessage,
         partBindings)

    // Content bindings
    "WordCount" |> Binding.oneWay (function :? Content as m -> m.wordCount | _ -> 0)
    "IsPartOfStory" |> Binding.twoWay ((fun (p: Part) -> match p with :? Content as m -> m.isPartOfStory | _ -> false), SetIsPartOfStory)
    ]

let bindings _ : Binding<Grouping, Message> list = [
        "Title" |> Binding.oneWay (fun m -> m.title)
        "CloseWindow" |> Binding.cmd CloseTableOfContents
        "Parts" |> Binding.subModelSeq(
            (fun m -> m.parts),
            snd,
            id,
            PartMessage,
            partBindings)
    ]