using CoinSight.Core.Enums;
using CoinSight.Core.Interfaces;
using CoinSight.Core.Models.Analysis;
using CoinSight.Core.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CoinSight.Controllers;

public class TrendsController : ControllerBase
{
    private readonly ITrendsHandler _handler;
    
    public TrendsController(ITrendsHandler handler)
    {
        _handler = handler;
    }
    
    [HttpPost("Trends")]
    public async Task<IActionResult> GetTrend([FromBody] TrendsRequest? request)
    {
        if (!IsBaseRequestValid(request) || request!.Days is < 1 or > 6000)
            return BadRequest("Request object is invalid.");

        try
        {
            Trend analysis = await _handler.DetermineTrend(request!.CoinId, request.Days);
            return Ok(analysis);
        }
        catch (HttpRequestException)
        {
            return StatusCode(500, "Something went wrong. Please try again later.");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpPost("Trends/Reccomendations")]
    public async Task<IActionResult> GetTrendReccomendations([FromBody] ReccomendationsRequest? request)
    {
        if (!IsBaseRequestValid(request) || request!.Days is < 1 or > 6000 || request!.TopN is < 2 or > 10)
            return BadRequest("Request object is invalid.");

        try
        {
            RecommendationsAnalysis analysis = await _handler.GetReccomendations(request!.CoinId, request.Days, request.TopN);
            return Ok(analysis);
        }
        catch (HttpRequestException)
        {
            return StatusCode(500, "Something went wrong. Please try again later.");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}