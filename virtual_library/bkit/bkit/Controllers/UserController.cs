using bkit.Data;
using bkit.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace bkit.Controllers
{
    public class UserController : Controller
    {

        [Authorize]
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

        public IActionResult Borrow(int book, string user)
        {
            DataDAO datadao = new DataDAO();
            datadao.Borrow(book, user);
            List<DataModel> dataModel = new List<DataModel>();
            dataModel = datadao.FetchAll();

            return View("List", dataModel);
        }

        [HttpGet]
        public IActionResult Borrowed(string id)
        {
            List<DataModel> dataModel = new List<DataModel>();
            DataDAO datadao = new DataDAO();
            dataModel = datadao.FetchBorrowed(id);

            return View("Borrowed", dataModel);
        }

        public IActionResult Return(int book, string user)
        {
            DataDAO datadao = new DataDAO();
            datadao.Return(book, user);
            List<DataModel> dataModel = new List<DataModel>();
            dataModel = datadao.FetchAll();

            return View("List", dataModel);
        }
    }
}
