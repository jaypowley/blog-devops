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

let removeParens (x:string) = x.Replace("(", "-").Replace(")","-").Replace("[", "-").Replace("]","-").Replace("{", "-").Replace("}","-")
let replaceWithLatinAlphabetA (x:string) = x.Replace("à", "a").Replace("á", "a").Replace("â", "a").Replace("ã", "a").Replace("ä", "a").Replace("å", "a")
let replaceWithLatinAlphabetE (x:string) = x.Replace("è", "e").Replace("é", "e").Replace("ê", "e").Replace("ë", "e")
let replaceWithLatinAlphabetI (x:string) = x.Replace("ì", "i").Replace("í", "i").Replace("î", "i").Replace("ï", "i")
let replaceWithLatinAlphabetO (x:string) = x.Replace("ð", "o").Replace("ò", "o").Replace("ó", "o").Replace("ô", "o").Replace("õ", "o").Replace("ö", "o").Replace("ø", "o")
let replaceWithLatinAlphabetU (x:string) = x.Replace("ù", "u").Replace("ú", "u").Replace("û", "u").Replace("ü", "u")
let replaceWithLatinAlphabetN (x:string) = x.Replace("ñ", "n")
let replaceWithLatinAlphabet (x:string) = x
                                            |> replaceWithLatinAlphabetA
                                            |> replaceWithLatinAlphabetE
                                            |> replaceWithLatinAlphabetI
                                            |> replaceWithLatinAlphabetO
                                            |> replaceWithLatinAlphabetU
                                            |> replaceWithLatinAlphabetN

let removeOther (x:string) = x.Replace("'","").Replace(":","").Replace(".","").Replace("–","-").Replace("/","").Replace("&","and")
let removeNonHtmlCharacters (x:string) = removeParens x
                                            |> replaceWithLatinAlphabet
                                            |> removeOther

let removeMultipleHyphens (x:string) = Regex.Replace(x, "-+", "-")
let trimHyphen (x:string) = x.Trim('-')
let toLower (x:string) = x.ToLower()

let formatInput (x:string) = toLower x
                                |> replaceSpaceWithHyphen
                                |> removeNonHtmlCharacters
                                |> removeMultipleHyphens
                                |> trimHyphen

let formatPostName (date:string) (text:string) = formatInput (date + "-" + text)

// Removes " Show Less" from the About text
let cleanAboutText (x:string) = let pattern = "((?s).*)( Show Less)"
                                let match1 = Regex.Match(x, pattern)
                                if match1.Success then
                                    match1.Groups.[1].Value
                                else
                                    x

