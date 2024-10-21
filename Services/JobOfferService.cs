using JobWorld.Data;
using Newtonsoft.Json;
using System.Net;
using JobWorld.Models;


namespace JobWorld.Services
{
    public class JobOfferService
    {
          private readonly string apiKey = "6f338763-d154-4bc5-88d9-5f5d024ca85b";
    private readonly string apiUrl = "https://jooble.org/api/";

    public async Task<dynamic> GetJobOffersAsync(string keywords, string location)
    {
        var url = apiUrl + apiKey;
        var requestBody = JsonConvert.SerializeObject(new { keywords, location });

        // Crea la richiesta
        WebRequest request = HttpWebRequest.Create(url);
        request.Method = "POST";
        request.ContentType = "application/json";

        // Scrivi il body della richiesta
        using (var writer = new StreamWriter(request.GetRequestStream()))
        {
            await writer.WriteAsync(requestBody);
            await writer.FlushAsync();
        }

        // Leggi la risposta
        using (var response = await request.GetResponseAsync())
        using (var reader = new StreamReader(response.GetResponseStream()))
        {
            var jsonResponse = await reader.ReadToEndAsync();
            // Deserializza la risposta JSON
            return jsonResponse;
        }
    }
    }
}
