module Blog.Webscraper

#r @"lib\FSharp.Data\FSharp.Data.3.0.0\lib\net45\FSharp.Data.dll"
#load "BlogHelpers.fsx"
open BlogHelpers
open FSharp.Data
open System

let urlSource = "https://untappd.com/"
let userUrl = "user/jpowley"
let blogSource = "https://jasonpowley.com/"
let assetDir = "assets/img/"

let userPage = HtmlDocument.Load(urlSource + userUrl)

// all posts are under item CSS class
let items = userPage.Descendants()
            |> Seq.filter (fun n -> n.HasClass("item"))

// a single check in
let getCheckin (item:HtmlNode) = 
    item.Descendants()
    |> Seq.filter (fun n -> n.HasClass("checkin"))
    |> Seq.head

// top section of check in. contains beer and brewery names and links
let checkinTop (checkIn:HtmlNode) = 
    checkIn.CssSelect("div.top a")
    |> List.map(fun a -> a.InnerText().Trim(), a.AttributeValue("href"))
    |> List.filter(fun (title, href) -> title <> String.Empty && title <> "Jason P.")

// bottom section of check in. contains check in date
let checkinBottom (checkIn:HtmlNode) = 
    checkIn.CssSelect("div.feedback div.bottom a")
    |> List.map(fun a -> a.InnerText().Trim())
    |> List.filter(fun (title) -> title <> "View Detailed Check-in" && title <> "Delete Check-In")

let getBeerPage (url:string) = HtmlDocument.Load(url)
let getBeerAboutText (beerPage:HtmlDocument) = beerPage.CssSelect("div.beer-descrption-read-less").Head.InnerText()
let getBreweryPage (url:string) = HtmlDocument.Load(url)
let getBreweryAboutText (breweryPage:HtmlDocument) = breweryPage.CssSelect("div.beer-descrption-read-less").Head.InnerText()
let getBeerStyles (beerPage:HtmlDocument) (breweryName:string) = 
    let beerStyle = beerPage.CssSelect("div.name p.style").Head.InnerText()
    matchStyles beerStyle breweryName

let getAboutTextAndSource (data:(string * string) list) =
    let mutable aboutText = String.Empty
    let mutable aboutHeading = String.Empty
    let mutable untappdLink = String.Empty
    let beerLink = snd data.Head 
    let beerName = fst data.Head 
    let breweryLink = snd (List.item 1 data)
    let breweryName = fst (List.item 1 data)
    let beerPage = getBeerPage (urlSource + beerLink)
    let breweryPage = getBreweryPage (urlSource + breweryLink)
    let aboutBeer = getBeerAboutText beerPage
    let aboutBrewery = getBreweryAboutText breweryPage
    let beerStyles = getBeerStyles beerPage breweryName

    if aboutBeer <> "Show Less" then
        aboutText <- cleanAboutText aboutBeer
        aboutHeading <- "About " + beerName
        untappdLink <- urlSource + beerLink
    elif aboutBrewery <> "Show Less" then
        aboutText <- cleanAboutText aboutBrewery
        aboutHeading <- "About " + breweryName
        untappdLink <- urlSource + breweryLink
    else 
        aboutText <- String.Empty
        aboutHeading <- String.Empty
        untappdLink <- String.Empty

    (aboutHeading.Trim(), aboutText.Trim(), beerStyles, untappdLink)

let mapToType beerName breweryName consumedDate aboutHeading aboutText beerStyle untappdLink =  
    let couldParse, parsedDate = DateTime.TryParse(consumedDate)
    let postDate = match couldParse with
                    | true -> parsedDate
                    | false -> DateTime.Today 
   
    let formattedDate = postDate.ToString "yyyy-MM-dd"    
    let formatImageUrl (date:string) (text:string) = formatInput (date + "-" + text) + ".jpeg"
    let formattedImgUrl = blogSource + assetDir + formatImageUrl formattedDate beerName

    let post = {   
        PostDate = postDate;
        BeerName = beerName;
        BreweryName = breweryName;
        AboutHeader = aboutHeading;
        AboutText = aboutText;
        Tags = beerStyle;
        UntappdLink = untappdLink;
        BeerImgLink = formattedImgUrl };
    post

let getPostDataList node =
    let checkin = getCheckin node
    let checkinHeader = checkin |> checkinTop
    let checkinFooter = checkin |> checkinBottom
    let aboutHeading, aboutText, beerStyle, untappdLink =
        checkin
        |> checkinTop
        |> getAboutTextAndSource

    let beerName = fst (List.item 0 checkinHeader)
    let breweryName = fst (List.item 1 checkinHeader) 
    let consumedDate = checkinFooter.Head
    let result = mapToType beerName breweryName consumedDate aboutHeading aboutText beerStyle untappdLink
    result

// Not all, but latest *postcount* (max of 15) posts
let allPosts postCount = 
    [ 
        for item in Seq.take postCount items do
            yield getPostDataList item 
    ]