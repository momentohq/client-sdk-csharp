﻿namespace MomentoSdk.Exceptions
{
    /// <summary>
    /// Momento Service encountered an unexpected exception while trying to fulfill the request.
    /// </summary>
    public class InternalServerException : MomentoServiceException
    {
        public InternalServerException(string message) : base(message)
        {
        }
    }
}
