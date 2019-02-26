using System;
using Windows.UI.Xaml.Data;

namespace DilbertRetriever {
    public sealed class DateConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            if (value is DateTime) {
                return ((DateTime) value).ToString("d");
            } else {
                throw new ArgumentException("The passed value can not be converted.");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }
}
