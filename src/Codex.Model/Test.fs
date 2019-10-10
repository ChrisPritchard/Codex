namespace Codex.Model

module Core =

    type Part = interface end

    type Grouping = {
        title: string
        parts: Part list
        }
        with interface Part

    type Content = {
            wordCount: int
            xamlContent: string
            isPartOfStory: bool
        }
        with interface Part

    //let test = {
    //        title = "test"
    //        parts = [
    //            { title = "test2"; parts = [] }
    //            { wordCount = 0; xamlContent = ""; isPartOfStory = true }
    //        ]
    //    }