using System.Windows;
using System.Windows.Controls;

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

        public int LabelWidth
        {
            get { return (int)GetValue(LabelWidthProperty); }
            set { SetValue(LabelWidthProperty, value); }
        }

        public static readonly DependencyProperty LabelWidthProperty = DependencyProperty.Register(
            nameof(LabelWidth),
            typeof(int),
            typeof(LabeledTextBoxControl),
            new PropertyMetadata(100)
        );

        public LabeledTextBoxControl()
        {
            InitializeComponent();
        }
    }
}
