namespace MapCreationTool.Models
{
    public class CompilationSettings
    {
        private string outSmfFilePath;
        private string heightMapName;
        private string diffuseMapName;
        private string grassMapName;
        private string metalMapName;
        private int minHeight;
        private int maxHeight;
        private string geoventDecalPath;
        private string featurePlacementFilePath;

        private string featureListFilePath;
        private string featureMapFilePath;
        private string typeMapFilePath;
        private string minimapFilePath;
        private string nvdxtOptions;
        private string highResMapFilter;
        private string dirty;

        private bool useMetalMap;
        private bool useGeoventDecal;
        private bool useFeaturePlacement;

        private bool useFeatureList;
        private bool useFeatureMap;
        private bool useTypeMap;
        private bool useMinimap;
        private bool useNvdxt;
        private bool useHighResMapFilter;
        private bool useDirty;

        public string OutSmfFilePath { get => outSmfFilePath; set => outSmfFilePath = value; }
        public string HeightMapName { get => heightMapName; set => heightMapName = value; }
        public string DiffuseMapName { get => diffuseMapName; set => diffuseMapName = value; }
        public string GrassMapName { get => grassMapName; set => grassMapName = value; }
        public string MetalMapName { get => metalMapName; set => metalMapName = value; }

        public int MinHeight { get => minHeight; set => minHeight = value; }
        public int MaxHeight { get => maxHeight; set => maxHeight = value; }
        public bool UseMetalMap { get => useMetalMap; set => useMetalMap = value; }
        public string GeoventDecalPath { get => geoventDecalPath; set => geoventDecalPath = value; }
        public bool UseGeoventDecal { get => useGeoventDecal; set => useGeoventDecal = value; }
        public string FeaturePlacementFilePath { get => featurePlacementFilePath; set => featurePlacementFilePath = value; }
        public bool UseFeaturePlacement { get => useFeaturePlacement; set => useFeaturePlacement = value; }

        public string FeatureListFilePath { get => featureListFilePath; set => featureListFilePath = value; }
        public string FeatureMapFilePath { get => featureMapFilePath; set => featureMapFilePath = value; }
        public string TypeMapFilePath { get => typeMapFilePath; set => typeMapFilePath = value; }
        public string MinimapFilePath { get => minimapFilePath; set => minimapFilePath = value; }
        public string NvdxtOptions { get => nvdxtOptions; set => nvdxtOptions = value; }
        public string HighResMapFilter { get => highResMapFilter; set => highResMapFilter = value; }
        public string Dirty { get => dirty; set => dirty = value; }
        public bool UseFeatureList { get => useFeatureList; set => useFeatureList = value; }
        public bool UseFeatureMap { get => useFeatureMap; set => useFeatureMap = value; }
        public bool UseTypeMap { get => useTypeMap; set => useTypeMap = value; }
        public bool UseMinimap { get => useMinimap; set => useMinimap = value; }
        public bool UseNvdxt { get => useNvdxt; set => useNvdxt = value; }
        public bool UseHighResMapFilter { get => useHighResMapFilter; set => useHighResMapFilter = value; }
        public bool UseDirty { get => useDirty; set => useDirty = value; }

    }
}
