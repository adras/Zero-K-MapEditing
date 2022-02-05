using MapCreationTool.Tabs;
using MapCreationTool.WPF;
using System.Collections.ObjectModel;

namespace MapCreationTool.ViewModels
{
    public class MainWindowViewModel : PropertyChangedBase
    {
        private ObservableCollection<TabBase> tabs;
        private TabBase selectedTab;

        public ObservableCollection<TabBase> Tabs
        {
            get => tabs;
            set
            {
                tabs = value;
                OnPropertyChanged();
            }
        }

        public TabBase SelectedTab
        {
            get => selectedTab;
            set
            {
                selectedTab = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            tabs = new ObservableCollection<TabBase>();
            tabs.Add(new StartTab());
            tabs.Add(new MapSizeCalculatorTab());
        }
    }
}
