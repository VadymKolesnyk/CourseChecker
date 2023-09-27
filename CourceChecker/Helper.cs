﻿using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;

namespace CourceChecker;

static class Helper
{
    public static T ToObject<T>(this DataRow dataRow) where T : new()
    {
        T item = new T();

        foreach (DataColumn column in dataRow.Table.Columns)
        {
            PropertyInfo property = GetProperty(typeof(T), column.ColumnName);

            if (property != null && dataRow[column] != DBNull.Value && dataRow[column].ToString() != "NULL")
            {
                var value = dataRow[column];
                property.SetValue(item, ChangeType((value as string)?.Trim() ?? value, property.PropertyType), null);
            }
        }

        return item;
    }

    public static PropertyInfo GetProperty(Type type, string attributeName)
    {
        PropertyInfo property = type.GetProperty(attributeName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        if (property != null)
        {
            return property;
        }

        return type.GetProperties(BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
             .Where(p => p.IsDefined(typeof(DisplayAttribute), false) && p.GetCustomAttributes(typeof(DisplayAttribute), false).Cast<DisplayAttribute>().Single().Name.Equals(attributeName, StringComparison.InvariantCultureIgnoreCase))
             .FirstOrDefault();
    }

    public static object ChangeType(object value, Type type)
    {
        if (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
        {
            if (value == null)
            {
                return null;
            }

            return Convert.ChangeType(value, Nullable.GetUnderlyingType(type));
        }

        return Convert.ChangeType(value, type);
    }
}
