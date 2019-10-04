namespace Codex.Model

type Novel = {
    title: string option
    synopsis: string
    acts: Act list
}
and Act = {
    title: string option
    synopsis: string
    chapters: Chapter list
}
and Chapter = {
    title: string option
    synopsis: string
    scenes: Scene list
}
and Scene = {
    outline: string
    content: string
    wordCount: int
}

type Test = {
    xaml: string
    pageCount: int
}