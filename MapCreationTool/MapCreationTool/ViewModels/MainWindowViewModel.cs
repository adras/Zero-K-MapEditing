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
        public ObservableCollection<TabBase> tabs;

        public ObservableCollection<TabBase> Tabs
        {
            get => tabs;
            set
            {
                tabs = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            tabs = new ObservableCollection<TabBase>();
            tabs.Add(new StartTab());
        }
    }
}
