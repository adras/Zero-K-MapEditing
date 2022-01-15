using MapCreationTool.Models;

namespace MapCreationTool
{
    internal class MapCreationResult
    {
        public string type;
        public bool success;
        public string error;
        public MapPathInformation mapPathInfo;

        public MapCreationResult(string type, MapPathInformation mapPathInfo = null, string error = null)
        {
            if (error == null)
            {
                success = true;
            }
            else
            {
                success = false;
                this.error = error;
            }
            this.mapPathInfo = mapPathInfo;
            this.type = type;
        }
    }
}
