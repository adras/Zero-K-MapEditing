# Heightmap editor planning

# What already exists
## Heightmap image
The heightmap file can be considered as a grid with a width and height of the image. Since the file is 16 bit grayscale it means that every pixel in the heightmap/grid is a single
value between 0-65535.

## Mesh generation
The mesh consists of 3D points. The X/Y coordinates are the coordinates of the pixel in the heightmap. The Z value is the color value from the pixel scaled by some value.

The triangles of the mesh are defined in a second step after creating the points of the mesh. Two triangles are created per step. First triangle is one point from row 1
and two points for row 2. Second triangle is two points from row 1 and one point from row 2, where the column is offset by one.

Triangles are made up by an additional list, which consists of indices in the point collection. E.g. The indices 4,5,6 define a triangle of the points in the pointlist at index 4,5,6.

* Points could be reused to improve performance, but it probably makes editing the heightmap more difficult since new points need to be created which also requires a complex update of
the triangle indices

## Editing
It's possible to update the positions of each point in the mesh at any time. Only the z coordinate will require an update, since x/y coordinates don't change (they are just the x/y position in the image)

To allow for editing the following steps need to be implemented:
1. Get the position of the mouse cursor in heightmap coordinates, x/y
2. Calculate the index of the Point of the mesh by: int idx = x + y * image.Width;
3. Change Z coordinate of given point


Since modifying a single point doesn't make much sense a brush needs to be defined, which could be a rectangle or circle.
For a simple rectangle the following should work, where mousePos is the position relative to the mesh:


    for (int x = mousePos.x-20; x < mousePos.x+20)
    { 
      for (int y = mousePos.y-20; y < mousePos.y+20)  
      {
         int pointIndex = x + y * image.width;
         Point3D meshPoint = mesh.Points[pointIndex];
         meshPoint.Z += 20;
         mesh.Points[pointIndex] = meshPoint;
      }
	}
  
To smooth that a smoothingFactor needs to be calculated, which can simply be defined by the distance from the current point to the mouse-position. 

The amount of change is then multiplied by this factor

# Performance
It seems like WPF rendering performance is not sufficient for bigger heightmaps. 4x4 maps are fine, but 12x16 is also problematic.
Check out other rendering engines see: https://www.reddit.com/r/csharp/comments/k4tqjy/3d_rendering_engineframework_for_games/

## Tech-Investigation results
### Silk.NET
25.01.2021 Doesnt't support a WPF control which is a requirement to embed it into the existing application. Would require a
complete rebuilt of the UI, facing possible issues with not existing controls, e.g. configurable/bindable listviews etc

TODO:
* Investigate if it's possible to create a WPF control for silk.net. OpenTK.WPFControl might be a canditate to figure out how this could work.
