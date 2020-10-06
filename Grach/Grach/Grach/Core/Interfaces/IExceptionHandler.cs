using System;

namespace Grach.Core.Interfaces
{
    public interface IExceptionHandler
    {
        Exception HandleException(Exception exception);
    }
}