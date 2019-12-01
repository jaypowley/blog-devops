
#r "C:/Shared Resources/FSharp.Data/FSharp.Data.3.0.0/lib/net45/FSharp.Data.dll"
 
open FSharp.Data
open System
open System.IO
open System.Text.RegularExpressions
 
type SourceData = JsonProvider<"C:/Users/Jason/Desktop/beer_data_07292019.json">
 
let beers = SourceData.GetSamples()
 
type PostObject = {   
        PostDate : DateTime
        BeerName : String
        BreweryName : String
        AboutHeader : String
        AboutText : String
        Tags : String
        UntappdLink : String
        BeerImgLink : String
    }
 
let replaceSpaceWithHyphen (x:string) = x.Replace(" ", "-")
let removeNonHtmlCharacters (x:string) = x.Replace("(", "-").Replace(")","-").Replace("'","").Replace(":","").Replace(".","").Replace("–","-").Replace("/","")
let removeMultipleHyphens (x:string) = Regex.Replace(x, "-+", "-")
let trimHyphen (x:string) = x.Trim('-')
let toLower (x:string) = x.ToLower()
 
let formatInput (x:string) = replaceSpaceWithHyphen x
                                |> removeNonHtmlCharacters
                                |> removeMultipleHyphens
                                |> trimHyphen
                                |> toLower
 
let formatPostName (date:string) (text:string) = formatInput (date + "-" + text)
 
let query1 =
    query {
        for beer in beers do
        sortByDescending beer.CreatedAt
        select {
                PostDate = beer.CreatedAt;
                BeerName = beer.BeerName;
                BreweryName = beer.BreweryName;
                AboutHeader = String.Empty;
                AboutText = String.Empty;
                Tags = beer.BeerType;
                UntappdLink = beer.BeerUrl;
                BeerImgLink =  match beer.PhotoUrl with
                                | Some x -> x
                                | None -> String.Empty;
        };
        take 10
    }
 
//type Pair = {Number : int; Product : int;}
//let productPairs = list |> List.map(fun e -> {Number = e; Product = e*2});;

let mapToType (beerName:string) (breweryName:string) = beerName + breweryName
 
let beerList =  query1 |> Seq.toList
 
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

 

