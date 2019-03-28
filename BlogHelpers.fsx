[<AutoOpen>]
module BlogHelpers

open System
open System.Text.RegularExpressions

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
let removeNonHtmlCharacters (x:string) = x.Replace("(", "-").Replace(")","-").Replace("'","").Replace(":","").Replace(".","").Replace("–","-")
let removeMultipleHyphens (x:string) = Regex.Replace(x, "-+", "-")
let trimHyphen (x:string) = x.Trim('-')
let toLower (x:string) = x.ToLower()

let formatInput (x:string) = replaceSpaceWithHyphen x
                                |> removeNonHtmlCharacters
                                |> removeMultipleHyphens
                                |> trimHyphen
                                |> toLower

// Removes " Show Less" from the About text
let cleanAboutText (x:string) = let pattern = "((?s).*)( Show Less)"
                                let match1 = Regex.Match(x, pattern)
                                if match1.Success then
                                    match1.Groups.[1].Value
                                else
                                    x
