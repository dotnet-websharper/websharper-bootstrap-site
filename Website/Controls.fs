module Site.Controls

open IntelliFactory.WebSharper

[<Sealed>]
type EntryPoint() =
    inherit Web.Control()

    [<JavaScript>]
    override this.Body =
        Client.EntryPoint() :> _
