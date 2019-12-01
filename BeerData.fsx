module BeerData

#load "BlogHelpers.fsx"
open BlogHelpers
open System

let getBeerList = 
     let beerList =
          [ 

           {   PostDate = new DateTime(2019, 5, 25);
                BeerName = "Sunday IPA";
                BreweryName = "Martin House Brewing Company";
                AboutHeader = "About Sunday IPA";
                AboutText = "Triple IPA w/ Centennial, Cascade, and Hallertau Blanc hops. 4 pack and draft release March 2019.";
                Tags = "IPA, TIPA, Tavour";
                UntappdLink = "https://untappd.com/b/martin-house-brewing-company-sunday-ipa/3121267";
                BeerImgLink = "https://jasonpowley.com/assets/img/2019-05-25-sunday-ipa.jpeg" };

           {   PostDate = new DateTime(2019, 5, 26);
                BeerName = "Kentucky Breakfast Stout (KBS) (2019)";
                BreweryName = "Founders Brewing Co.";
                AboutHeader = "About Kentucky Breakfast Stout (KBS) (2019)";
                AboutText = "What we’ve got here is an imperial stout brewed with a massive amount of coffee and chocolates, then cave-aged in oak bourbon barrels for an entire year to make sure wonderful bourbon undertones come through in the finish. Makes your taste buds squeal with delight.";
                Tags = "Stout, BBA";
                UntappdLink = "https://untappd.com/b/founders-brewing-co-kentucky-breakfast-stout-kbs-2019/3114973";
                BeerImgLink = "https://jasonpowley.com/assets/img/2019-05-26-kentucky-breakfast-stout-kbs-2019.jpeg" };

           ]
     beerList


(* Sample data

{    PostDate = new DateTime(2019, XX, XX);
     BeerName = "Hooray Beer!";
     BreweryName = "";
     AboutHeader = "";
     AboutText = "";
     Tags = "General, Tavour";
     UntappdLink = "https://about.tavour.com/";
     BeerImgLink = "https://jasonpowley.com/assets/img/2019-XX-XX-hooray-beer.jpeg" };

*)