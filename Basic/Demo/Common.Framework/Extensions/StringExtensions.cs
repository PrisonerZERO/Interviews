//------------------------------------------------------------------------------------------------------
// <copyright company="Robert M Jordan LLC" division="Bushido">
//     Copyright (c) Robert M. Jordan all rights reserved.
// </copyright>
//------------------------------------------------------------------------------------------------------

namespace Bushido.Common.Framework
{
    using System;
    using System.Security.Cryptography;

    /// <summary>
    /// A container for concrete extension types
    /// </summary>
    public static class StringExtensions
    {
        #region <Methods>

        public static Enum ToEnumValue(this string value, Type enumValue)
        {
            var fieldInfos = enumValue.GetFields();
            
            foreach (var field in fieldInfos)
            {
                var result = field.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];
                var attribute = result != null && result.Length > 0 ? result[0].StringValue : string.Empty;
                
                if (value.Equals(attribute))
                {
                    return (Enum)field.GetValue(enumValue);
                }
            }

            return null;
        }

        #endregion
    }
}