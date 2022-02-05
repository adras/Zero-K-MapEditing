using MapCreationTool.WPF;

namespace MapCreationTool.ViewModels
{
    internal class SettingsWindowViewModel : PropertyChangedBase
    {
        private string gameDirectory;

        public string GameDirectory
        {
            get
            {
                return gameDirectory;
            }

            set
            {
                gameDirectory = value;
                OnPropertyChanged();
            }
        }
    }
}
