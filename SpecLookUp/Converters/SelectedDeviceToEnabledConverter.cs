﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace SpecLookUp.Converters
{
    class SelectedDeviceToEnabledConverter :IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null ? true : false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
