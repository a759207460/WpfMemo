using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfDemo.Common.Conveters
{
    [ValueConversion(typeof(int), typeof(bool))]
    public class IntToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value!=null&&int.TryParse(value.ToString(),out int resulst))
            {
                if (resulst == 1)
                    return false;
            }
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && bool.TryParse(value.ToString(), out bool resulst))
            {
                if (resulst)
                    return 2;
            }
            return 1;
        }
    }
}
