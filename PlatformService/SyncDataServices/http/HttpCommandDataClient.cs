using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PlatformService.DTO;

namespace PlatformService.SyncDataServices.http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;
        public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.httpClient = httpClient;
        }

        public async Task SendPlatformCommand(PlatformReadDTO plat)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(plat),
                Encoding.UTF8,
                "application/json"
            );

            var uriBase = configuration["CommandService"];

            var response = await this.httpClient.PostAsync($"{uriBase}/api/c/platforms", httpContent);

            if (response.IsSuccessStatusCode)
                Console.WriteLine("---> sync POST to command service was ok!");
            else
                Console.WriteLine("---> sync POST to command service was NOT ok!");
        }
    }
}