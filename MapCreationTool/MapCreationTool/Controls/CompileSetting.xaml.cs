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
    /// Interaction logic for CompileSetting.xaml
    /// </summary>
    public partial class CompileSetting : UserControl
    {
        public bool SettingIsEnabled
        {
            get { return (bool)GetValue(SettingIsEnabledProperty); }
            set { SetValue(SettingIsEnabledProperty, value); }
        }

        public string SettingName
        {
            get { return (string)GetValue(SettingNameProperty); }
            set { SetValue(SettingNameProperty, value); }
        }

        public string SettingValue
        {
            get { return (string)GetValue(SettingValueProperty); }
            set { SetValue(SettingValueProperty, value); }
        }

        public string SettingDescription
        {
            get { return (string)GetValue(SettingDescriptionProperty); }
            set { SetValue(SettingDescriptionProperty, value); }
        }

        public bool SettingHasFileBrowser
        {
            get { return (bool)GetValue(SettingHasFileBrowserProperty); }
            set { SetValue(SettingHasFileBrowserProperty, value); }
        }


        public static readonly DependencyProperty SettingIsEnabledProperty = DependencyProperty.Register(
            nameof(SettingIsEnabled),
            typeof(bool),
            typeof(CompileSetting),
            new PropertyMetadata(true)
        );

        public static readonly DependencyProperty SettingNameProperty = DependencyProperty.Register(
            nameof(SettingName),
            typeof(string),
            typeof(CompileSetting),
            new PropertyMetadata("")
        );

        public static readonly DependencyProperty SettingValueProperty = DependencyProperty.Register(
            nameof(SettingValue),
            typeof(string),
            typeof(CompileSetting),
            new PropertyMetadata("")
        );

        public static readonly DependencyProperty SettingDescriptionProperty = DependencyProperty.Register(
            nameof(SettingDescription),
            typeof(string),
            typeof(CompileSetting),
            new PropertyMetadata("")
        );

        public static readonly DependencyProperty SettingHasFileBrowserProperty = DependencyProperty.Register(
            nameof(SettingHasFileBrowser),
            typeof(bool),
            typeof(CompileSetting),
            new PropertyMetadata(false)
        );

        public CompileSetting()
        {
            InitializeComponent();
        }

        private void btnBrowseFile_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
