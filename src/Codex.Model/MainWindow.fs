module MainWindow

open Elmish

type CodexModel = {
    sceneEditor: SceneEditor.Model
}

type Message = 
    | SceneEditorMessage of SceneEditor.Message

let update message model =
    match message with
    | SceneEditorMessage m -> 
        let subWindow, subMessage = SceneEditor.update m model.sceneEditor
        { model with sceneEditor = subWindow }, Cmd.map SceneEditorMessage subMessage