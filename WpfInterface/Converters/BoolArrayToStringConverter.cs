using System;
using System.Globalization;
using System.Windows.Data;
using Logic;
namespace WpfInterface.Converters;
public class BoolArrayToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not bool[])
            throw new InvalidCastException("Конвертируемое значение не является типом bool[].");
        return Converter.BoolArrayToString((bool[])value);
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not string)
            throw new InvalidCastException("Конвертируемое значение не является типом string.");
        return Converter.StringToBoolArray((string)value);
    }
}