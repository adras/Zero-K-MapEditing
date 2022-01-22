using MapCreationTool.MapSizes;

namespace MapCreationTool.Models
{
    // Warning CS8618  Non-nullable field 'xxx' must contain a non-null value when exiting constructor.
    #pragma warning disable CS8618
    public class MapSizeDefinition
    {
        private WidthHeight gameMapSize;
        private WidthHeight grassMapSize;
        private WidthHeight metalMapSize;
        private WidthHeight heightMapSize;
        private WidthHeight diffuseMapSize;

        public WidthHeight GameMapSize { get => gameMapSize; set => gameMapSize = value; }
        public WidthHeight GrassMapSize { get => grassMapSize; set => grassMapSize = value; }
        public WidthHeight MetalMapSize { get => metalMapSize; set => metalMapSize = value; }
        public WidthHeight HeightMapSize { get => heightMapSize; set => heightMapSize = value; }
        public WidthHeight DiffuseMapSize { get => diffuseMapSize; set => diffuseMapSize = value; }

        public MapSizeDefinition(WidthHeight gameMapSize)
        {
            this.GameMapSize = gameMapSize;
            this.GrassMapSize = MapSizeCalculator.CalculateGrassMap(gameMapSize);
            this.HeightMapSize = MapSizeCalculator.CalculateHeightMap(gameMapSize);
            this.MetalMapSize = MapSizeCalculator.CalculateMetalMap(gameMapSize);
            this.DiffuseMapSize = MapSizeCalculator.CalculateDiffuseMap(gameMapSize);
        }

        public MapSizeDefinition()
        {
        }
    }
}
