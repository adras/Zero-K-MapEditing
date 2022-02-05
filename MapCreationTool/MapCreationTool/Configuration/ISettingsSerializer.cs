namespace MapCreationTool.Configuration
{
    interface ISettingsSerializer<T>
    {

        public T DeserializeFromFile(string path);

        public void SerializeToFile(string path, T settings);
    }
}
