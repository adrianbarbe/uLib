﻿namespace RemoteFinder.BLL.Exceptions
{
    public class UnauthenticatedException : Exception
    {
        public UnauthenticatedException(string message) : base (message)
        {
        }
    }
}