using MapCreationTool.MapSizes;

namespace MapCreationTool.Models
{
    public class MapSizeDefinition
    {
        public WidthHeight gameMapSize;
        public WidthHeight grassMapSize;
        public WidthHeight metalMapSize;
        public WidthHeight heightMapSize;
        public WidthHeight diffuseMapSize;

        public MapSizeDefinition(WidthHeight gameMapSize)
        {
            this.gameMapSize = gameMapSize;
            this.grassMapSize = MapSizeCalculator.CalculateGrassMap(gameMapSize);
            this.heightMapSize = MapSizeCalculator.CalculateHeightMap(gameMapSize);
            this.metalMapSize = MapSizeCalculator.CalculateMetalMap(gameMapSize);
            this.diffuseMapSize = MapSizeCalculator.CalculateDiffuseMap(gameMapSize);
        }

        public MapSizeDefinition()
        {
        }
    }
}
