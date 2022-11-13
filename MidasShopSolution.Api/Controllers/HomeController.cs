using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MidasShopSolution.Api.Models;

namespace MidasShopSolution.Api.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IWebHostEnvironment _hostingEnvironment;

    public HomeController(ILogger<HomeController> logger
    , IWebHostEnvironment hostingEnvironment)
    {
        _logger = logger;
        _hostingEnvironment = hostingEnvironment;
    }

    public IActionResult Index()
    {
        return Ok("Oke");
    }
}