module MainWindow

open Elmish
open Elmish.WPF
open System
open System.Windows
open System.IO
open System.Xml.Linq
open Microsoft.Win32
open Codex.Model.Core

let exportFileFilter = "RTF (*.rtf)|*.rtf|Plain Text (*.txt)|*.txt|XAML Pack (*.xaml)|*.xaml"
let projectFileFilter = "Codex Project (*.cdxml)|*.cdxml";

type CodexModel = {
    sceneEditor: SceneEditor.Model option
    tableOfContents: Codex.Model.Core.Grouping option
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

let openFile filter fileName = 
    Application.Current.Dispatcher.Invoke(fun () ->
        let dialog = OpenFileDialog (Filter = filter, FileName = fileName)
        let result = dialog.ShowDialog ()
        if result.HasValue && result.Value
            then LoadFileSelected dialog.FileName
            else FileSelectCanceled
    )

let saveFile filter fileName =
    Application.Current.Dispatcher.Invoke(fun () ->
        let dialog = SaveFileDialog (Filter = filter, FileName = fileName)
        let result = dialog.ShowDialog ()
        if result.HasValue && result.Value
            then SaveFileSelected dialog.FileName
            else FileSelectCanceled
    )

let saveCurrentModel model fileName =
    let rec xmlForPart p =
        match p with
        | Grouping g ->
            let childrenXml = g.parts |> List.map xmlForPart |> String.concat ""
            sprintf "<grouping title=\"%s\">%s</grouping>" g.title childrenXml
        | Content c ->
            sprintf "<content title=\"%s\" isPartOfStory=\"%b\" wordCount=\"%i\"><![CDATA[%s]]></content>" 
                c.title c.isPartOfStory c.wordCount c.xamlContent
    let xml = XDocument.Parse(xmlForPart model)
    use stream = File.Create fileName
    xml.Save stream
    ()

let loadSavedModel fileName =
    try
        use stream = File.OpenRead fileName
        let doc = XDocument.Load stream

        let rec parser (node: XElement) = 
            match node.Name.LocalName with 
            | "grouping" ->
                let children = node.Elements () |> Seq.map parser |> Seq.toList
                Grouping { 
                    title = node.Attribute(XName.Get("title")).Value
                    parts = children 
                }
            | "content" ->
                Content { 
                    title = node.Attribute(XName.Get("title")).Value
                    isPartOfStory = bool.Parse (node.Attribute(XName.Get("isPartOfStory")).Value)
                    wordCount = Int32.Parse (node.Attribute(XName.Get("wordCount")).Value)
                    xamlContent = node.Value
                }
            | _ -> failwith "invalid xml encountered while parsing codex document"

        let model = parser doc.Root
        match model with
        | Grouping g ->
            Ok g
        | Content _ ->
            Error "invalid xml structure"
    with
    | ex ->
        Error ex.Message

let update message model =
    match message, model.sceneEditor, model.tableOfContents with
    | LoadNovel, _, _ -> 
        model, Cmd.OfFunc.perform (openFile projectFileFilter) "" id // TODO set filename
    | SaveNovel, _, _ -> 
        model, Cmd.OfFunc.perform (saveFile projectFileFilter) "" id // TODO set filename
    | SaveFileSelected fileName, _, Some subWindow ->
        saveCurrentModel (Grouping subWindow) fileName
        model, Cmd.none
    | LoadFileSelected fileName, _, _->
        match loadSavedModel fileName with
        | Ok g ->
            { model with tableOfContents = Some g }, Cmd.none
        | Error s ->
            MessageBox.Show (sprintf "Loading an existing project failed with error %s" s) |> ignore
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