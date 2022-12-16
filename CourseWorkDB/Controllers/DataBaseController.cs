using CourseWorkDB.DataBase;
using CourseWorkDB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;
using System.Diagnostics;

namespace CourseWorkDB.Controllers
{
    [Authorize]
    public class DataBaseController : Controller
    {
        private readonly ILogger<DataBaseController> _logger;
        private ChurchParishCourseWorkContext db;
        public DataBaseController(ILogger<DataBaseController> logger, ChurchParishCourseWorkContext context)
        {
            _logger = logger;
            db = context;
        }

        public ActionResult Index()
        {
            return View("Index");
        }
        public ActionResult Priest()
        {
            IEnumerable<Priest> model = db.Priests.AsEnumerable();
            return View("../Priest/Index", model);
        }
        public ActionResult PriestActions(string button, int ids)
        {
            switch (button)
            {
                case "Добавить священнослужителя":
                    return CreatePriest();
                case "Редактировать священнослужителя":
                    return EditPriest(ids);
                case "Удалить священнослужителя":
                    return DeletePriest(ids);
                default:
                    return null;
                    
            }
        }
        [HttpGet]
        public ActionResult CreatePriest()
        {
            return View("../Priest/Create");
        }
        [HttpPost]
        public ActionResult CreatePriest(string title, string name, DateTime initialDate)
        {
            Priest priest = new Priest
            {
                Title = title,
                Name = name,
                InitialDate = initialDate
            };
            db.Priests.Add(priest);
            db.SaveChanges();
            IEnumerable<Priest> model = db.Priests.AsEnumerable();
            return View("../Priest/Index", model);
        }
        [HttpGet]
        public ActionResult EditPriest(int ids)
        {
            var model = db.Priests.SingleOrDefault(p => p.Id == ids);
            return View("../Priest/Edit", model);
        }
        [HttpPost]
        public ActionResult EditPriest(int id, string title, string name, DateTime initialDate)
        {
            var Priest = db.Priests.SingleOrDefault(p => p.Id == id);
            Priest.Title = title;
            Priest.Name = name;
            Priest.InitialDate = initialDate;
            db.Priests.Update(Priest);
            db.SaveChanges();
            IEnumerable<Priest> model = db.Priests.AsEnumerable();
            return View("../Priest/Index", model);
        }
        public ActionResult DeletePriest(int id)
        {
            var Priest = db.Priests.SingleOrDefault(p => p.Id == id);
            db.Remove(Priest);
            db.SaveChanges();
            IEnumerable<Priest> model = db.Priests.AsEnumerable();
            return View("../Priest/Index", model);
        }
        public IActionResult Privacy()
        {
            return View("Privacy");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}