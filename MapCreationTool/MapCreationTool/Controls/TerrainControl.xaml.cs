using MapCreationTool.Rendering;
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
	/// Interaction logic for TerrainControl.xaml
	/// </summary>
	public partial class TerrainControl : UserControl
	{
		Testing testing;

		public TerrainControl()
		{
			InitializeComponent();

			testing = new Testing(this);
	
		}
	}
}
