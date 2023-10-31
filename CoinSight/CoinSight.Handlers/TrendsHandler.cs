using System.Text.Json;
using CoinSight.Core.Enums;
using CoinSight.Core.Interfaces;
using CoinSight.Core.Models.Analysis;

namespace CoinSight.Handlers;

public class TrendsHandler : ITrendsHandler
{
    private readonly ICoinGeckoClient _client;

    public TrendsHandler(ICoinGeckoClient client)
    {
        _client = client;
    }

    public async Task<RecommendationsAnalysis> GetReccomendations(string coinId, int days, int topN)
    {
        var targetTrend = await DetermineTrend(coinId, days);

        JsonDocument topCoinsResponse = await _client.GetTopCoinsByMarketCap(topN);
        var topCoins = topCoinsResponse.RootElement.EnumerateArray().ToList();
        var analysis = new RecommendationsAnalysis()
        {
            Reccomendations = new List<string>(),
        };

        foreach (var coin in topCoins)
        {
            string? id = coin.GetProperty("id").GetString();
            
            if (id == null)
                throw new HttpRequestException();
            
            var coinTrend = await DetermineTrend(id, days);

            if (coinTrend == targetTrend)
                analysis.Reccomendations.Add(id);
        }
        
        return analysis;
    }
    
    public async Task<Trend> DetermineTrend(string coinId, int days)
    {
        var historicalData = await _client.GetHistoricalData(coinId, days);
        var currentPrice = historicalData.RootElement.GetProperty("prices").EnumerateArray().Last()[1].GetDecimal();
        var averagePrice = historicalData.RootElement.GetProperty("prices").EnumerateArray().Average(dataPoint => dataPoint[1].GetDecimal());
    
        return currentPrice >= averagePrice ? Trend.Upward : Trend.Downward;
    }
}