using bkit.Data;
using bkit.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bkit.Controllers
{
    public class AdminController : Controller
    {
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            List<DataModel> dataModel = new List<DataModel>();
            DataDAO datadao = new DataDAO();
            dataModel = datadao.FetchAll();

            return View("List", dataModel);
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

            return View("List", dataModel);
        }
    }
}
