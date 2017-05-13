//------------------------------------------------------------------------------------------------------
// <copyright company="Robert M Jordan LLC" division="Bushido">
//     Copyright (c) Robert M. Jordan all rights reserved.
// </copyright>
//------------------------------------------------------------------------------------------------------

namespace Bushido.Common.Framework
{
    using System;

    /// <summary>
    /// A container for concrete extension types
    /// </summary>
    public static class EnumExtensions
    {
        #region <Methods>

        public static string ToStringValue(this Enum value)
        {
            var type = value.GetType();
            var fieldInfo = type.GetField(value.ToString());
            var result = fieldInfo.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];

            return result != null && result.Length > 0 ? result[0].StringValue : string.Empty;
        }

        #endregion
    }
}