let matchStyles (style:string) (brewery:string) = 
    let paCraftBrewed = match brewery with  
                        | "Pizza Boy Brewing Co." | "Newfangled Brew Works" | "Tröegs Independent Brewing" -> ", PA Craftbrew"
                        | _ -> ""
    
    let tags = match style with
                // | "Altbier" -> ""
                // | "American Wild Ale" -> ""
                | "Barleywine - American" | "Barleywine - English" | "Barleywine - Other" -> "Barleywine"
                | "Belgian Blonde" -> "Belgian Blonde, Belgian, Blonde"
                | "Belgian Dubbel" -> "Belgian Dubbel, Belgian, Dubbel"
                | "Belgian Quadrupel" -> "Belgian Quadrupel, Belgian, Quadrupel"
                | "Belgian Strong Dark Ale" -> "Belgian Strong Dark Ale, Belgian"
                | "Belgian Strong Golden Ale" -> "Belgian Strong Golden Ale, Belgian"
                | "Belgian Tripel" -> "Belgian Tripel, Belgian, Tripel"
                | "Bière de Champagne / Bière Brut" -> ""
                // | "Bière de Garde" -> ""
                // | "Blonde Ale" -> ""
                | "Bock - Doppelbock" | "Bock - Hell / Maibock / Lentebock" -> "Bock"
                | "Brown Ale - American" | "Brown Ale - Belgian" | "Brown Ale - Imperial / Double" -> "Brown Ale"
                // | "Burton Ale" -> ""
                // | "Dark Ale" -> ""
                // | "Dunkelweizen" -> ""
                | "Farmhouse Ale - Other" -> "Farmhouse Ale"
                | "Farmhouse Ale - Saison" -> "Farmhouse Ale, Saison"
                | "Gruit / Ancient Herbed Ale" -> "Herbed Ale"
                // | "Hefeweizen" -> ""
                | "IPA - American" | "IPA - Black / Cascadian Dark Ale" | "IPA - English" | "IPA - Imperial / Double Black" | "IPA - Imperial / Double New England" | "IPA - New England" | "IPA - Session / India Session Ale" -> "IPA"    
                | "IPA - Imperial / Double" -> "IPA, DIPA"
                | "IPA - Sour" -> "IPA, Sour"
                | "IPA - Triple" -> "IPA, TIPA"
                | "IPA - Belgian" -> "IPA, Belgian Style"
                | "Kellerbier / Zwickelbier" -> "Kellerbier"
                // | "Kölsch" -> ""
                | "Lager - Amber" | "Lager - American" | "Lager - American Light" | "Lager - Euro Pale" | "Lager - IPL (India Pale Lager)" | "Lager - Munich Dunkel" | "Lager - Pale" | "Lager - Vienna" -> "Lager"
                | "Lambic - Faro" | "Lambic - Kriek" -> "Lambic"
                | "Mead - Other" -> "Mead"
                // | "Märzen" -> ""
                // | "Old Ale" -> ""
                | "Pale Ale - American" | "Pale Ale - Belgian" | "Pale Ale - English" -> "Pale Ale"
                | "Pilsner - German" | "Pilsner - Other" -> "Pilsner"
                | "Porter - American" | "Porter - Baltic" | "Porter - Coffee" | "Porter - English" | "Porter - Imperial / Double" | "Porter - Other" -> "Porter"
                | "Pumpkin / Yam Beer" -> "Pumpkin"
                | "Red Ale - American Amber / Red" | "Red Ale - Imperial / Double" -> "Red Ale"
                // | "Rye Beer" -> ""
                // | "Schwarzbier" -> ""
                | "Scotch Ale / Wee Heavy" -> "Scotch Ale, Wee Heavy"
                // | "Scottish Ale" -> ""
                | "Shandy / Radler" -> "Radler"
                // | "Smoked Beer" -> ""
                | "Sour - Flanders Red Ale" | "Sour - Fruited" | "Sour - Gose - Fruited" | "Sour - Other" | "Stout - White" -> "Sour"
                | "Spiced / Herbed Beer" -> "Herbed Beer"
                | "Stout - American" | "Stout - Foreign / Export" | "Stout - Other" | "Stout - Oyster" -> "Stout"
                | "Stout - American Imperial / Double" -> "Stout, American Imperial Stout"
                | "Stout - Coffee" -> "Stout, Coffee Stout"    
                | "Stout - Imperial / Double" | "Stout - Imperial / Double Milk" | "Stout - Imperial / Double Oatmeal" | "Stout - Imperial / Double White" -> "Stout, Imperial Stout"
                | "Stout - Irish Dry" -> "Stout, Irish Dry Stout"
                | "Stout - Milk / Sweet" -> "Stout, Milk Stout"
                | "Stout - Oatmeal" -> "Stout, Oatmeal Stout"    
                | "Stout - Pastry" -> "Stout, Pastry Stout"
                | "Stout - Russian Imperial" -> "Stout, Russian Imperial Stout"    
                | "Strong Ale - American" -> "American Strong Ale"
                | "Strong Ale - Other" -> "Strong Ale"
                | "Wheat Beer - American Pale Wheat" | "Wheat Beer - Other" -> "Wheat Beer"
                // | "Wheat Wine" -> ""
                // | "Winter Ale" -> ""
                // | "Winter Warmer" -> ""
                // | "Witbier" -> ""
                | _ -> style
    tags + paCraftBrewed