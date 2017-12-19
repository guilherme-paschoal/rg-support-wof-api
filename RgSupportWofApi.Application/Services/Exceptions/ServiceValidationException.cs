using System;
namespace RgSupportWofApi.Application.Services.Exceptions
{
    public class ServiceValidationException : Exception
    {
        public ServiceValidationException(string message) : base(message)
        {
        }
    }
}
