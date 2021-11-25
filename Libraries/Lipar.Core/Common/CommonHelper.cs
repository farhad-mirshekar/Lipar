using Lipar.Core.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace Lipar.Core.Common
{
    public class CommonHelper
    {
        public static ILiparFileProvider DefaultFileProvider { get; set; }
        public static int GetPersianDay(DateTime dateTime)
        => new PersianCalendar().GetDayOfMonth(dateTime);
        /// <summary>
        /// Get Name Month Fa
        /// </summary>
        /// <param name="dateTime">Date Type : DateTime</param>
        /// <returns></returns>
        public static string GetPersianMonthName(DateTime dateTime)
        {
            return dateTime.Month switch
            {
                1 => "فروردین",
                2 => "اردیبهشت",
                3 => "خرداد",
                4 => "تیر",
                5 => "مرداد",
                6 => "شهریور",
                7 => "مهر",
                8 => "آبان",
                9 => "آذر",
                10 => "دی",
                11 => "بهمن",
                12 => "اسفند",

            };
        }

        /// <summary>
        /// Get Year Fa
        /// </summary>
        /// <param name="dateTime">Date Type : DateTime</param>
        /// <returns></returns>
        public static int GetPersianYear(DateTime dateTime)
        => new PersianCalendar().GetYear(dateTime);

        /// <summary>
        /// Get Month Fa
        /// </summary>
        /// <param name="dateTime">Date Type : DateTime</param>
        /// <returns></returns>
        public static int GetPersianMonth(DateTime dateTime)
        => new PersianCalendar().GetMonth(dateTime);

        /// <summary>
        /// Get Field Such As : Title,Name,... In Model By Length
        /// </summary>
        /// <param name="Title">Such As Title,Name , In Your Model</param>
        /// <param name="length">Length Return</param>
        /// <returns></returns>
        public static string GetFieldByLength(string Title, int length = 100)
        => Title.Length > length ? Title.Substring(0, length) + " ..." : Title;

        /// <summary>
        /// Check Extention Media File
        /// </summary>
        /// <param name="Ext">Extention File</param>
        /// <returns></returns>
        public static bool IsValidateExtention(string Ext)
        {
            var imgExt = new List<string>
            {
                ".bmp",
                ".gif",
                ".jpeg",
                ".jpg",
                ".jpe",
                ".jfif",
                ".pjpeg",
                ".pjp",
                ".png",
                ".tiff",
                ".tif"
            } as IReadOnlyCollection<string>;

            if (imgExt.All(ext => !ext.Equals(Ext, StringComparison.CurrentCultureIgnoreCase)))
                return false;

            return true;
        }

        /// <summary>
        /// Get Current DateTime
        /// </summary>
        /// <returns></returns>
        public static DateTime GetCurrentDateTime()
            => DateTime.Now;

        /// <summary>
        /// Converts a value to a destination type.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="destinationType">The type to convert the value to.</param>
        /// <returns>The converted value.</returns>
        public static object To(object value, Type destinationType)
        {
            return To(value, destinationType, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts a value to a destination type.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="destinationType">The type to convert the value to.</param>
        /// <param name="culture">Culture</param>
        /// <returns>The converted value.</returns>
        public static object To(object value, Type destinationType, CultureInfo culture)
        {
            if (value == null)
                return null;

            var sourceType = value.GetType();

            var destinationConverter = TypeDescriptor.GetConverter(destinationType);
            if (destinationConverter.CanConvertFrom(value.GetType()))
                return destinationConverter.ConvertFrom(null, culture, value);

            var sourceConverter = TypeDescriptor.GetConverter(sourceType);
            if (sourceConverter.CanConvertTo(destinationType))
                return sourceConverter.ConvertTo(null, culture, value, destinationType);

            if (destinationType.IsEnum && value is int)
                return Enum.ToObject(destinationType, (int)value);

            if (!destinationType.IsInstanceOfType(value))
                return Convert.ChangeType(value, destinationType, culture);

            return value;
        }

        /// <summary>
        /// Converts a value to a destination type.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <typeparam name="T">The type to convert the value to.</typeparam>
        /// <returns>The converted value.</returns>
        public static T To<T>(object value)
        {
            //return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
            return (T)To(value, typeof(T));
        }

        /// <summary>
        /// serialize by json convert
        /// </summary>
        /// <param name="value"></param>
        public static string SerializeObject(object value)
            => JsonConvert.SerializeObject(value);

        /// <summary>
        /// deserialize by json convert
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(string value)
            => JsonConvert.DeserializeObject<T>(value);

        /// <summary>
        /// get persian day by format
        /// </summary>
        /// <param name="format">format:
        /// 1:YYYY/MM/DD
        /// </param>
        /// <param name="dateTime">datetime</param>
        /// <returns></returns>
        public string GetPersianDateByFormat(int format, DateTime? dateTime)
        {
            switch (format)
            {
                case 1:
                    return PersianDateFormat1(dateTime);
            }

            return string.Empty;
        }

        /// <summary>
        /// YYYY/MM/DD
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private string PersianDateFormat1(DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                return $"{GetPersianYear(dateTime.Value)}/{GetPersianMonth(dateTime.Value)}/{GetPersianDay(dateTime.Value)}";
            }
            return string.Empty;
        }

        /// <summary>
        /// check password is safe
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool IsSafePassword(string password)
        {
            var badPasswords = new HashSet<string>
            {
                "password",
                "password1",
                "123456",
                "12345678",
                "1234",
                "qwerty",
                "12345",
                "dragon",
                "******",
                "baseball",
                "football",
                "letmein",
                "monkey",
                "696969",
                "abc123",
                "mustang",
                "michael",
                "shadow",
                "master",
                "jennifer",
                "111111",
                "2000",
                "jordan",
                "superman",
                "harley",
                "1234567",
                "iloveyou",
                "trustno1",
                "sunshine",
                "123123",
                "welcome"
            };

            if (string.IsNullOrWhiteSpace(password))
            {
                return false;
            }
            if (password.Length < 5)
            {
                return false;
            }
            if (badPasswords.Contains(password.ToLowerInvariant()))
            {
                return false;
            }
            if (AreAllCharsEuqal(password))
            {
                return false;
            }

            return true;
        }

        public static bool AreAllCharsEuqal(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                return false;
            }
            data = data.ToLowerInvariant();
            var firstElement = data.ElementAt(0);
            var euqalCharsLen = data.ToCharArray().Count(x => x == firstElement);
            if (euqalCharsLen == data.Length)
            {
                return true;
            }

            return false;
        }
    }
}
