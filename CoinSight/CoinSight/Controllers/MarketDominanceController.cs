using CoinSight.Core.Interfaces;
using CoinSight.Core.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CoinSight.Controllers;

public class MarketDominanceController : ControllerBase
{
    private readonly IMarketDominanceHandler _handler;
    
    public MarketDominanceController(IMarketDominanceHandler handler)
    {
        _handler = handler;
    }
    
    [HttpPost("MarketDominance")]
    public async Task<IActionResult> GetMarketDominance([FromBody] MarketDominanceRequest? request)
    {
        if (!IsBaseRequestValid(request) || request!.TopN is < 2 or > 20000)
            return BadRequest("Request object is invalid.");

        try
        {
            decimal analysis = await _handler.GetMarketDominance(request!.CoinId, request.TopN);
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