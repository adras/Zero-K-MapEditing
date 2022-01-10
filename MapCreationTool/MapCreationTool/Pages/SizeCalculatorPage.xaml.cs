using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using Forms = System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace MapCreationTool
{
    /// <summary>
    /// Interaction logic for CreateMapPage.xaml
    /// </summary>
    public partial class SizeCalculatorPage : Page
    {
        public SizeCalculatorPage()
        {
            InitializeComponent();
        }

        private void tbMapSizeX_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalculateMapSize();
        }

        private void CalculateMapSize()
        {
            CalculateMapSize();
        }

        private void tbMapSizeY_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
