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
	public enum SettingDialogType
    {
		None,
		Open,
		Save,
    }

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

		public SettingDialogType SettingDialogType
		{
			get { return (SettingDialogType)GetValue(SettingDialogTypeProperty); }
			set { SetValue(SettingDialogTypeProperty, value); }
		}
		public static readonly DependencyProperty SettingDialogTypeProperty = DependencyProperty.Register(
			nameof(SettingDialogType),
			typeof(SettingDialogType),
			typeof(ValueCompileSetting),
			new PropertyMetadata(SettingDialogType.None)
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


		public string FileDialogFilter
		{
			get { return (string)GetValue(FileDialogFilterProperty); }
			set { SetValue(FileDialogFilterProperty, value); }
		}
		public static readonly DependencyProperty FileDialogFilterProperty = DependencyProperty.Register(
			nameof(FileDialogFilter),
			typeof(string),
			typeof(ValueCompileSetting),
			new PropertyMetadata("")
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
			Forms.FileDialog dialog = null;
            switch (SettingDialogType)
            {
                case SettingDialogType.Open:
					dialog = new Forms.OpenFileDialog();
					break;
                case SettingDialogType.Save:
					dialog = new Forms.SaveFileDialog();
                    break;
			}

			// This is crappy and unflexible, make it a dependencyproperty
			dialog.Filter = FileDialogFilter;
			dialog.CheckFileExists = false;
			dialog.AddExtension = true;
			Forms.DialogResult result = dialog.ShowDialog();

			if (result != Forms.DialogResult.OK)
				return;

			SettingValue = dialog.FileName;
		}
	}
}
