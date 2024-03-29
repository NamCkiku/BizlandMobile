﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace Bizland.Utilities.Extensions
{
    public static class Extensions
    {
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static string ToDescription<TEnum>(this TEnum EnumValue) where TEnum : struct
        {
            return GetEnumDescription((Enum)(object)EnumValue);
        }

        public static string CurrencyFormat(string text, string format)
        {
            if (!string.IsNullOrEmpty(text) && text.Contains(".")) text = text.Replace(".", "");

            if (decimal.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out var returnVal))
            {
                string s = returnVal.ToString(format, CultureInfo.InvariantCulture);
                return s;
            }
            return text;
        }

        public static IList<dynamic> ListFrom<T>(this Enum value)
        {
            var list = new List<dynamic>();
            var enumType = value.GetType();

            foreach (var o in Enum.GetValues(enumType))
            {
                list.Add(new
                {
                    Name = Enum.GetName(enumType, o),
                    Value = (int)o
                });
            }

            return list;
        }

        public static T ToEnum<T>(this string enumString)
        {
            return (T)Enum.Parse(typeof(T), enumString);
        }

        public static ICollection<EnumValue> ConvertEnumToList<T>() where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new Exception("Type given T must be an Enum");
            }

            var result = Enum.GetValues(typeof(T))
                             .Cast<T>()
                             .Select(x => new EnumValue
                             {
                                 Key = Convert.ToInt32(x),
                                 Value = x.ToDescription()
                             })
                             .ToList()
                             .AsReadOnly();

            return result;
        }

        public static string GetDescription(this Enum enumValue)
        {
            FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return enumValue.ToString();
        }

        /// <summary>
        /// Convert the hexa to int.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// Namth  11/5/2012   created
        /// </Modified>
        public static int ConvertHexToInt(this string value)
        {
            int ret = 0;
            try
            {
                // strip the leading 0x
                if (value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                {
                    value = value.Substring(2);
                }
                if (value.StartsWith("#", StringComparison.OrdinalIgnoreCase))
                {
                    value = value.Substring(1);
                }

                ret = Int32.Parse(value, NumberStyles.HexNumber);
            }
            catch
            {
                // ignored
            }
            return ret;
        }

        public static T DeepClone<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }

        /// <summary>
        /// Convert the int to hexa.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// Namth  11/5/2012   created
        /// </Modified>
        public static string ConvertIntToHex(this int value)
        {
            return value.ToString("X").PadLeft(6, '0');
        }
    }

    public class EnumValue
    {
        public int Key { get; set; }

        public string Value { get; set; }
    }
}