namespace CoinSight.Core.Models.Analysis;

public class PriceFluctuationAnalysis
{
    public decimal AverageFluctuation { get; set; }
    public decimal HighestFluctuation { get; set; }
    public string Interval { get; set; } = string.Empty;
}