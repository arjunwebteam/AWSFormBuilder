using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;

namespace AWSFormBuilder.Areas.FormBuilder.BLL
{
    public class RateLimiter
    {
        private static readonly Dictionary<string, Tuple<int, DateTime>> Cache = new Dictionary<string, Tuple<int, DateTime>>();
        private static readonly int RequestLimit = 5;
        private static readonly TimeSpan TimeWindow = TimeSpan.FromMinutes(1);
        private static readonly HttpClient client = new HttpClient();

        public static bool IsRequestAllowed(string ip)
        {
            if (Cache.ContainsKey(ip))
            {
                var cacheEntry = Cache[ip];
                var requestCount = cacheEntry.Item1;
                var timestamp = cacheEntry.Item2;

                // If the time window has expired, reset the request count
                if (DateTime.Now - timestamp > TimeWindow)
                {
                    Cache[ip] = new Tuple<int, DateTime>(1, DateTime.Now);
                    return true;
                }

                if (requestCount >= RequestLimit)
                    return false;

                // Increment the request count
                Cache[ip] = new Tuple<int, DateTime>(requestCount + 1, timestamp);
            }
            else
            {
                // Add new entry with 1 request count and current timestamp
                Cache[ip] = new Tuple<int, DateTime>(1, DateTime.Now);
            }
            return true;
        }


       
        static RateLimiter()
        {
            // Set a reasonable timeout for HttpClient requests
            client.Timeout = TimeSpan.FromSeconds(30); // You can adjust this timeout as needed
        }

        public static async Task<bool> IsSpamIPAsync1(string ip)
        {
            bool isSpam = false;

            try
            {
                // Example URL (ensure it's correct for the service you're using)
                string url = $"https://api.some-ip-reputation-service.com/check?ip={ip}";

                Console.WriteLine($"Checking IP reputation for: {ip}");

                // Send HTTP request to the API
                var response = await client.GetAsync(url);

                // Check if the response was successful (status code 200-299)
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    dynamic result = JsonConvert.DeserializeObject(json);
                    isSpam = result.isSpam; // Assume the response has an 'isSpam' flag
                }
                else
                {
                    Console.WriteLine($"Request failed with status code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                // This catches network-related errors such as connectivity issues or DNS resolution failures
                Console.WriteLine($"Network-related error occurred: {ex.Message}");
            }
            catch (TaskCanceledException ex)
            {
                // This catches timeout issues (HttpClient Timeout)
                Console.WriteLine($"Request timed out: {ex.Message}");
            }
            catch (Exception ex)
            {
                // This catches any other unexpected errors
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }

            return isSpam;
        }

        public static async Task<bool> IsSpamIPAsync(string ip)
        {
            string apiKey = "23045e36f12a0d7bf83c469de739e5813e90e1819fb84243a960227dec67fccdc24628db5f7b529a";
            string url = $"https://api.abuseipdb.com/api/v2/check?ipAddress={ip}&maxAgeInDays=90";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Key", apiKey);
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    dynamic result = JsonConvert.DeserializeObject(json);
                    return result.data.abuseConfidenceScore > 50; // Example: flag if abuse score > 50
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                    return false;
                }
            }
        }
    }
}
