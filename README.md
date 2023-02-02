# Zero-K Map Tool

## Description
Goal of this tool is to help users with map-creation in the game Zero-K.

Right now there's no official release yet, since this is in a very early prototype stage. 

## Download
A very early prototype can be found in [Releases](https://github.com/adras/Zero-K-MapEditing/releases)

Be aware though, that it might crash any time.

## Build instructions
### Get sourcecode

Since this project includes other git repositories as submodules it's not enough to simply download the code as .zip from github.

The code needs to be pulled via git. For instance, create a new empty directory and run:
```git clone --recurse-submodules -j8 https://github.com/adras/Zero-K-MapEditing.git```

See [this stackoverflow article](https://stackoverflow.com/questions/3796927/how-do-i-git-clone-a-repo-including-its-submodules) for additional information

### Build solution
* Get Visual Studio e.g. Visual Studio Community Edition
* Open Project/Solution -> MapCreationTool\MapCreationTool.sln 
* Build/Rebuild Solution
* Excecutable can be found in: src\MapCreationTool\MapCreationTool\bin\x64\Debug\net5.0-windows\

Feel free to report issues here on github.

# Current state
For the current state of development see [FeaturePlanning](https://github.com/adras/Zero-K-MapEditing/blob/main/FeaturePlanning.md)

# Contribute
If you like to contribute contact me here on github
