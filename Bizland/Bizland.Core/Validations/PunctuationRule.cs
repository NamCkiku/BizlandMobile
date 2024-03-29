﻿using System.Text.RegularExpressions;

namespace Bizland.Core
{
    public class PunctuationRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value as string;
            Regex regex = new Regex(@"[^a-zA-Z0-9]");
            Match match = regex.Match(str);

            return !match.Success;
        }
    }
}