using CoinSight.Core.Interfaces;
using CoinSight.Core.Models;
using CoinSight.Core.Models.Analysis;
using CoinSight.Core.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CoinSight.Controllers;

public class PriceFluctuationController : ControllerBase
{
    private readonly IPriceFluctuationHandler _handler;
    
    public PriceFluctuationController(IPriceFluctuationHandler handler)
    {
        _handler = handler;
    }

    [HttpPost("PriceFluctuations")]
    public async Task<IActionResult> AnalysePriceFluctuations([FromBody] PriceFluctuationRequest? request)
    {
        if (!IsBaseRequestValid(request) || request!.Days is < 2 or > 6000)
            return BadRequest("Request object is invalid.");

        try
        {
            PriceFluctuationAnalysis analysis = await _handler.AnalysePriceFluctuations(request!.CoinId, request.Days);
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