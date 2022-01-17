using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreationTool.Models
{
    public class WidthHeight
    {
        private int width;
        private int height;

        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }

        public WidthHeight()
        {
        }

        public WidthHeight(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        public static WidthHeight CreateByFactor(WidthHeight widthHeight, int factor)
        {
            WidthHeight result = new WidthHeight(widthHeight.Width * factor, widthHeight.Height * factor);
            return result;
        }

        public static WidthHeight CreateByFactorAdd(WidthHeight widthHeight, int factor, int add)
        {
            WidthHeight result = new WidthHeight(widthHeight.Width * factor + add, widthHeight.Height * factor + add);
            return result;
        }

        public override string ToString()
        {
            string result = $"Width: {Width} Height: {Height}";
            return result;
        }
    }
}
