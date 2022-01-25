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
* DONE Add file browser to select heightmap file

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
* ONGOING Contains map-related settings like compile options for map compiler
* ONGOING Contains all other map-related settings

## Update/Deploy Map
* DONE Allows to recompile/update the map
* Allows to deploy the compiled map to game directory
* DONE Allows to change setting from project file
* DONE Allow setting of compile parameters
* Allow to deploy as directory or as 7z

## Project file / Application settings
### Application settings
* Contains a list of setting for the application e.g. Gamedirectory

### Project file
* Names of images files for grass, diffuse, height etc image files

## TODO
* DONE Add image library to allow for creation of images during map creation
* DONE Implement pymapcomp to allow for map compilation
* DONE Redesign Update/deploy Map dialog to embedd required features
* DONE Complete compilation settings
* DONE Add support to load mapinfo.lua
* DONE Add support to edit some values of mapinfo.lua
* DONE Add support to save mapinfo.lua

* Define Project-Settings file with read/write/edit support
* Implement support for .7z creation
* Add support to read and write start-zones
* Add toggle button to show texture on heightmap
* Add support to save ProjectSettings
* Add deploy support
* Create github release pipeline
* TENTATIVE Add support to edit heightmap

