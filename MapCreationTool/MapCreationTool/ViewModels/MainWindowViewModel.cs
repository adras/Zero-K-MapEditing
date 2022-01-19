using MapCreationTool.Configuration;
using MapCreationTool.Tabs;
using MapCreationTool.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreationTool.ViewModels
{
    public class MainWindowViewModel : PropertyChangedBase
    {
        private ObservableCollection<TabBase> tabs;
        private TabBase selectedTab;
        private ProjectSettings projectSettings;

        public ObservableCollection<TabBase> Tabs
        {
            get => tabs;
            set
            {
                tabs = value;
                OnPropertyChanged();
            }
        }

        public ProjectSettings ProjectSettings { get => projectSettings; set => projectSettings = value; }
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
