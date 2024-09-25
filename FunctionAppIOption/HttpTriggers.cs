using System.Net;
using FunctionAppIOption.Configuration;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FunctionAppIOption
{
    public class HttpTriggers
    {
        private readonly ILogger _logger;
        private readonly IOptions<AppConfigurations> _appConfigurations;
        private readonly IOptions<AppSettings> _appSettings;
        public HttpTriggers(ILoggerFactory loggerFactory, IOptions<AppConfigurations> appConfigurations, IOptions<AppSettings> appSettings)
        {
            _logger = loggerFactory.CreateLogger<HttpTriggers>();
            _appConfigurations = appConfigurations;
            _appSettings = appSettings;
        }

        [Function("SampleHttpGetOperation")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.WriteAsJsonAsync(
                new 
                { 
                    AccountNumber = _appConfigurations.Value.AccountNumber, 
                    SortCode = _appConfigurations.Value.SortCode,
                    AccountName = _appSettings.Value.AccountName,
                    Bank = _appSettings.Value.Bank,
                    Contact = _appSettings.Value.Contact
                }
            );

            return response;
        }
    }
}
