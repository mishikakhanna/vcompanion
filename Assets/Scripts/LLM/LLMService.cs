using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace LLM.Services
{
    /// <summary>
    /// Manages the Local Language Model integration
    /// - Handles model initialization
    /// - Manages text generation
    /// - Implements response caching
    /// - Controls model resources
    /// </summary>
    public class LLMService : IDisposable
    {
        private Dictionary<string, string> responseCache = new Dictionary<string, string>();
        private const int MAX_CACHE_SIZE = 100;
        private readonly HttpClient httpClient;
        private const string apiKey = "AIzaSyAYiNog9uPIXBaJXkkqjDMtgPFQrAOGTVc";
        private const string apiUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key=" + apiKey;

        public LLMService()
        {
            httpClient = new HttpClient();
        }

        public async Task<string> GenerateResponse(string input)
        {
            // Check cache first
            if (responseCache.ContainsKey(input))
            {
                return responseCache[input];
            }

            // Implement actual LLM integration
            string response = await CallLLMApi(input);

            // Cache the response
            if (responseCache.Count >= MAX_CACHE_SIZE)
            {
                // Remove oldest entry
                var enumerator = responseCache.GetEnumerator();
                enumerator.MoveNext();
                responseCache.Remove(enumerator.Current.Key);
            }
            responseCache[input] = response;

            return response;
        }

        private async Task<string> CallLLMApi(string input)
        {
            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new { text = input }
                        }
                    }
                }
            };

            var jsonContent = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(apiUrl, content);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<ResponseObject>(responseString);

            return responseObject.contents[0].parts[0].text;
        }

        private class ResponseObject
        {
            public Content[] contents { get; set; }
        }

        private class Content
        {
            public Part[] parts { get; set; }
        }

        private class Part
        {
            public string text { get; set; }
        }

        public void Dispose()
        {
            responseCache.Clear();
            httpClient.Dispose();
        }
    }
}