using CoinSight.Core.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CoinSight.Controllers;

public class ControllerBase : Controller
{
    public bool IsBaseRequestValid(RequestBase? request)
    {
        return request != null && !string.IsNullOrEmpty(request.CoinId);
    }
}