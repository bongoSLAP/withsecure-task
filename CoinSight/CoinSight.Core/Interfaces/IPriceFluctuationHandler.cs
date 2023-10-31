using CoinSight.Core.Models.Analysis;

namespace CoinSight.Core.Interfaces;

public interface IPriceFluctuationHandler
{
    Task<PriceFluctuationAnalysis> AnalysePriceFluctuations(string coinId, int days);
}