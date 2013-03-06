namespace Site

open IntelliFactory.Html
open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Sitelets

[<Sealed>]
type Website() =
    interface IWebsite<Actions.Action> with
        member this.Sitelet = Content.Main
        member this.Actions = [Actions.Home; Actions.About; Actions.Contact]

[<assembly: WebsiteAttribute(typeof<Website>)>]
do ()
