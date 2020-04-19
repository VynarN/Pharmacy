using Pharmacy.Application.Common.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Application.Common.Validators
{
    public static class StringArgumentValidator
    {
        public static void ValidateStringArgument(string stringArgument, string argumentName)
        {
            if (string.IsNullOrEmpty(stringArgument) || string.IsNullOrWhiteSpace(stringArgument))
                throw new ArgumentException(ExceptionStrings.InvalidArgumentString, argumentName);
        }
    }
}
