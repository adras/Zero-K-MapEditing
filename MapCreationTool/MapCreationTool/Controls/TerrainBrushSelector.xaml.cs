using MapCreationTool.Terrain;
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
    /// Interaction logic for TerrainBrushSelector.xaml
    /// </summary>
    public partial class TerrainBrushSelector : UserControl
    {

        BrushTypes selectedBrushType;
        public BrushTypes SelectedBrushType { get => selectedBrushType; set => selectedBrushType = value; }

        public TerrainBrushSelector()
        {
            InitializeComponent();
        }

        private void radioBrush_Checked(object sender, RoutedEventArgs e)
        {
            //RadioButton senderRadio = (RadioButton)sender;
            switch (SelectedBrushType)
            {

            }
        }

    }
}
