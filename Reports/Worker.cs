using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Reports
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public Worker(ILogger<Worker> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                try
                {
                    _logger.LogInformation("przed");
                    var client = _httpClientFactory.CreateClient("ApiClient");
                    var response = await client.GetAsync("api/clients", stoppingToken);
                    _logger.LogInformation("po");
                    _logger.LogInformation(response.IsSuccessStatusCode.ToString());
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync(stoppingToken);
                        // var clients = JsonSerializer.Deserialize<dynamic>(jsonResponse, new JsonSerializerOptions
                        // {
                        //     PropertyNameCaseInsensitive = true
                        // });
                        //
                        // foreach (var clientData in clients)
                        // {
                        //     // Replace with your PDF generation logic
                        //     GenerateReport(clientData);
                        // }
                    }
                    else
                    {
                        _logger.LogError($"Failed to fetch data from API. Status code: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while fetching data from API.");
                }

                // Wait 24 hours (adjust as needed)
                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
        }

        private void GenerateReport(dynamic clientData)
        {
            // Example logic for generating a report
            _logger.LogInformation($"Generating report for client: {clientData.Name}");
            // Add your PdfService logic here
        }
    }
}
