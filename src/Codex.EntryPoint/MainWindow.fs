module MainWindow

open Elmish
open Elmish.WPF

open System.Windows
open Microsoft.Win32

let fileFilter = "RTF (*.rtf)|*.rtf|Plain Text (*.txt)|*.txt|XAML Pack (*.xaml)|*.xaml"

type CodexModel = {
    sceneEditor: SceneEditor.Model option
    tableOfContents: TableOfContents.Novel option
}

type Message = 
    | FileSelected of fileName:string
    | FileSelectCanceled

    | ShowSceneEditor
    | SceneEditorMessage of SceneEditor.Message
    | ShowTableOfContents
    | TableOfContentsMessage of TableOfContents.Message

let openFile fileName = 
    Application.Current.Dispatcher.Invoke(fun () ->
        let dialog = OpenFileDialog (Filter = fileFilter, FileName = fileName)
        let result = dialog.ShowDialog ()
        if result.HasValue && result.Value
            then FileSelected dialog.FileName
            else FileSelectCanceled
    )

let saveFile fileName =
    Application.Current.Dispatcher.Invoke(fun () ->
        let dialog = SaveFileDialog (Filter = fileFilter, FileName = fileName)
        let result = dialog.ShowDialog ()
        if result.HasValue && result.Value
            then FileSelected dialog.FileName
            else FileSelectCanceled
    )

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
            tableOfContents = Some TestNovel.testNovel }, Cmd.none
    | TableOfContentsMessage TableOfContents.CloseTableOfContents, _, Some _ ->
        { model with tableOfContents = None }, Cmd.none
    // no other messages on this window at this time
    //| TableOfContentsMessage m, _, Some subWindow -> 
    //    let subWindow, subMessage = TableOfContents.update m subWindow
    //    { model with tableOfContents = Some subWindow }, Cmd.map TableOfContentsMessage subMessage

    | _ ->
        // invalid command, model combo
        model, Cmd.none

let bindings _ = [

    "ShowSceneEditor" |> Binding.cmd ShowSceneEditor
    "SceneEditor" |> Binding.subModelWin (
         (fun m -> m.sceneEditor |> WindowState.ofOption), 
         snd, 
         SceneEditorMessage,
         SceneEditor.bindings,
         (Codex.Views.SceneEditor))

    "ShowTableOfContents" |> Binding.cmd ShowTableOfContents
    "TableOfContents" |> Binding.subModelWin (
        (fun m -> m.tableOfContents |> WindowState.ofOption), 
        snd, 
        TableOfContentsMessage,
        TableOfContents.novelBindings,
        (Codex.Views.TableOfContents))

    ]