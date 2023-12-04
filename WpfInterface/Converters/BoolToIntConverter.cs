using System;
using System.Globalization;
using System.Windows.Data;
using Logic;
namespace WpfInterface.Converters;
public class BoolToIntConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not bool)
            throw new InvalidCastException("Конвертируемое значение не является типом bool.");
        return (bool)value ? 1 : 0;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}