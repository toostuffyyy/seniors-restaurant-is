using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia.Data.Converters;
using Seniors.Models;

namespace Seniors.Converters;

public class ListTypeDishToStringConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        IList<TypeDish> typeDishes = value as IList<TypeDish>;
        return typeDishes == null ? null : String.Join(", ", typeDishes.Select(a => $"{a.Name}"));
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}