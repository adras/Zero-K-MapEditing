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


namespace MapCreationTool
{
    /// <summary>
    /// Interaction logic for CreateMapPage.xaml
    /// </summary>
    public partial class CreateMapPage : Page
    {
        public CreateMapPage()
        {
            InitializeComponent();
        }

        private void btnSelectWorkingDir_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog;
        }

        private void btnCreateMap_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbMapWorkingDir.Text))
            {
                MessageBox.Show("Please select or enter a path to a working directory for the new map");
                return;
            }
        }
    }
}
