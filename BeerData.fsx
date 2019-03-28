module BeerData

#load "BlogHelpers.fsx"
open BlogHelpers
open System

let getBeerList = 
     let beerList =
          [ 

           {   PostDate = new DateTime(2019, 3, 7);
                BeerName = "Oil Man Bourbon Barrel Imperial Stout";
                BreweryName = "Elevation Beer Company";
                AboutHeader = "About Elevation Beer Company";
                AboutText = "Elevation Beer Company is a Colorado microbrewery specializing in seasonal speciality and barrel aged beers for the craft beer fanatic.";
                Tags = "Stout, BBA, Tavour";
                UntappdLink = "https://untappd.com/elevationbeerco";
                BeerImgLink = "https://jasonpowley.com/assets/img/2019-03-07-oil-man-bourbon-barrel-imperial-stout.jpeg" };

           {   PostDate = new DateTime(2019, 3, 8);
                BeerName = "Hooray Beer!";
                BreweryName = "";
                AboutHeader = "";
                AboutText = "";
                Tags = "";
                UntappdLink = "";
                BeerImgLink = "https://jasonpowley.com/assets/img/2019-03-08-hooray-beer.jpeg" };

           {   PostDate = new DateTime(2019, 3, 8);
                BeerName = "Bourbon Barrel Aged Dark Star (2017)";
                BreweryName = "Fremont Brewing";
                AboutHeader = "About Bourbon Barrel Aged Dark Star (2017)";
                AboutText = "This year’s release is a blend of 24, 18, 12, and 8-month Bourbon Barrel-Aged Dark Star in 7-12-year old Kentucky bourbon barrels. The roasted and chocolate malts complement the smooth oats to bring you a stout delight wrapped in the gentle embrace of bourbon barrel-aged warmth. A touch of sweetness dances in balance with the hops to finish with a wave, and then she’s gone.";
                Tags = "Stout, Oatmeal Stout, BBA, Tavour";
                UntappdLink = "https://www.fremontbrewing.com/bourbon-dark-star";
                BeerImgLink = "https://jasonpowley.com/assets/img/2019-03-08-bourbon-barrel-aged-dark-star-2017.jpeg" };

           ]
     beerList


(* Sample data

{    PostDate = new DateTime(2019, 3, 7);
     BeerName = "";
     BreweryName = "";
     AboutHeader = "About ";
     AboutText = "";
     Tags = "";
     UntappdLink = "";
     BeerImgLink = "https://jasonpowley.com/assets/img/" };

*)