# Zero-K-MapEditing
This is a playground to figure out how map-editing works for the game: Zero-K

# Useful links
* Map Converter: https://springrts.com/wiki/MapConvNG
* Map Dev Tutorial: https://springrts.com/wiki/Mapdev:Tutorial_Simple
* Map Creation Wiki: https://zero-k.info/mediawiki/index.php?title=Map_Creation

# First steps
* Create a copy of Map-Blueprint/mapcontainer.sdd
* rename mapcontainer.sdd to the name of your map e.g. MyAwesomeMap.sdd
* Open mapinfo.lua, and update name, shortname, description, author, version

## Heightmap
Details: https://springrts.com/wiki/Mapdev:height
* Calculate the size of your heightmap image
	* e.g. MapSize=12*16
	* Formula: pixels = MapSize * 64 + 1
	* 12 * 64 + 1 = 769x
	* 16 * 64 + 1 = 1025y

* Create an image with the calculated dimensions in your favorite program
	* Image properties: Greyscale, 16bpp
	* Black will be the lowest elevation, white will be the highest elevation. How high these colors will be is defined in mapinfo.lua as minHeight(black) ans maxHeight(white)
* Save image in working directory (TODO) as heightmap.png

## Metalmap
Details: https://springrts.com/wiki/Mapdev:metal
* Same steps as for the heightmap, but use: Formula: pixels = MapSize * 32 instead
* Image properties: RGB, 8bpp
* Color Red indicates how much metal is on a spot. For instance: #010000 is barely any metal, #FF0000 is maximum metal
* The max-amount of metal which an extractor can extract for a bright red color (#FF0000) is defined in mapinfo.lua as maxMetal

## Diffusemap
Details: https://springrts.com/wiki/Mapdev:diffuse
* Same steps as for heightmap, but use Formula: pixels = MapSize * 512 instead
* Image Properties: RGBA, 8bpp
