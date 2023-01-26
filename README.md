# KT2CG - Kill Team 2 card generator

For rosters and data cards we have [BattleScribe](https://battlescribe.net/) and [Datacard App](https://datacard.app/). For me they do the job very well. 
But, for convenience in game I also want printed out versions of eqipment, ploys, and tac ops, so that I can leave the books on the shelf. 
This is what this project is for.

I'm not trying to show off any mad coding skills, so this is a hit-it-with-a-hammer solution. 

# What can it do?

Well, currently it's work in progress. 

Given that the data is not my IP it will not be included in the project. 
Also it can change on a regular basis, so goal is to make card generation repetable form a stable source with updated documentation.

[Wahapedia](https://wahapedia.ru/) have all the data online, but it seems like there is no data api or json files to snatch. 
The structure of the pages is pretty consistent (2 permutations), so I've decided scrape data from there, and render that. 

Goal one is to render eqipment, ploys and tackops in a simple HTML output formatting it with CSS, that can be printed on A4 paper with a layout there is easy to cut. 
I don't care for duplex, or mega-niceness, since these cards may have to be replaced 4 times a year.
Preferably cards will be american mini size, but they may be annoingly small for some scenarios.

# Status

- Data can be craweld and stored as json. Can crawl any of the factions i found relevant, but it's trivial to add more.
- Data or Json can be passed to a "HTML printer" that will generate output for printing. Layout is currently wip, but should be done soon. 
- I'll make sure cards are printed in different colors so they are easy to identify per faction. For equipment cards, I imagine the following amounts should be printed
  - for "only one" cards, render only one
  - for cards with non-conditional cost, render as many as you can get for max 10EP
  - for cards with variable cost, render 5. 


Stay tuned. 

