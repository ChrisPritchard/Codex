﻿namespace Codex.Model

type Messages =
    | Save 
    | Load
    | Quit
    | UpdateXamlContent of string
    | UpdateWordCount of int