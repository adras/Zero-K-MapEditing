using MapCreationTool.Configuration;
using MapCreationTool.ViewModels;

namespace MapCreationTool.ModelConverters
{
    // Name is an ode to my favorite programmer, all hail to Sven!
    internal class ProgramSettingsToAndFromSettingsWindowConverter
    {
        public static void ApplySettingsFromViewModel(ProgramSettings current, SettingsWindowViewModel viewModel)
        {
            current.GameDirectory = viewModel.GameDirectory;
        }

        public static void ApplySettingsToViewModel(ProgramSettings current, SettingsWindowViewModel viewModel)
        {
            viewModel.GameDirectory = current.GameDirectory;
        }
    }
}
