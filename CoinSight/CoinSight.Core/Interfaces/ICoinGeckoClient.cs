using System.Text.Json;

namespace CoinSight.Core.Interfaces;

public interface ICoinGeckoClient
{
    Task<JsonDocument> GetHistoricalData(string coinId, int days);
    Task<JsonDocument> GetTopCoinsByMarketCap(int topN);
}