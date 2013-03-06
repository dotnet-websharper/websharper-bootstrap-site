module Site.Content

open IntelliFactory.Html
open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Sitelets

let ( => ) text url =
    A [HRef url] -< [Text text]

let Menu (ctx: Context<Actions.Action>) =
    [
        LI ["Home" => ctx.Link Actions.Home]
        LI ["About" => ctx.Link Actions.About]
        LI ["Contact" => ctx.Link Actions.Contact]
    ]

let HomePage =
    Skin.WithTemplate "Home" Menu <| fun ctx ->
        [
            Div [Class "hero-unit"] -< [Text "Welcome"]
            Div [Class "row"] -< [
                yield Div [Class "span2"] -< [
                    new Controls.SampleButtons()
                ]
                for n in 1 .. 3 do
                    yield Div [Class "span2"] -< [
                        ("Detail " + string n) =>
                            ctx.Link (Actions.Detail n)
                    ]
            ]
        ]

let AboutPage =
    Skin.WithTemplate "About" Menu <| fun ctx ->
        [
            Div [Text "About Us"]
        ]

let ContactPage =
    Skin.WithTemplate "Contact" Menu <| fun ctx ->
        [
            Div [Text "Contact"]
        ]

let Detail (n: int) =
    Skin.WithTemplate "Detail" Menu <| fun ctx ->
        [
            Div [Text ("Detail: " + string n)]
        ]

let Main =
    Sitelet.Sum [
        Sitelet.Content "/" Actions.Home HomePage
        Sitelet.Content "/about" Actions.About AboutPage
        Sitelet.Content "/contact" Actions.Contact ContactPage
        Sitelet.Infer (function
            | Actions.Home -> HomePage
            | Actions.About -> AboutPage
            | Actions.Contact -> ContactPage
            | Actions.Detail n -> Detail n)
    ]
