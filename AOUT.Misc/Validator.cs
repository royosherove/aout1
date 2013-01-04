using System;

namespace AOUT.Misc
{
    internal class Validator
    {
        public bool IsValid(string password)
        {
            if (!HasNoSpaces(null) && !HasTheLetterA(null))
            {
                return true;
            }

            return HasAtLeast6Chars(null) && HasNoSpaces(null);
        }

        protected internal virtual bool HasAtLeast6Chars(string password)
        {
            throw new NotImplementedException();
        }

        protected internal virtual bool HasNoSpaces(string password)
        {
            return !password.Contains(" ");
        }

        protected internal virtual bool HasTheLetterA(string password)
        {
            throw new NotImplementedException();
        }
    }
}