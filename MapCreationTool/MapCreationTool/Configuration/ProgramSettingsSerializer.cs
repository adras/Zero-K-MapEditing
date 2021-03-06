namespace MapCreationTool.Configuration
{
    class ProgramSettingsSerializer : ISettingsSerializer<ProgramSettings>
    {
        GenericSerializer<ProgramSettings> serializer;

        public ProgramSettingsSerializer()
        {
            serializer = new GenericSerializer<ProgramSettings>();
        }

        public ProgramSettings DeserializeFromFile(string path)
        {
            ProgramSettings result = serializer.DeserializeFromFile(path);
            return result;
        }

        public void SerializeToFile(string path, ProgramSettings settings)
        {
            serializer.SerializeToFile(path, settings);
        }
    }
}
