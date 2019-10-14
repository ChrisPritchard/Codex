module TestNovel

open Codex.Model.Core

let testNovel = {
        title = "My Test Novel"
        parts = [
            Grouping {
                title = "Act 1"
                parts = [
                    Grouping {
                        title = "Chapter 1"
                        parts = [
                            Content { title = "Scene 1"; wordCount = 20; xamlContent = ""; isPartOfStory = true }
                            Content { title = "Scene 2"; wordCount = 20; xamlContent = ""; isPartOfStory = true }
                            Content { title = "Scene 3"; wordCount = 20; xamlContent = ""; isPartOfStory = true }
                        ]
                    }
                    Grouping {
                        title = "Chapter 2"
                        parts = [
                            Content { title = "Scene 4"; wordCount = 20; xamlContent = ""; isPartOfStory = true }
                            Content { title = "Scene 5"; wordCount = 20; xamlContent = ""; isPartOfStory = true }
                            Content { title = "Scene 6"; wordCount = 20; xamlContent = ""; isPartOfStory = true }
                        ]
                    }
                    Grouping {
                        title = "Chapter 3"
                        parts = [
                            Content { title = "Scene 7"; wordCount = 20; xamlContent = ""; isPartOfStory = true }
                            Content { title = "Scene 8"; wordCount = 20; xamlContent = ""; isPartOfStory = true }
                            Content { title = "Scene 9"; wordCount = 20; xamlContent = ""; isPartOfStory = true }
                        ]
                    }
                ]
            }
            Grouping {
                title = "Act 2"
                parts = [
                    Grouping {
                        title = "Chapter 4"
                        parts = [
                            Content { title = "Scene 10"; wordCount = 20; xamlContent = ""; isPartOfStory = true }
                            Content { title = "Scene 11"; wordCount = 20; xamlContent = ""; isPartOfStory = true }
                            Content { title = "Scene 12"; wordCount = 20; xamlContent = ""; isPartOfStory = true }
                        ]
                    }
                    Grouping {
                        title = "Chapter 5"
                        parts = [
                            Content { title = "Scene 13"; wordCount = 20; xamlContent = ""; isPartOfStory = true }
                            Content { title = "Scene 14"; wordCount = 20; xamlContent = ""; isPartOfStory = true }
                            Content { title = "Scene 15"; wordCount = 20; xamlContent = ""; isPartOfStory = true }
                        ]
                    }
                    Grouping {
                        title = "Chapter 6"
                        parts = [
                            Content { title = "Scene 16"; wordCount = 20; xamlContent = ""; isPartOfStory = true }
                            Content { title = "Scene 17"; wordCount = 20; xamlContent = ""; isPartOfStory = true }
                            Content { title = "Scene 18"; wordCount = 20; xamlContent = ""; isPartOfStory = true }
                        ]
                    }
                ]
            }
            Grouping {
                title = "Act 3"
                parts = [
                    Grouping {
                        title = "Chapter 7"
                        parts = [
                            Content { title = "Scene 19"; wordCount = 20; xamlContent = ""; isPartOfStory = true }
                            Content { title = "Scene 20"; wordCount = 20; xamlContent = ""; isPartOfStory = true }
                            Content { title = "Scene 21"; wordCount = 20; xamlContent = ""; isPartOfStory = true }
                        ]
                    }
                    Grouping {
                        title = "Chapter 8"
                        parts = [
                            Content { title = "Scene 22"; wordCount = 20; xamlContent = ""; isPartOfStory = true }
                            Content { title = "Scene 23"; wordCount = 20; xamlContent = ""; isPartOfStory = true }
                            Content { title = "Scene 24"; wordCount = 20; xamlContent = ""; isPartOfStory = true }
                        ]
                    }
                    Grouping {
                        title = "Chapter 9"
                        parts = [
                            Content { title = "Scene 25"; wordCount = 20; xamlContent = ""; isPartOfStory = true }
                            Content { title = "Scene 26"; wordCount = 20; xamlContent = ""; isPartOfStory = true }
                            Content { title = "Scene 27"; wordCount = 20; xamlContent = ""; isPartOfStory = true }
                        ]
                    }
                ]
            }
        ]
    }

