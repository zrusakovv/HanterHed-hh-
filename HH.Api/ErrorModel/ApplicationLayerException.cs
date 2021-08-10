﻿using System;
using System.Runtime.Serialization;

namespace HanterHed_hh_.ErrorModel
{
    public class ApplicationLayerException: Exception
    {
        public ApplicationLayerException()
        {
        }

        public ApplicationLayerException(string message) : base(message)
        {
        }

        public ApplicationLayerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ApplicationLayerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
