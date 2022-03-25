using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using bookit.Models;
using bookit.Data;

namespace bookit.Controllers;

public class DataController : Controller
{
    public IActionResult Index()
    {
        List<DataModel> dataModel = new List<DataModel>();
        DataDAO datadao = new DataDAO();
        dataModel = datadao.FetchAll();

        return View("Index", dataModel);
    }

    public IActionResult Details(int output_id)
    {
        DataModel dataModel = new DataModel();
        DataDAO datadao = new DataDAO();
        dataModel = datadao.FetchOne(output_id);
        return View("Details", dataModel);
    }

    public IActionResult Create()
    {
        return View("CreateForm");
    }

    public IActionResult ProcessCreate(DataModel createData)
    {
        DataDAO datadao = new DataDAO();
        datadao.Create(createData);
        return View("Details", createData);
    }

}
