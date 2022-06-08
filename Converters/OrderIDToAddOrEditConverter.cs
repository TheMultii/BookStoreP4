using System;
using System.Globalization;
using System.Windows.Data;

namespace BookStoreP4.Converters {
    public class OrderIDToAddOrEditConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is int) {
                return (int)value != 0 ? "Edytuj zamówienie" : "Dodaj zamówienie";
            }
            return "Dodaj zamówienie";
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
