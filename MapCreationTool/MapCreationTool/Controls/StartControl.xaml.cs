using MapCreationTool.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MapCreationTool.Controls
{
    /// <summary>
    /// Interaction logic for StartControl.xaml
    /// </summary>
    public partial class StartControl : UserControl
    {

        public StartControl()
        {
            InitializeComponent();
        }

        private void btnCreateMap_Click(object sender, RoutedEventArgs e)
        {
            CreateMap createMapWindow = new CreateMap();
            bool? result = createMapWindow.ShowDialog();
            //NavigationService.Navigate(new Uri("Pages/CreateMapPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnUpdateMap_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new Uri("Pages/UpdateMapPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnSizeCalculator_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new Uri("Pages/SizeCalculatorPage.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
