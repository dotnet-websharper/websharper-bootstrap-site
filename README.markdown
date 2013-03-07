# websharper-bootstrap-site

Clone this sample application skeleton to quickly get started with
[WebSharper](http://websharper.com).  This is an alpha (pre-release)
version based on WebSharper 2.5.2-alpha.  Current features:

  * Layout using [Twitter Bootstrap](http://twitter.github.com/bootstrap)

  * Uses [FAKE](http://github.com/fsharp/FAKE) for build automation

  * Builds on [AppHarbor](http://appharbor.com)

  * In fact, a there is a live copy: [http://websharper-bootstrap-site.apphb.com](http://websharper-bootstrap-site.apphb.com)

  * Supports the F# 3.0 runtime

## Building

On the first checkout, run `MSBuild.exe` in the root directory of the
checkout.  This will download the necessary dependencies and perform
an initial build.  Then you can open `Build/VS2012/Site.sln` in
VisualSudio, or edit the files manually.

For example, using PowerShell:

    set-alias msb C:\WINDOWS\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe
    msb

## Bugs

Please use the Bitbucket [tracker](http://bitbucket.org/IntelliFactory/websharper-bootstrap-site/issues).
