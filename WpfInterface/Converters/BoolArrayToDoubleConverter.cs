using System;
using System.Globalization;
using System.Windows.Data;
using Logic;
namespace WpfInterface.Converters;
public class BoolArrayToDoubleConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not bool[])
            throw new InvalidCastException("Конвертируемое значение не является типом bool[].");
        return Converter.BoolArrayToDouble((bool[])value);
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}