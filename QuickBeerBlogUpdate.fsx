#load "BlogHelpers.fsx"
#load "BeerData.fsx"
#load "WebScraper.fsx"

open BeerData
open BlogHelpers
open Blog.Webscraper
open System.IO

let formatPostName (date:string) (text:string) = formatInput (date + "-" + text)

let beerList = allPosts 15
                //getBeerList

for beer in beerList do
    let formattedPostName = formatPostName (beer.PostDate.ToString "MM-dd") beer.BeerName
    let fullBeerName = beer.BeerName + " by " + beer.BreweryName
    let outputFileName = @"C:\FSharp Scripts\blog related\" + formattedPostName + ".md"

    let header = "## " + fullBeerName + "\r\n"
    let aboutBeer = "### " + beer.AboutHeader + "\r\n"
    let aboutText = "> " + beer.AboutText + "\r\n"
    let untappdUrl = "[untappd-url]: <" + beer.UntappdLink + ">" + "\n"
    let beerPicUrl = "[beer-pic]: " + beer.BeerImgLink + " " + "\"" + fullBeerName + "\"" + "\r\n"

    let generatePostHeader = "@{\r\n Layout = \"beerpost\";\r\n Title = \"" 
                            + beer.BeerName 
                            + "\";\r\n AddedDate = \"" 
                            + beer.PostDate.ToString("yyyy-MM-ddThh:mm:ss") 
                            + "\";\r\n Tags = \"" 
                            + beer.Tags 
                            + "\";\r\n Description = \"\";\r\n }\r\n \r\n"

    //printf "%s\r\n" outputFileName
    File.WriteAllText(outputFileName, generatePostHeader)
    File.AppendAllText(outputFileName, "\r\n")
    File.AppendAllText(outputFileName, header)
    File.AppendAllText(outputFileName, "\r\n")
    File.AppendAllText(outputFileName, "![beer-pic]" + "\r\n")
    File.AppendAllText(outputFileName, "\r\n")
    File.AppendAllText(outputFileName, aboutBeer)
    File.AppendAllText(outputFileName, "\r\n")
    File.AppendAllText(outputFileName, aboutText)
    File.AppendAllText(outputFileName, "\n")
    File.AppendAllText(outputFileName, "Via [untappd][untappd-url]." + "\r\n")
    File.AppendAllText(outputFileName, "\r\n")
    File.AppendAllText(outputFileName, untappdUrl)
    File.AppendAllText(outputFileName, beerPicUrl)
