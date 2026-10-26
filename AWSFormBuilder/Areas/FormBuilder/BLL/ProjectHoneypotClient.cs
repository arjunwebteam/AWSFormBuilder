using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AWSFormBuilder.Areas.FormBuilder.BLL
{
    public class ProjectHoneypotClient
    {
        private readonly string _apiKey;
        private readonly string _apiUrl;

        public ProjectHoneypotClient(string apiKey, string apiUrl = "https://api.projecthoneypot.org")
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentException("API key cannot be null or empty", nameof(apiKey));

            _apiKey = apiKey;
            _apiUrl = apiUrl;
        }

        public async Task<HoneypotResponse> CheckIPAsync(string ip)
        {
            if (string.IsNullOrWhiteSpace(ip))
                throw new ArgumentException("IP address cannot be null or empty", nameof(ip));

            try
            {
                using (var client = new HttpClient())
                {
                    // Construct the API URL
                    var url = $"{_apiUrl}/{_apiKey}/{ip}";

                    // Log the URL (if needed for debugging)
                    Console.WriteLine($"Calling Project Honeypot API: {url}");

                    // Make the request
                    var response = await client.GetAsync(url);

                    // Check if the request was successful
                    if (!response.IsSuccessStatusCode)
                    {
                        // Log error and rethrow
                        var errorMessage = $"Error while calling Project Honeypot: {response.StatusCode} - {response.ReasonPhrase}";
                        Console.WriteLine(errorMessage);
                        throw new HttpRequestException(errorMessage);
                    }

                    // Parse the response
                    var json = await response.Content.ReadAsStringAsync();

                    // Deserialize the response into HoneypotResponse
                    var honeypotResponse = JsonConvert.DeserializeObject<HoneypotResponse>(json);

                    return honeypotResponse;
                }
            }
            catch (HttpRequestException ex)
            {
                // Handle HTTP-specific exceptions
                Console.WriteLine($"HTTP error in CheckIPAsync: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                // Handle all other exceptions
                Console.WriteLine($"Unexpected error in CheckIPAsync: {ex.Message}");
                throw;
            }
        }
    }

    public class HoneypotResponse
    {
        public bool IsMalicious { get; set; }
        public string ActivityType { get; set; }
        public string LastSeen { get; set; }
        public string ThreatScore { get; set; }

        public string IP { get; set; }
        public bool IsSpam { get; set; }
        public string LastActivity { get; set; }
    }
}
