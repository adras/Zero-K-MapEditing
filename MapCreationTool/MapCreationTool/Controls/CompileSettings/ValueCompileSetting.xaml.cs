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

namespace MapCreationTool.Controls.CompileSettings
{
	/// <summary>
	/// Interaction logic for ValueCompileSetting.xaml
	/// </summary>
	public partial class ValueCompileSetting : UserControl
	{
		public bool SettingIsEnabled
		{
			get { return (bool)GetValue(SettingIsEnabledProperty); }
			set { SetValue(SettingIsEnabledProperty, value); }
		}

		public static readonly DependencyProperty SettingIsEnabledProperty = DependencyProperty.Register(
			nameof(SettingIsEnabled),
			typeof(bool),
			typeof(ValueCompileSetting),
			new PropertyMetadata(true)
		);

		public bool SettingEnabledLocked
		{
			get { return (bool)GetValue(SettingEnabledLockedProperty); }
			set { SetValue(SettingEnabledLockedProperty, value); }
		}

		public static readonly DependencyProperty SettingEnabledLockedProperty = DependencyProperty.Register(
			nameof(SettingEnabledLocked),
			typeof(bool),
			typeof(ValueCompileSetting),
			new PropertyMetadata(true)
		);


		public string SettingName
		{
			get { return (string)GetValue(SettingNameProperty); }
			set { SetValue(SettingNameProperty, value); }
		}
		public static readonly DependencyProperty SettingNameProperty = DependencyProperty.Register(
			nameof(SettingName),
			typeof(string),
			typeof(ValueCompileSetting),
			new PropertyMetadata("")
		);


		public string SettingValue
		{
			get { return (string)GetValue(SettingValueProperty); }
			set { SetValue(SettingValueProperty, value); }
		}
		public static readonly DependencyProperty SettingValueProperty = DependencyProperty.Register(
			nameof(SettingValue),
			typeof(string),
			typeof(ValueCompileSetting),
			new PropertyMetadata("")
		);


		public string SettingDescription
		{
			get { return (string)GetValue(SettingDescriptionProperty); }
			set { SetValue(SettingDescriptionProperty, value); }
		}
		public static readonly DependencyProperty SettingDescriptionProperty = DependencyProperty.Register(
			nameof(SettingDescription),
			typeof(string),
			typeof(ValueCompileSetting),
			new PropertyMetadata("")
		);


		public bool SettingHasFileBrowser
		{
			get { return (bool)GetValue(SettingHasFileBrowserProperty); }
			set { SetValue(SettingHasFileBrowserProperty, value); }
		}
		public static readonly DependencyProperty SettingHasFileBrowserProperty = DependencyProperty.Register(
			nameof(SettingHasFileBrowser),
			typeof(bool),
			typeof(ValueCompileSetting),
			new PropertyMetadata(false)
		);



		public ValueCompileSetting()
		{
			InitializeComponent();
			DataContext = this;

			// Default value
			SettingEnabledLocked = false;
		}

		private void btnBrowseFile_Click(object sender, RoutedEventArgs e)
		{
			Forms.SaveFileDialog dialog = new Forms.SaveFileDialog();
			// This is crappy and unflexible, make it a dependencyproperty
			dialog.Filter = "smf files (*.smf)|*.smt|All files (*.*)|*.*";
			dialog.CheckFileExists = false;
			dialog.AddExtension = true;
			Forms.DialogResult result = dialog.ShowDialog();

			if (result != Forms.DialogResult.OK)
				return;

			SettingValue = dialog.FileName;
		}
	}
}
