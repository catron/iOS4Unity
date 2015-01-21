#r @"packages/FAKE.3.5.4/tools/FakeLib.dll"
#load "build-helpers.fsx"
open Fake
open System
open System.IO
open System.Linq
open BuildHelpers
open Fake.XamarinHelper

let version = "1.0.0"
let project = "iOS4Unity"
let files = [|"Assets/iOS4Unity/ObjC.cs"|]

Target "clean" (fun () ->
    Exec "rm" "-Rf iOS4Unity/bin iOS4Unity/obj"
)

Target "build" (fun () ->
    let output = Path.Combine(project, "bin", "Release")
    let csproj = Path.Combine(project, project + ".csproj")
    MSBuild output "Build" [ ("Configuration", "Release") ] [ csproj ] |> ignore
)

Target "unity" (fun () ->
    let text = ""
    for f in files do
        text = text + f + " "
    Unity text
)

RunTarget()
