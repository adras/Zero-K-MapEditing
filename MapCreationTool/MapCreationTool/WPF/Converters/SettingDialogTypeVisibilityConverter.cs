using MapCreationTool.Controls.CompileSettings;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MapCreationTool.WPF.Converters
{
    [ValueConversion(typeof(SettingDialogType), typeof(Visibility))]
    internal class SettingDialogTypeVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Visibility))
                throw new NotSupportedException($"Can not convert to type {targetType.Name}. Can only convert to {nameof(Visibility)}");
            if (value.GetType() != typeof(SettingDialogType))
                throw new NotSupportedException($"Can not convert from type {value.GetType().Name}. Can only convert to {nameof(SettingDialogType)}");

            SettingDialogType dialogType = (SettingDialogType)value;
            Visibility result = Visibility.Hidden;
            switch (dialogType)
            {
                case SettingDialogType.None:
                    result = Visibility.Hidden;
                    break;
                case SettingDialogType.OpenFile:
                    result = Visibility.Visible;
                    break;
                case SettingDialogType.SaveFile:
                    result = Visibility.Visible;
                    break;
                case SettingDialogType.OpenDirectory:
                    result = Visibility.Visible;
                    break;

            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // It's a bit difficult to figure out if visible/notvisible is an open/save dialog ;). Therefore this is not supported
            throw new NotSupportedException("Conversion in this direction is not supported");
        }
    }
}
