namespace SwClub.Common.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using SwClub.Common.Constants;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Options;

    public static class ExtensionMethodHelper
    {
        /// <summary>
        /// Convert datetime to string.
        /// </summary>
        /// <param name="dateTime">DateTime.</param>
        /// <returns>String value width format: dd/MM/yyyy.</returns>
        public static string ConvertToString(this DateTime dateTime)
        {
            return dateTime.ToString(DateConstant.DateFormat);
        }

        /// <summary>
        /// Convert datetime to string.
        /// </summary>
        /// <param name="dateTime">DateTime.</param>
        /// <param name="format">Format default: MM/dd/yyyy.</param>
        /// <returns>String value width default format: MM/dd/yyyy.</returns>
        public static string ConvertToString(this DateTime dateTime, string format = DateConstant.DateFormatMMDDYYYY)
        {
            return dateTime.ToString(format);
        }

        /// <summary>
        /// Convert datetime to string.
        /// </summary>
        /// <param name="dateTime">DateTime.</param>
        /// <param name="def">default: dd/MM/yyyy HH:mm.</param>
        /// <returns>String value width format: dd/MM/yyyy HH:mm.</returns>
        public static string ConvertToTime(this DateTime dateTime, string def = DateConstant.DateTimeShortFormat)
        {
            return dateTime.ToString(def);
        }

        /// <summary>
        /// Convert string to date.
        /// </summary>
        /// <param name="input">String.</param>
        /// <param name="format">Format default: dd/MM/yyyy.</param>
        /// <returns>Datetime value.</returns>
        public static DateTime? ConvertToDate(this string input, string format = DateConstant.DateFormat)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }

            try
            {
                return DateTime.ParseExact(input, format, CultureInfo.InvariantCulture);
            }
            catch
            {
                return null;
            }
        }

        public static DateTime ConvertToDateTime(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return DateTime.Now;
            }

            try
            {
                return DateTime.Parse(input);
            }
            catch
            {
                return DateTime.Now;
            }
        }

        /// <summary>
        /// Remove whitespace from string.
        /// </summary>
        /// <param name="text">String.</param>
        /// <returns>New string without whitespace.</returns>
        public static string RemoveWhiteSpaces(this string text)
        {
            return string.IsNullOrEmpty(text) ? text : Regex.Replace(text, @"\s+", string.Empty);
        }

        public static string Base64Encode(this string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(this string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static bool IsBase64String(this string base64)
        {
            if (string.IsNullOrEmpty(base64))
            {
                return false;
            }

            return Convert.TryFromBase64String(base64, new Span<byte>(new byte[base64.Length]), out _);
        }

        public static bool IsEmail(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            string pattern = EmailConstant.Regex;
            RegexOptions options = RegexOptions.IgnoreCase;
            Match m = Regex.Match(text, pattern, options);
            return m.Success;
        }

        public static string HashPassword(this string password)
        {
            PasswordHasher<string> passwordHasher = new(
                    new OptionsWrapper<PasswordHasherOptions>(
                        new PasswordHasherOptions()
                        {
                            CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV3,
                        }));

            string hashPassword = passwordHasher.HashPassword(null, password);
            return hashPassword;
        }

        public static string GetExtentionImage(this byte[] fileBytes)
        {
            var pngFormat = new byte[] { 137, 80, 78, 71 };
            var jpegFormat = new byte[] { 255, 216, 255, 224 };
            var jpegCanonFormat = new byte[] { 255, 216, 255, 225 };

            if (fileBytes.Length >= pngFormat.Length && pngFormat.SequenceEqual(fileBytes.Take(pngFormat.Length)))
            {
                return GlobalConstant.ImageExtentions.Png;
            }
            else if (fileBytes.Length >= jpegFormat.Length && jpegFormat.SequenceEqual(fileBytes.Take(jpegFormat.Length)))
            {
                return GlobalConstant.ImageExtentions.Jpg;
            }
            else if (fileBytes.Length >= jpegCanonFormat.Length && jpegCanonFormat.SequenceEqual(fileBytes.Take(jpegCanonFormat.Length)))
            {
                return GlobalConstant.ImageExtentions.Jpeg;
            }

            return string.Empty;
        }

        public static double Percent(int numerator, int denominator, int degit = GlobalConstant.DecimalNumbers.TwoDecimalNumber)
        {
            if (denominator > 0)
            {
                return Math.Round((double)numerator * 100 / denominator, degit);
            }

            return 0;
        }

        public static decimal RoundingNumber(this decimal input, int decimals = GlobalConstant.DecimalNumbers.TwoDecimalNumber)
        {
            return Math.Round(input, decimals);
        }

        public static string GetDisplayName(this System.Enum enumValue)
        {
            return enumValue.GetType()?
                            .GetMember(enumValue.ToString())?
                            .First()?
                            .GetCustomAttribute<DisplayAttribute>()?
                            .Name;
        }

        public static async Task<byte[]> GetBytes(this IFormFile formFile)
        {
            using var memoryStream = new MemoryStream();
            await formFile.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }

        public static string RemoveSpecialCharacter(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            string pattern = GlobalConstant.RegexCharacterAndDigit;
            RegexOptions options = RegexOptions.Multiline;
            var result = new StringBuilder();
            var matchArray = Regex.Matches(input, pattern, options);
            var index = 0;
            foreach (Match item in matchArray)
            {
                // Do not get last index.
                if (index != matchArray.Count - 1)
                {
                    result.Append(item.Value);
                }

                index++;
            }

            result.Append('.');
            result.Append(matchArray.Last());

            return result.ToString();
        }

        public static bool TryConvertGuid(this string input, out Guid result)
        {
            if (string.IsNullOrEmpty(input))
            {
                result = Guid.Empty;
                return false;
            }

            var format = new Regex("^[A-Fa-f0-9]{8}-([A-Fa-f0-9]{4}-){3}[A-Fa-f0-9]{12}$");
            Match match = format.Match(input);
            if (match.Success)
            {
                result = new Guid(input);
                return true;
            }

            result = Guid.Empty;
            return false;
        }

        public static List<T> Shuffle<T>(this List<T> list)
        {
            int n = list.Count;
            var rng = new Random();
            while (n > 1)
            {
                int k = rng.Next(n--);
                (list[k], list[n]) = (list[n], list[k]);
            }

            return list;
        }

        public static string RemoveMultiSpaceCharacter(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            return Regex.Replace(input, @"\s+", " ");
        }

        public static string GetFileBaseName(this string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }

            string[] pathArr = path.Split('\\');
            string[] fileArr = pathArr.Last().Split('.');
            string fileBaseName = fileArr.First().ToString();
            return fileBaseName;
        }

        public static byte[] ConvertToByteArray(this Stream input)
        {
            if (input == null || input.Length == 0)
            {
                return Array.Empty<byte>();
            }

            var ms = new MemoryStream();
            input.CopyTo(ms);
            return ms.ToArray();
        }

        public static string GetFileNameFromUrl(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            var array = input.Split('/');
            if (array == null)
            {
                return input;
            }

            return array[array.Length - 1];
        }
    }
}
