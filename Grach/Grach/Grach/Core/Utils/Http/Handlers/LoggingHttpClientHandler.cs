using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Grach.Core.Interfaces;

namespace Grach.Core.Utils.Http.Handlers
{
    public class LoggingHttpClientHandler : DelegatingHandler
    {
        private readonly ILoggingServiceProvider _logger;

        public LoggingHttpClientHandler(ILoggingServiceProvider logger)
        {
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            string content = null;
            HttpResponseMessage result = null;
            try
            {
                _logger.Debug(await GetFormattedString(request));

                result = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
                content = await result.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                _logger.Debug(ex.Message);
                throw;
            }
            finally
            {
                stopwatch.Stop();

                string uri = request.RequestUri.AbsoluteUri;
                _logger.Debug($"{uri}\n{stopwatch.ElapsedMilliseconds} ms \nContent:\n{content}");
            }

            return result;
        }

        private async Task<string> GetFormattedString(HttpRequestMessage request)
        {
            var res = new StringBuilder("Request\n{  '" + request.RequestUri + "'\n");

            if (request.Headers.Any())
            {
                res.Append("Headers:\n");
                foreach (var header in request.Headers.Select(x => $" {x.Key}: {GetFormattedString(x.Value)}\n"))
                {
                    res.Append(header);
                }
            }

            if (request.Properties.Any())
            {
                res.Append("Properties:\n");
                foreach (var str in request.Properties.Select(x => string.Format($" {x.Key}  : {x.Value}\n")))
                {
                    res.Append(str);
                }
            }

            if (request.Content != null)
            {
                var content = await request.Content.ReadAsStringAsync();
                res.Append(content + "\n");
            }

            return res.Append("}").ToString();
        }

        private string GetFormattedString(IEnumerable<string> values)
        {
            var res = new StringBuilder("{ ");
            foreach(var value in values)
            {
                res.Append($"{value},");
            }
            res.Remove(res.Length - 1, 1);
            return res.Append(" }").ToString();
        }
    }
}