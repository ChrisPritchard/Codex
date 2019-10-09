module MainWindow

open Elmish
open Elmish.WPF

open System.Windows
open System.IO
open System.Xml.Serialization
open Microsoft.Win32

open Core

let fileFilter = "RTF (*.rtf)|*.rtf|Plain Text (*.txt)|*.txt|XAML Pack (*.xaml)|*.xaml"

type CodexModel = {
    sceneEditor: SceneEditor.Model option
    tableOfContents: Core.Novel option
}

type Message = 
    | LoadNovel
    | SaveNovel
    | LoadFileSelected of fileName:string
    | SaveFileSelected of fileName:string
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
            then LoadFileSelected dialog.FileName
            else FileSelectCanceled
    )

let saveFile fileName =
    Application.Current.Dispatcher.Invoke(fun () ->
        let dialog = SaveFileDialog (Filter = fileFilter, FileName = fileName)
        let result = dialog.ShowDialog ()
        if result.HasValue && result.Value
            then SaveFileSelected dialog.FileName
            else FileSelectCanceled
    )

let saveCurrentModel model fileName =
    let serialiser = new XmlSerializer (typeof<Novel>)
    use stream = File.Create fileName
    serialiser.Serialize(stream, model) // TODO save xaml text content
    ()

let update message model =
    match message, model.sceneEditor, model.tableOfContents with
    | LoadNovel, _, _ -> 
        model, Cmd.OfFunc.perform openFile "" id // TODO set filename
    | SaveNovel, _, _ -> 
        model, Cmd.OfFunc.perform saveFile "" id // TODO set filename
    | SaveFileSelected fileName, _, Some subWindow ->
        saveCurrentModel subWindow fileName
        model, Cmd.none

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

    "LoadNovel" |> Binding.cmd LoadNovel
    "SaveNovel" |> Binding.cmd SaveNovel

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
        TableOfContents.bindings,
        (Codex.Views.TableOfContents))

    ]