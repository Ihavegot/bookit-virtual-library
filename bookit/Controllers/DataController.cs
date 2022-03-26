using bookit.Data;
using bookit.Models;
using Microsoft.AspNetCore.Mvc;

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

    public IActionResult Edit(int id)
    {
        DataModel dataModel = new DataModel();
        DataDAO datadao = new DataDAO();
        dataModel = datadao.FetchOne(id);
        return View("EditForm", dataModel);
    }

    public IActionResult ProcessCreate(DataModel createData)
    {
        DataDAO datadao = new DataDAO();
        datadao.CreateOrUpdate(createData);
        return View("Details", createData);
    }

    public IActionResult Delete(int id)
    {
        DataDAO datadao = new DataDAO();
        datadao.Delete(id);
        List<DataModel> dataModel = new List<DataModel>();
        dataModel = datadao.FetchAll();

        return View("Index", dataModel);
    }

}
