module MainWindow

open Elmish
open Elmish.WPF

type CodexModel = {
    sceneEditor: SceneEditor.Model option
}

type Message = 
    | SceneEditorMessage of SceneEditor.Message

let update message model =
    match message, model.sceneEditor with
    | SceneEditorMessage SceneEditor.CloseSceneEditor, Some _ ->
        { model with sceneEditor = None }, Cmd.none
    | SceneEditorMessage m, Some subWindow -> 
        let subWindow, subMessage = SceneEditor.update m subWindow
        { model with sceneEditor = Some subWindow }, Cmd.map SceneEditorMessage subMessage
    | _ ->
        // invalid command, model combo
        model, Cmd.none