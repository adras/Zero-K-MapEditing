using MapCreationTool.Models;

namespace MapCreationTool.MapSizes
{
    public class MapSizeCalculator
    {
        public static WidthHeight CalculateDiffuseMap(WidthHeight mapSize)
        {
            WidthHeight result = WidthHeight.CreateByFactor(mapSize, 512);

            return result;
        }

        public static WidthHeight CalculateHeightMap(WidthHeight mapSize)
        {
            WidthHeight result = WidthHeight.CreateByFactorAdd(mapSize, 64, 1);

            return result;
        }


        public static WidthHeight CalculateMetalMap(WidthHeight mapSize)
        {
            WidthHeight result = WidthHeight.CreateByFactor(mapSize, 32);

            return result;

        }
        public static WidthHeight CalculateGrassMap(WidthHeight mapSize)
        {
            WidthHeight result = WidthHeight.CreateByFactor(mapSize, 16);

            return result;
        }

    }
}
