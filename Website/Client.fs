module Site.Client

open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Html

[<JavaScript>]
let EntryPoint () =
    let ( @ ) a b =
        Html.Default.NewAttr a b
    let Button (text: string) =
        Button [Attr.Type "button"; Attr.Class "btn btn-primary"] -< [Text text]
    let Group (xs: list<IPagelet>) =
        Div [Attr.Class "btn-group"; "data-toggle" @ "buttons-radio"] -< xs
    let Content = Div []
    Div [
        Group [
            Button "Left"
            |>! OnClick (fun _ _ -> Content.Text <- "Left")
            Button "Middle"
            |>! OnClick (fun _ _ -> Content.Text <- "Middle")
            Button "Right"
            |>! OnClick (fun _ _ -> Content.Text <- "Right")
        ]
        Content
    ]
