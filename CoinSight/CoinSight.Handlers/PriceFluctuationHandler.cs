using CoinSight.Core.Interfaces;
using CoinSight.Core.Models.Analysis;

namespace CoinSight.Handlers;

public class PriceFluctuationHandler : IPriceFluctuationHandler
{
    private readonly ICoinGeckoClient _client;
    
    public PriceFluctuationHandler(ICoinGeckoClient client)
    {
        _client = client;
    }

    public async Task<PriceFluctuationAnalysis> AnalysePriceFluctuations(string coinId, int days)
    {
        var historicalData = await _client.GetHistoricalData(coinId, days);
        var prices = historicalData.RootElement.GetProperty("prices").EnumerateArray().ToList();

        var fluctuations = new List<decimal>();
        for (var i=1; i<prices.Count; i++)
        {
            var todayPrice = prices[i][1].GetDecimal();
            var yesterdayPrice = prices[i-1][1].GetDecimal();
            var fluctuation = Math.Abs(todayPrice - yesterdayPrice);
            fluctuations.Add(fluctuation);
        }

        return new PriceFluctuationAnalysis
        {
            AverageFluctuation = fluctuations.Average(),
            HighestFluctuation = fluctuations.Max(),
            Interval = days switch
            {
                1 => "5 minutes",
                > 1 and < 90 => "1 hour",
                >= 90 => "1 day",
                _ => "Unknown" 
            }
        };
    }
}