module Site.Dependencies

open IntelliFactory.WebSharper

[<Require(typeof<JQuery.Resources.JQuery>)>]
[<Sealed>]
type TwitterBootstrap() =
    inherit Resources.BaseResource("//netdna.bootstrapcdn.com/twitter-bootstrap/2.3.1/",
        "js/bootstrap.min.js", "css/bootstrap-combined.min.css")

[<assembly: Require(typeof<TwitterBootstrap>)>]
do ()
