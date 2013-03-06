module Site.Skin

open System.Web
open IntelliFactory.Html
open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Sitelets

type Page =
    {
        Body : list<Content.HtmlElement>
        Menu : list<Content.HtmlElement>
        Project : string
        Title : string
    }

let MainTemplate =
    let path = HttpContext.Current.Server.MapPath("~/Main.html")
    Content.Template<Page>(path)
        .With("body", fun x -> x.Body)
        .With("menu", fun x -> x.Menu)
        .With("project", fun x -> x.Project)
        .With("title", fun x -> x.Title)

let WithTemplate title menu body : Content<Actions.Action> =
    Content.WithTemplate MainTemplate <| fun context ->
        {
            Body =
                Div [Style "display:none"] -< [new Controls.EntryPoint()]
                :: body context
            Menu = menu context
            Project = "Site"
            Title = title
        }
