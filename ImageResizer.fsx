
#r @"C:\Shared Resources\ImageResizer\ImageResizer.4.2.5\lib\net45\ImageResizer.dll"

open ImageResizer
open System.IO

let sourceFolder = @"C:\FSharp Scripts\blog related"
let origCopyFolder = sourceFolder + "\\orig"
let resizedCopyFolder = sourceFolder + "\\resized"

let images = Directory.GetFiles(sourceFolder, "*.jpeg", SearchOption.AllDirectories)

let resizeImages = 
    // Clear previous copies
    let diOrig = DirectoryInfo(origCopyFolder)
    for file in diOrig.GetFiles() do
        file.Delete()

    let diResized = DirectoryInfo(resizedCopyFolder)
    for file in diResized.GetFiles() do
        file.Delete()

    // Backup and resize jpegs
    for image in images do
        let sourceImg = image.Substring(image.LastIndexOf("\\"))
        
        // Backup untouched
        let origFile = sourceFolder + sourceImg
        let origDest = sourceFolder + "\\orig" + sourceImg
        File.Copy(origFile, origDest)

        // Copy edited
        let resizedDest = sourceFolder + "\\resized" + sourceImg
        let settings = ResizeSettings("width=375&quality=95&autorotate=false&srotate=90")
        ImageBuilder.Current.Build(image, resizedDest, settings)

resizeImages

//View in explorer, right click "Rotate left/right" to quick fix.