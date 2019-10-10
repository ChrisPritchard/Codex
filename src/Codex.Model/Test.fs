namespace Codex.Model

module Core =

    type Part = 
        | Grouping of Grouping
        | Content of Content
        with 
            member p.AsGrouping = match p with Grouping g -> (true, g) | _ -> (false, Unchecked.defaultof<_>)
            member p.AsContent = match p with Content c -> (true, c) | _ -> (false, Unchecked.defaultof<_>)
    and Grouping = {
        title: string
        parts: Part list
        }
    and Content = {
        wordCount: int
        xamlContent: string
        }