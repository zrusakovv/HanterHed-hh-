using System;
using System.Globalization;

namespace HH.Core
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException() : base()
        {
        }
        public EntityNotFoundException(string message) : base(message)
        {
        }
        public EntityNotFoundException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
