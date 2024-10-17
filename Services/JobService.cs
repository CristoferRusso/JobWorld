// Services/JobService.cs
using JobWorld.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Formatting; 

namespace JobWorld.Services
{
    public class JobService
    {
        private readonly HttpClient _httpClient;

        public JobService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Job>> GetJobsFromApiAsync()
        {
            // Logica per chiamare le API di terze parti e ottenere le offerte di lavoro
            var response = await _httpClient.GetAsync("URL_DELL_API_DE_OFFERTI_DI_LAVORO");
            response.EnsureSuccessStatusCode();
            var jobs = await response.Content.ReadAsAsync<IEnumerable<Job>>();
            return jobs;
        }
    }
}
