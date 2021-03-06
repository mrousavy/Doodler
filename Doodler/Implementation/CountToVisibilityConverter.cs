﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Doodler.Implementation
{
    /// <summary>
    ///     Convert an integer higher than 0 to <see cref="Visibility.Visible"/>, otherwise <see cref="Visibility.Collapsed"/>
    /// </summary>
    [ValueConversion(typeof(int), typeof(Visibility))]
    public class CountToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case int i:
                    return i > 0 ? Visibility.Collapsed : Visibility.Visible;
                default:
                    return Visibility.Visible;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}