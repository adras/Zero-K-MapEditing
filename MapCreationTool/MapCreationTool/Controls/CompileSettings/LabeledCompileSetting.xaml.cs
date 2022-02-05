using System.Windows;
using System.Windows.Controls;

namespace MapCreationTool.Controls.CompileSettings
{
    /// <summary>
    /// Interaction logic for LabeledCompileSetting.xaml
    /// </summary>
    public partial class LabeledCompileSetting : UserControl
    {
        public bool SettingIsEnabled
        {
            get { return (bool)GetValue(SettingIsEnabledProperty); }
            set { SetValue(SettingIsEnabledProperty, value); }
        }

        public bool SettingEnabledLocked
        {
            get { return (bool)GetValue(SettingEnabledLockedProperty); }
            set { SetValue(SettingEnabledLockedProperty, value); }
        }

        public string SettingName
        {
            get { return (string)GetValue(SettingNameProperty); }
            set { SetValue(SettingNameProperty, value); }
        }

        public string SettingDescription
        {
            get { return (string)GetValue(SettingDescriptionProperty); }
            set { SetValue(SettingDescriptionProperty, value); }
        }


        public static readonly DependencyProperty SettingIsEnabledProperty = DependencyProperty.Register(
            nameof(SettingIsEnabled),
            typeof(bool),
            typeof(LabeledCompileSetting),
            new PropertyMetadata(true)
        );

        public static readonly DependencyProperty SettingEnabledLockedProperty = DependencyProperty.Register(
            nameof(SettingEnabledLocked),
            typeof(bool),
            typeof(LabeledCompileSetting),
            new PropertyMetadata(true)
        );

        public static readonly DependencyProperty SettingNameProperty = DependencyProperty.Register(
            nameof(SettingName),
            typeof(string),
            typeof(LabeledCompileSetting),
            new PropertyMetadata("")
        );

        public static readonly DependencyProperty SettingDescriptionProperty = DependencyProperty.Register(
            nameof(SettingDescription),
            typeof(string),
            typeof(LabeledCompileSetting),
            new PropertyMetadata("")
        );

        public LabeledCompileSetting()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
