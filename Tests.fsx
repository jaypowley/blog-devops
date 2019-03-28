#load "BlogHelpers.fsx"
#load "BeerData.fsx"
#load "WebScraper.fsx"

// open BeerData
// open BlogHelpers
open Blog.Webscraper
//open System.Text.RegularExpressions

let beerList = items

let testCheckIn = Seq.item 2 beerList

let testCheckInTop = checkinTop testCheckIn
for item in testCheckInTop do
    printf "Name: %s\r\nLink: %s\r\n\r\n" (fst item) (snd item)

let testCheckInBottom = checkinBottom testCheckIn
for item in testCheckInBottom do
    printf "Post Date: %s\r\n" item

let (aboutHeading, aboutText, _, _) = getAboutTextAndSource testCheckInTop
printf "aboutHeading: %s\r\naboutText: %s\r\n" aboutHeading aboutText