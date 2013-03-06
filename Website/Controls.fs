module Site.Controls

open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Html

[<Sealed>]
type EntryPoint() =
    inherit Web.Control()

    [<JavaScript>]
    override this.Body =
        Client.EntryPoint()
        Span [] :> _

[<Sealed>]
type SampleButtons () =
    inherit Web.Control()

    [<JavaScript>]
    override this.Body =
        Client.SampleButtons() :> _

