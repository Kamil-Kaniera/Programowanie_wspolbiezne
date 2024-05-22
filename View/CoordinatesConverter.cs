using System.Globalization;
using System.Windows.Data;

namespace View
{
    public class CoordinatesConverter : IValueConverter
    {
        private const int Rescale = 100;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value / Rescale;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
