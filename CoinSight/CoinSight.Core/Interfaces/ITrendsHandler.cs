using CoinSight.Core.Enums;
using CoinSight.Core.Models.Analysis;

namespace CoinSight.Core.Interfaces;

public interface ITrendsHandler
{
    Task<RecommendationsAnalysis> GetReccomendations(string coinId, int days, int topN);
    Task<Trend> DetermineTrend(string coinId, int days);
}