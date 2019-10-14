namespace Codex.Model

module Core =

    type Part =
    | Grouping of Grouping
    | Content of Content

    and [<CLIMutable>]Grouping = {
        title: string
        parts: Part list
        }

    and [<CLIMutable>]Content = {
        title: string
        wordCount: int
        xamlContent: string
        isPartOfStory: bool
        }