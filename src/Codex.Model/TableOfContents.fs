﻿module TableOfContents

open Elmish
open Elmish.WPF
open Codex.Model.Core

type Message =
    | CloseTableOfContents
    | PartMessage of id: string * message: PartMessage
and PartMessage =
    // grouping messages
    | AddGrouping
    | AddContent
    | ChildMessage of id: string * message: PartMessage
    // content messages
    | SetIsPartOfStory of bool

let update message model =
    match message with
    | _ ->
        // messages not covered above are probably used by parent windows (e.g. main for close)
        model, Cmd.none

let getIdForParts = function Grouping g -> g.title | Content c -> c.title
     
let rec private partBindings _ : Binding<Part, PartMessage> list = [
    "IsContent" |> Binding.oneWay (function Content _ -> true | _ -> false)

    // Grouping bindings
    "Title" |> Binding.oneWay (function Grouping m -> m.title | _ -> "")
    "Parts" |> Binding.subModelSeq(
         (function Grouping m -> m.parts | _ -> []),
         snd,
         getIdForParts,
         ChildMessage,
         partBindings)

    // Content bindings
    "WordCount" |> Binding.oneWay (function Content m -> m.wordCount | _ -> 0)
    "IsPartOfStory" |> Binding.twoWay ((fun (p: Part) -> match p with Content m -> m.isPartOfStory | _ -> false), SetIsPartOfStory)
    ]

let bindings _ : Binding<Grouping, Message> list = [
        "Title" |> Binding.oneWay (fun m -> m.title)
        "CloseWindow" |> Binding.cmd CloseTableOfContents
        "Parts" |> Binding.subModelSeq(
            (fun m -> m.parts),
            snd,
            getIdForParts,
            PartMessage,
            partBindings)
    ]