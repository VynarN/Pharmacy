using Pharmacy.Application.Common.Constants;
using System;

namespace Pharmacy.Application.Common.Validators
{
    public static class StringArgumentValidator
    {
        /// <summary>
        /// Validate string argument for being null, empty string or white space.
        /// </summary>
        /// <param name="stringArgument"></param>
        /// <param name="argumentName"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void ValidateStringArgument(string stringArgument, string argumentName)
        {
            if (string.IsNullOrEmpty(stringArgument) || string.IsNullOrWhiteSpace(stringArgument))
                throw new ArgumentException(ExceptionStrings.InvalidArgumentString, argumentName);
        }
    }
}
