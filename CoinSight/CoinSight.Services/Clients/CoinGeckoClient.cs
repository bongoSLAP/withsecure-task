using System.Net;
using System.Text.Json;
using CoinSight.Core.Interfaces;
using Newtonsoft.Json;

namespace CoinSight.Services.Clients;

public class CoinGeckoClient : ICoinGeckoClient
{
    private readonly HttpClient _http;
    private const string BaseUrl = "https://api.coingecko.com/api/v3";
    private const string MaxDayLimitMessage = "days_limit exceeded permissable value";

    public CoinGeckoClient(IHttpClientFactory factory)
    {
        _http = factory.CreateClient();
        _http.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");
    }
    
    public async Task<JsonDocument> GetHistoricalData(string coinId, int days)
    {
        HttpResponseMessage response = await Get($"{BaseUrl}/coins/{coinId}/market_chart?vs_currency=usd&days={days}&interval=daily");

        if (response.StatusCode == HttpStatusCode.NotFound)
            throw new ArgumentException("Coin ID was unrecognised.");
        
        var content = await response.Content.ReadAsStringAsync();
        dynamic? responseObject = JsonConvert.DeserializeObject<dynamic>(content);
        
        if (responseObject == null) 
            throw new HttpRequestException();
            
        if (responseObject.error == MaxDayLimitMessage)
            throw new ArgumentException("The number of days entered exceeds the max limit. Try a smaller number.");

        EnsureNoRateLimit(response.StatusCode);
        response.EnsureSuccessStatusCode();
        
        return JsonDocument.Parse(content);
    }

    public async Task<JsonDocument> GetTopCoinsByMarketCap(int topN)
    {
        HttpResponseMessage response = await Get($"{BaseUrl}/coins/markets?vs_currency=usd&order=market_cap_desc&per_page={topN}&page=1&order_by=market_cap");
        var content = await response.Content.ReadAsStringAsync();
        dynamic? responseObject = JsonConvert.DeserializeObject<dynamic>(content);
        
        if (responseObject == null) 
            throw new HttpRequestException();
        
        EnsureNoRateLimit(response.StatusCode);
        response.EnsureSuccessStatusCode();
    
        return JsonDocument.Parse(content);
    }

    private async Task<HttpResponseMessage> Get(string url)
    {
        HttpResponseMessage response = await _http.GetAsync(url);
        return response;
    }

    private void EnsureNoRateLimit(HttpStatusCode statusCode)
    {
        if (statusCode == HttpStatusCode.TooManyRequests)
            throw new InvalidOperationException("API rate limit exceeded. Please try again later.");
    }
}


