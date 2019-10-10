namespace Codex.Model

module Core =

    type Part = 
        abstract member title: string

    type Grouping(title) = 
        interface Part with 
            member _.title = title
        member _.Parts: Part list = []

    type Content(title) = 
        interface Part with 
            member _.title = title
        member _.wordCount: int = 0
        member _.xamlContent: string = ""
        member _.partOfStory: bool = false