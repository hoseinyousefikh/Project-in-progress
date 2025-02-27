﻿using System.ComponentModel.DataAnnotations;

namespace DwellMVC.Areas.Admin.CustomAuthorize
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            var attribute = enumValue.GetType()
                .GetField(enumValue.ToString())
                .GetCustomAttributes(typeof(DisplayAttribute), false)
                .FirstOrDefault() as DisplayAttribute;

            return attribute?.Name ?? enumValue.ToString();
        }
    }
}
