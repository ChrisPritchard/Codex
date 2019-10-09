module Core

type [<CLIMutable>]Novel = {
    title: string
    acts: Act list
    }
and [<CLIMutable>]Act = {
    title: string
    chapters: Chapter list
    }
and [<CLIMutable>]Chapter = {
    title: string
    scenes: Scene list
    }
and [<CLIMutable>]Scene = {
    wordCount: int
    xamlContent: string
    }