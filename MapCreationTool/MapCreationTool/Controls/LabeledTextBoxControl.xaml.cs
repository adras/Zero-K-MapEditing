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
    /// Interaction logic for LabeledTextBoxControl.xaml
    /// </summary>
    public partial class LabeledTextBoxControl : UserControl
    {
		public string LabelText
		{
			get { return (string)GetValue(LabelTextProperty); }
			set { SetValue(LabelTextProperty, value); }
		}

		public static readonly DependencyProperty LabelTextProperty = DependencyProperty.Register(
			nameof(LabelText),
			typeof(string),
			typeof(LabeledTextBoxControl),
			new PropertyMetadata("")
		);

		public string TextBoxValue
		{
			get { return (string)GetValue(TextBoxValueProperty); }
			set { SetValue(TextBoxValueProperty, value); }
		}

		public static readonly DependencyProperty TextBoxValueProperty = DependencyProperty.Register(
			nameof(TextBoxValue),
			typeof(string),
			typeof(LabeledTextBoxControl),
			new PropertyMetadata("")
		);

		public LabeledTextBoxControl()
        {
            InitializeComponent();
        }
    }
}
