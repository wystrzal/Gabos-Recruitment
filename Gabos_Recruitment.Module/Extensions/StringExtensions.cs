using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gabos_Recruitment.Module.Extensions
{
    public static class StringExtensions
    {
        public static string ChangeUnderlineToSpace(this string value)
        {
            return value.Replace('_', ' ');
        }
        public static string ChangeUnderlineToDash(this string value)
        {
            return value.Replace('_', '-');
        }

        public static string RemoveUnderline(this string value)
        {
            string newValue = value;

            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] == '_')
                {
                    newValue = value.Remove(i, 1);
                }
            }

            return newValue;
        }
    }
}
