module Core

type Novel = {
    title: string
    acts: Act list
    }
and Act = {
    title: string
    chapters: Chapter list
    }
and Chapter = {
    title: string
    scenes: Scene list
    }
and Scene = {
    wordCount: int
    xamlContent: string
    }