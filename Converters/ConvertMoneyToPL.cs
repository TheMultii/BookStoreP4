using System;
using System.Globalization;
using System.Windows.Data;

namespace BookStoreP4.Converters {
    public class ConvertMoneyToPL : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return $"{(float)value:0.00} zł";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
