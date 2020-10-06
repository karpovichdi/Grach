using System;
using System.Net.Http;
using Grach.Core.Utils.Constants;

namespace Grach.Core.Helpers
{
    public static class DefaultRefitHttpClientHelper
    {
        private static HttpClient _defaultHttpClient;

        public static HttpClient GetHttpClient(HttpMessageHandler nativeHandler, params DelegatingHandler[] handlers)
        {
            if (_defaultHttpClient == null)
            {
                foreach (DelegatingHandler handler in handlers)
                {
                    nativeHandler = nativeHandler.AddHttpMessageHandler(handler);
                }
                _defaultHttpClient = new HttpClient(nativeHandler) { BaseAddress = new Uri(ConstantsCore.Api.Host) };
            }
            return _defaultHttpClient;
        }

        private static HttpMessageHandler AddHttpMessageHandler(this HttpMessageHandler innerHandler, DelegatingHandler handler ) 
        {
            handler.InnerHandler = innerHandler;
            return handler;
        }
    }
}