﻿using MapCreationTool.MapSizes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreationTool.Models
{
    public class WidthHeight
    {
        public int width;
        public int height;

        public WidthHeight(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public static WidthHeight CreateByFactor(WidthHeight widthHeight, int factor)
        {
            WidthHeight result = new WidthHeight(widthHeight.width * factor, widthHeight.height * factor);
            return result;
        }

        public static WidthHeight CreateByFactorAdd(WidthHeight widthHeight, int factor, int add)
        {
            WidthHeight result = new WidthHeight(widthHeight.width * factor + add, widthHeight.height * factor + add);
            return result;
        }

        public override string ToString()
        {
            string result = $"Width: {width} Height: {height}";
            return result;
        }
    }

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

    }
}
