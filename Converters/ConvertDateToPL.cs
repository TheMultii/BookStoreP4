using System;
using System.Globalization;
using System.Windows.Data;

namespace BookStoreP4.Converters {
    public class ConvertDateToPL : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is DateTime) {
                DateTime date = (DateTime)value;
                return $"{date.Day}.{addZero(date.Month)}.{date.Year} {date.Hour}:{date.Minute}:{date.Second}";
            }
            return value;
        }

        private string addZero(int numb) {
            return numb < 10 ? $"0{numb}" : numb.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
