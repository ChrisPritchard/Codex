module MainWindow

open Elmish
open Elmish.WPF

type CodexModel = {
    sceneEditor: SceneEditor.Model option
    tableOfContents: TableOfContents.Novel option
}

type Message = 
    | ShowSceneEditor
    | SceneEditorMessage of SceneEditor.Message
    | ShowTableOfContents
    | TableOfContentsMessage of TableOfContents.Message

let testNovel: TableOfContents.Novel = {
        title = "My Test Novel"
        acts = [
            {
                title = "Act 1"
                chapters = [
                    {
                        title = "Chapter 1"
                        scenes = [
                            { wordCount = 20 }
                            { wordCount = 20 }
                            { wordCount = 20 }
                        ]
                    }
                    {
                        title = "Chapter 2"
                        scenes = [
                            { wordCount = 20 }
                            { wordCount = 20 }
                            { wordCount = 20 }
                        ]
                    }
                    {
                        title = "Chapter 3"
                        scenes = [
                            { wordCount = 20 }
                            { wordCount = 20 }
                            { wordCount = 20 }
                        ]
                    }
                ]
            }
            {
                title = "Act 2"
                chapters = [
                    {
                        title = "Chapter 4"
                        scenes = [
                            { wordCount = 20 }
                            { wordCount = 20 }
                            { wordCount = 20 }
                        ]
                    }
                    {
                        title = "Chapter 5"
                        scenes = [
                            { wordCount = 20 }
                            { wordCount = 20 }
                            { wordCount = 20 }
                        ]
                    }
                    {
                        title = "Chapter 6"
                        scenes = [
                            { wordCount = 20 }
                            { wordCount = 20 }
                            { wordCount = 20 }
                        ]
                    }
                ]
            }
            {
                title = "Act 3"
                chapters = [
                    {
                        title = "Chapter 7"
                        scenes = [
                            { wordCount = 20 }
                            { wordCount = 20 }
                            { wordCount = 20 }
                        ]
                    }
                    {
                        title = "Chapter 8"
                        scenes = [
                            { wordCount = 20 }
                            { wordCount = 20 }
                            { wordCount = 20 }
                        ]
                    }
                    {
                        title = "Chapter 9"
                        scenes = [
                            { wordCount = 20 }
                            { wordCount = 20 }
                            { wordCount = 20 }
                        ]
                    }
                ]
            }
        ]
    }

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
        { model with 
            tableOfContents = Some testNovel }, Cmd.none
    | TableOfContentsMessage TableOfContents.CloseTableOfContents, _, Some _ ->
        { model with tableOfContents = None }, Cmd.none
    // no other messages on this window at this time
    //| TableOfContentsMessage m, _, Some subWindow -> 
    //    let subWindow, subMessage = TableOfContents.update m subWindow
    //    { model with tableOfContents = Some subWindow }, Cmd.map TableOfContentsMessage subMessage

    | _ ->
        // invalid command, model combo
        model, Cmd.none