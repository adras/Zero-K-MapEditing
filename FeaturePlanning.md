# Main Features
## Map Size Calculator
* Allows for calculation of image sizes for metal, diffuce, height, grass maps

## Create Map
* Allows to create a new map by copying a default template
* Allows to specify a map size
* Automatically creates images for grass, diffuse, height etc with correct image settings
* Creates a project file with default settings

## Open Map
* Opens or creates a project file

## Project File
* Contains map-related settings like compile options for map compiler
* Contains all other map-related settings

# Update/Deploy Map
* Allows to recompile/update the map
* Allows to deploy the compiled map to game directory
* Allows to change setting from project file
* Allow to deploy as directory or as 7z

# Project file
Contains a list of settings like:
* Gamedirectory for deployment
* Names of images files for grass, diffuse, height etc image files

# TODO
* Add image library to allow for creation of images during map creation
* Define Project-Settings file with read/write/edit support
* Implement pymapcomp to allow for map compilation
* Redesign Update/deploy Map dialog to embedd required features
