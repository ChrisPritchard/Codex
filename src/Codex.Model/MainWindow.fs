module MainWindow

open Elmish
open Elmish.WPF

type CodexModel = {
    sceneEditor: SceneEditor.Model option
    tableOfContents: TableOfContents.Model option
}

type Message = 
    | ShowSceneEditor
    | SceneEditorMessage of SceneEditor.Message
    | ShowTableOfContents
    | TableOfContentsMessage of TableOfContents.Message

let update message model =
    match message, model.sceneEditor, model.tableOfContents with
    | ShowSceneEditor, None, _ ->
        { model with sceneEditor = Some { title = "Current Scene"; xamlContent = ""; wordCount = 0 } }, Cmd.none
    | SceneEditorMessage SceneEditor.CloseSceneEditor, Some _, _ ->
        { model with sceneEditor = None }, Cmd.none
    | SceneEditorMessage m, Some subWindow, _ -> 
        let subWindow, subMessage = SceneEditor.update m subWindow
        { model with sceneEditor = Some subWindow }, Cmd.map SceneEditorMessage subMessage

    | ShowTableOfContents, _, None ->
        { model with tableOfContents = Some { title = "My Custom Novel" } }, Cmd.none
    | TableOfContentsMessage TableOfContents.CloseTableOfContents, _, Some _ ->
        { model with tableOfContents = None }, Cmd.none
    // no other messages on this window at this time
    //| TableOfContentsMessage m, _, Some subWindow -> 
    //    let subWindow, subMessage = TableOfContents.update m subWindow
    //    { model with tableOfContents = Some subWindow }, Cmd.map TableOfContentsMessage subMessage

    | _ ->
        // invalid command, model combo
        model, Cmd.none