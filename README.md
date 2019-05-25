# blog-devops
A couple of scripts to gather or format blog data.
## BeerData.fsx
A script file that you can fill in for use as a datasource for "QuickBeerBlogUpdate.fsx".
## BlogHelpers.fsx
A script file with helper functions for formatting text.
## ImageResizer.fsx
A script file that resizes images. Update source folder location and resize settings to suit your needs and run in FSI.
## QuickBeerBlogUpdate.fsx
Basically the only file that needs to be run in FSI. It uses either "WebScraper.fsx" or "BeerData.fsx" as a datasource and then formats the output into markup pages and writes them to disk.
## Tests.fsx
A script file with a couple of function tests.
## WebScraper.fsx
A script file that utilizes FSharp.Data HTML Type provider to scrape data off [Untappd](https://Untappd.com) for the supplied user page.