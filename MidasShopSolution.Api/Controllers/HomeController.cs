using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MidasShopSolution.Api.Models;

namespace MidasShopSolution.Api.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return Ok("Oke");
    }
}