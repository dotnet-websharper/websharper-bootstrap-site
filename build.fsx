#if BOOT
open Fake
module FB = Fake.Boot
FB.Prepare {
    FB.Config.Default __SOURCE_DIRECTORY__ with
        NuGetDependencies =
            let ( ! ) x = FB.NuGetDependency.Create x
            [
                 !"WebSharper"
                 !"IntelliFactory.Build"
                 !"FAKE"
            ]
}
#else
#load ".build/boot.fsx"

open Fake
open System.IO
module B = IntelliFactory.Build.CommonBuildSetup
module NG = IntelliFactory.Build.NuGetUtils
module X = IntelliFactory.Build.XmlGenerator

let ( +/ ) a b = Path.Combine(a, b)
let RootDir = __SOURCE_DIRECTORY__
let T n f = Target n f; n

let Config : B.BuildConfiguration =
    {
        ConfigurationName = "Release"
        Debug = false
        FrameworkVersion = B.Net40
        NuGetDependencies = new NuGet.PackageDependencySet(B.Net40.ToFrameworkName(), [])
    }

let DefProject (name: string) (ext: string) : B.Project =
    {
        BuildConfigurations = [Config]
        MSBuildProjectFilePath = Some (RootDir +/ name +/ (name + ext))
        Name = name
    }

let Solution =
    B.Solution
        (
            RootDir,
            Projects =
                [
                    DefProject "Website" ".fsproj"
                    DefProject "Web" ".csproj"
                ]
        )

type PackageInfo =
    {
        WebSharperHome : string
        FSharpCoreLocation : string
    }

let GetPackageInfo () =
    let repo = NG.LocalRepository.Create (RootDir +/ "packages")
    let ws = Option.get (NG.FindPackage repo "WebSharper")
    let fake = Option.get (NG.FindPackage repo "FAKE")
    {
        WebSharperHome =
            repo.Path +/ repo.PathResolver.GetPackageDirectory(ws) +/ "tools" +/ "net40"
        FSharpCoreLocation =
            repo.Path +/ repo.PathResolver.GetPackageDirectory(fake) +/ "tools" +/ "FSharp.Core.dll"
    }

let GenerateWebSharperTargets = T "GenerateWebSharperTargets" <| fun () ->
    let info = GetPackageInfo ()
    let NS = "http://schemas.microsoft.com/developer/msbuild/2003"
    let E name = X.Element.Create(name, NS)
    let xml =
        E "Project" + ["ToolsVersion", "4.0"] - [
            E "PropertyGroup" - [
                E "WebSharperHome" - [X.TextNode info.WebSharperHome]
                E "FSharpCoreLocation" - [X.TextNode info.FSharpCoreLocation]
            ]
        ]
    let f = RootDir +/ ".build" +/ "WebSharper.targets"
    X.WriteFile f xml
    tracefn "Writing %s" f

let Build = T "Build" <| fun () ->
    Solution.MSBuild()
    |> Async.RunSynchronously

let Clean = T "Clean" <| fun () ->
    Solution.MSBuild {
        BuildConfiguration = None
        Targets = ["Clean"]
        Properties = Map.empty
    }
    |> Async.RunSynchronously

let Prepare = T "Prepare" <| fun () ->
    IntelliFactory.Build.CommonBuildSetup.Prepare (tracefn "%s") RootDir

GenerateWebSharperTargets ==> Build

RunTargetOrDefault Build

#endif
