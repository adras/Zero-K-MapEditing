# Current/Planned features

## Create map
DONE Creates a map based on a map-template in a directory defined by the user.

## Calculate Texture-sizes
DONE A calculator allows to calculate the sizes of all textures like diffuse, heightmap, metalmap at once, simply by entering the map-size

## Open map
DONE Existing maps can be opened, if a directory exists. 7z-map files are not supported yet
* Support 7z files

## View heightmap
The heightmap of a map can be viewed, if the correct path is given
* Add file browser to select heightmap file

## Map Size Calculator
* DONE Allows for calculation of image sizes for metal, diffuce, height, grass maps

## Create Map
* DONE Allows to create a new map by copying a default template
* DONE Allows to specify a map size
* DONE Automatically creates images for grass, diffuse, height etc with correct image settings
* DONE Creates a project file with default settings

## Create/Edit start-zones
* DONE Allow creation of teams
* DONE Create/delete a startzone
* Load map_startboxes.lua
* Save map_startboxes.lua

## Open Map
* DONE Opens or creates a project file

## Project File
* Contains map-related settings like compile options for map compiler
* Contains all other map-related settings

## Update/Deploy Map
* Allows to recompile/update the map
* Allows to deploy the compiled map to game directory
* Allows to change setting from project file
* Allow to deploy as directory or as 7z

## Project file / Application settings
### Application settings
* Contains a list of setting for the application e.g. Gamedirectory

### Project file
* Names of images files for grass, diffuse, height etc image files

## TODO
* DONE Add image library to allow for creation of images during map creation

* Define Project-Settings file with read/write/edit support
* Implement pymapcomp to allow for map compilation
* Redesign Update/deploy Map dialog to embedd required features
* Implement support for .7z creation

