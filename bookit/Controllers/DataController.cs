using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using bookit.Models;
using bookit.Data;

namespace bookit.Controllers;

public class DataController : Controller
{
    public IActionResult Index()
    {
        List<DataModel> data = new List<DataModel>();

        DataDAO datadao = new DataDAO();

        data = datadao.FetchAll();

        return View("Index", data);
    }
}
