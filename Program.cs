using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using System.Text;

using Microsoft.Extensions.Configuration;

using SWGOH.DATA.Model;
using System.Text.Json;

namespace SWGOH.DATA
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task Main(string[] args)
        {
            var configuration = CreateConfigurationBuilder().Build();
            var endPoints = configuration.GetSection("endPoints").Get<EndpointData[]>();

            await GenerateData(endPoints);
        }

        private static IConfigurationBuilder CreateConfigurationBuilder() => new ConfigurationBuilder()
        .SetBasePath(AppContext.BaseDirectory)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        private static async Task GenerateData(EndpointData[] endPoints)
        {
            foreach (var endpoint in endPoints)
            {
                var response = await client.GetAsync(endpoint.url);
                var responseContent = string.Empty;

                try
                {
                    response.EnsureSuccessStatusCode();
                    responseContent = await response.Content.ReadAsStringAsync();
                }
                catch (HttpRequestException exception)
                {
                    Console.WriteLine(exception.Message);
                }

                GenerateDataFile(endpoint.name, responseContent);

                if (endpoint.name == "characters")
                {
                    await CheckCharImages(responseContent);
                }
            }
        }

        private static void GenerateDataFile(string fileName, string fileContent)
        {
            var charactersDataBytes = Encoding.UTF8.GetBytes(fileContent);

            using var fileStream = new FileStream($"{AppContext.BaseDirectory}/{fileName}.json", FileMode.Truncate);
            fileStream.Write(charactersDataBytes, 0, charactersDataBytes.Length);
            fileStream.Flush();
        }

        private static async Task CheckCharImages(string responseContent)
        {
            if(!Directory.Exists($"{AppContext.BaseDirectory}/character_images")) {
                Directory.CreateDirectory($"{AppContext.BaseDirectory}/character_images");
            }

            var characterData = JsonSerializer.Deserialize<CharacterData[]>(responseContent);

            foreach (var character in characterData)
            {
                var filePath = $"{AppContext.BaseDirectory}/character_images/{character.BaseId}.png";
                if (File.Exists(filePath))
                {
                    continue;
                }

                await DownloadCharImage(filePath, character.Image);
            }
        }

        private static async Task DownloadCharImage(string filePath, string imageUrl)
        {
            var response = await client.GetByteArrayAsync("http://swgoh.gg" + imageUrl);

            using var fileStream = new FileStream(filePath, FileMode.CreateNew);
            await fileStream.WriteAsync(response, 0, response.Length);
            fileStream.Flush();
        }
    }
}
