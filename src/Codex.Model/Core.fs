namespace Codex.Model

module Core =

    type Part =
    | Grouping of Grouping
    | Content of Content

    and Grouping = {
        title: string
        parts: Part list
        }

    and Content = {
        wordCount: int
        xamlContent: string
        isPartOfStory: bool
        }