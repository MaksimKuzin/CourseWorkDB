using CourseWorkDB.DataBase;
using CourseWorkDB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using System.Data.Common;
using System.Diagnostics;
using System.Reflection.Metadata;
using static Azure.Core.HttpHeader;

namespace CourseWorkDB.Controllers
{
    [Authorize]
    public class DataBaseController : Controller
    {
        private readonly ILogger<DataBaseController> _logger;
        private ChurchParishCourseWorkContext db;
        public static int tempEventId;
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
            try
            {
                IEnumerable<Priest> model = db.Priests.AsEnumerable();
                return View("../Priest/Index", model);
            }
            catch
            {
                return View("Error");
            }
        }
        public ActionResult PriestActions(string button, int ids)
        {
            try {
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
            catch
            {
                return View("Error");
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
            try
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
            catch
            {
                return View("Error");
            }
        }
        [HttpGet]
        public ActionResult EditPriest(int ids)
        {
            try
            {
                var priest = db.Priests.SingleOrDefault(p => p.Id == ids);
                if (priest != null)
                    return View("../Priest/Edit", priest);
                else
                {
                    IEnumerable<Priest> model = db.Priests.AsEnumerable();
                    return View("../Priest/Index", model);
                }
            }
            catch
            {
                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult EditPriest(int id, string title, string name, DateTime initialDate)
        {
            try
            {
                var priest = db.Priests.SingleOrDefault(p => p.Id == id);
                priest.Title = title;
                priest.Name = name;
                priest.InitialDate = initialDate;
                db.Priests.Update(priest);
                db.SaveChanges();
                IEnumerable<Priest> model = db.Priests.AsEnumerable();
                return View("../Priest/Index", model);
            }
            catch
            {
                return View("Error");
            }
        }
        public ActionResult DeletePriest(int id)
        {
            try
            {
                var priest = db.Priests.SingleOrDefault(p => p.Id == id);
                db.Remove(priest);
                db.SaveChanges();
                IEnumerable<Priest> model = db.Priests.AsEnumerable();
                return View("../Priest/Index", model);
            }
            catch
            {
                return View("Error");
            }
        }
        public ActionResult Parishioner()
        {
            try
            {
                IEnumerable<Parishioner> model = db.Parishioners;
                foreach (var parishioner in model)
                {
                    var priest = db.Priests.SingleOrDefault(p => p.Id == parishioner.PriestId);
                    parishioner.Priest = priest;
                }
                return View("../Parishioner/Index", model);
            }
            catch
            {
                return View("Error");
            }
        }
        public ActionResult ParishionerByPriestId(int priestId)
        {
            try
            {
                IEnumerable<Parishioner> model = db.Parishioners.Where(p => p.PriestId == priestId);
                foreach (var parishioner in model)
                {
                    var priest = db.Priests.SingleOrDefault(p => p.Id == parishioner.PriestId);
                    parishioner.Priest = priest;
                }
                return View("../Parishioner/Index", model);
            }
            catch
            {
                return View("Error");
            }
        }
        public ActionResult ParishionerActions(string button, int ids)
        {
            try
            {
                switch (button)
                {
                    case "Добавить прихожанина":
                        return CreateParishioner();
                    case "Редактировать прихожанина":
                        return EditParishioner(ids);
                    case "Удалить прихожанина":
                        return DeleteParishioner(ids);
                    default:
                        return null;

                }
            }
            catch
            {
                return View("Error");
            }
        }
        [HttpGet]
        public ActionResult CreateParishioner()
        {
            return View("../Parishioner/Create");
        }
        [HttpPost]
        public ActionResult CreateParishioner(string name, string surname, string patronymic, short age, bool sex, string address, string phoneNumber, int priestId)
        {
            try
            {
                var priest = db.Priests.SingleOrDefault(p => p.Id == priestId);
                var parishioner = new Parishioner
                {
                    Address = address,
                    PhoneNumber = phoneNumber,
                    Name = name,
                    Surname = surname,
                    Patronymic = patronymic,
                    Age = age,
                    Sex = sex,
                    PriestId = priestId,
                    Priest = priest
                };
                if (!priest.Parishioners.Contains(parishioner))
                    priest.Parishioners.Add(parishioner);
                db.Priests.Update(priest);
                db.Parishioners.Add(parishioner);
                db.SaveChanges();
                IEnumerable<Parishioner> model = db.Parishioners;
                foreach (var parish in model)
                {
                    var prst = db.Priests.SingleOrDefault(p => p.Id == parish.PriestId);
                    parish.Priest = prst;
                }
                return View("../Parishioner/Index", model);
            }
            catch
            {
                return View("Error");
            }
        }
        [HttpGet]
        public ActionResult EditParishioner(int id)
        {
            try
            {
                var parishioner = db.Parishioners.SingleOrDefault(p => p.Id == id);
                if (parishioner != null)
                    return View("../Parishioner/Edit", parishioner);
                else
                {
                    IEnumerable<Parishioner> model = db.Parishioners.AsEnumerable();
                    return View("../Parishioner/Index", model);
                }
            }
            catch
            {
                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult EditParishioner(int id, string name, string surname, string patronymic, short age, bool sex, string address, string phoneNumber, int priestId)
        {
            try
            {
                var parishioner = db.Parishioners.SingleOrDefault(p => p.Id == id);
                var priest = db.Priests.SingleOrDefault(p => p.Id == priestId);
                parishioner.Name = name;
                parishioner.Surname = surname;
                parishioner.Patronymic = patronymic;
                parishioner.Age = age;
                parishioner.Address = address;
                parishioner.PhoneNumber = phoneNumber;
                parishioner.PriestId = priestId;
                parishioner.Priest = priest;
                if (!priest.Parishioners.Contains(parishioner))
                    priest.Parishioners.Add(parishioner);
                db.Parishioners.Update(parishioner);
                db.Priests.Update(priest);
                db.SaveChanges();
                IEnumerable<Parishioner> model = db.Parishioners.AsEnumerable();
                foreach (var parish in model)
                {
                    var prst = db.Priests.SingleOrDefault(p => p.Id == parish.PriestId);
                    parish.Priest = prst;
                }
                return View("../Parishioner/Index", model);
            }
            catch
            {
                return View("Error");
            }
        }
        public ActionResult DeleteParishioner(int id)
        {
            try
            {
                SqlParameter parameter = new SqlParameter("@id", id);
                db.Database.ExecuteSqlRaw("OnParishionerDelete @id", parameter);
                var parishioner = db.Parishioners.SingleOrDefault(p => p.Id == id);
                db.Remove(parishioner);
                db.SaveChanges();
                IEnumerable<Parishioner> model = db.Parishioners;
                foreach (var parish in model)
                {
                    var prst = db.Priests.SingleOrDefault(p => p.Id == parish.PriestId);
                    parish.Priest = prst;
                }
                return View("../Parishioner/Index", model);
            }
            catch
            {
                return View("Error");
            }
        }
        public ActionResult Event()
        {
            try
            {
                var model = db.Events;
                return View("../Event/Index", model);
            }
            catch
            {
                return View("Error");
            }
        }
        public ActionResult EventActions(string button, int ids)
        {
            try
            {
                switch (button)
                {
                    case "Добавить службу":
                        return CreateDivineService();
                    case "Добавить священное событие":
                        return CreateSacredEvent();
                    case "Добавить деятельность":
                        return CreateActivity();
                    case "Редактировать мероприятие":
                        return EditEvent(ids);
                    case "Удалить мероприятие":
                        return DeleteEvent(ids);
                    default:
                        return null;
                }
            }
            catch
            {
                return View("Error");
            }
        }
        [HttpGet]
        public ActionResult CreateDivineService()
        {
            return View("../DivineService/Create");
        }
        [HttpPost]
        public ActionResult CreateDivineService(string name, DateTime date, int priestId, string justification, string prayer)
        {
            try
            {
                var @event = new Event
                {
                    Name = name,
                    Date = date,
                    PriestId = priestId,
                    Type = "Служба"
                };
                db.Events.Add(@event);
                db.SaveChanges();
                int maxId = db.Events.Max(e => e.Id);
                @event = db.Events.SingleOrDefault(e => e.Id == maxId);
                var divineService = new DivineService
                {
                    Event = @event,
                    EventId = maxId,
                    Justification = justification,
                    Prayer = prayer
                };
                db.DivineServices.Add(divineService);
                db.SaveChanges();
                var model = db.Events;
                return View("../Event/Index", model);
            }
            catch
            {
                return View("Error");
            }
        }
        public ActionResult AboutDivineService(int id)
        {
            try
            {
                var model = db.Events.SingleOrDefault(e => e.Id == id);
                var divineService = db.DivineServices.SingleOrDefault(e => e.EventId == id);
                model.DivineService = divineService;
                SqlParameter parameter = new SqlParameter("@eId", id);
                var parishionerId = db.Database.SqlQueryRaw<int>("SELECT ParishionerId FROM ParishionerEvent WHERE EventId = @eId", parameter).AsEnumerable().ToList();
                foreach (int pId in parishionerId)
                {
                    var parishioner = db.Parishioners.SingleOrDefault(p => p.Id == pId);
                    model.Parishioners.Add(parishioner);
                }
                var priest = db.Priests.SingleOrDefault(p => p.Id == model.PriestId);
                model.Priest = priest;
                return View("../DivineService/Index", model);
            }
            catch
            {
                return View("Error");
            }
        }
        [HttpGet]
        public ActionResult CreateSacredEvent()
        {
            return View("../SacredEvent/Create");
        }
        [HttpPost]
        public ActionResult CreateSacredEvent(string name, DateTime date, int priestId, string place, DateTime finishDate, string transport, string route, string sourceOfFunding)
        {
            try
            {
                var @event = new Event
                {
                    Name = name,
                    Date = date,
                    PriestId = priestId,
                    Type = "Священное событие"
                };
                db.Events.Add(@event);
                db.SaveChanges();
                int maxId = db.Events.Max(e => e.Id);
                @event = db.Events.SingleOrDefault(e => e.Id == maxId);
                var sEvent = new SacredEvent
                {
                    Place = place,
                    FinishDate = finishDate,
                    Route = route,
                    SourceOfFunding = sourceOfFunding,
                    Transport = transport,
                    EventId = maxId,
                    Event = @event
                };
                db.SacredEvents.Add(sEvent);
                db.SaveChanges();
                var model = db.Events;
                return View("../Event/Index", model);
            }
            catch
            {
                return View("Error");
            }
        }
        public ActionResult AboutSacredEvent(int id)
        {
            try
            {
                var model = db.Events.SingleOrDefault(e => e.Id == id);
                var sEvent = db.SacredEvents.SingleOrDefault(e => e.EventId == id);
                model.SacredEvent = sEvent;
                SqlParameter parameter = new SqlParameter("@eId", id);
                var parishionerId = db.Database.SqlQueryRaw<int>("SELECT ParishionerId FROM ParishionerEvent WHERE EventId = @eId", parameter).AsEnumerable().ToList();
                foreach (int pId in parishionerId)
                {
                    var parishioner = db.Parishioners.SingleOrDefault(p => p.Id == pId);
                    model.Parishioners.Add(parishioner);
                }
                var priest = db.Priests.SingleOrDefault(p => p.Id == model.PriestId);
                model.Priest = priest;
                return View("../SacredEvent/Index", model);
            }
            catch
            {
                return View("Error");
            }
        }
        [HttpGet]
        public ActionResult CreateActivity()
        {
            return View("../Activity/Create");
        }
        [HttpPost]
        public ActionResult CreateActivity(string name, DateTime date, int priestId, short duration, short parishionerAmount)
        {
            try
            {
                var @event = new Event
                {
                    Name = name,
                    Date = date,
                    PriestId = priestId,
                    Type = "Деятельность"
                };
                db.Events.Add(@event);
                db.SaveChanges();
                int maxId = db.Events.Max(e => e.Id);
                @event = db.Events.SingleOrDefault(e => e.Id == maxId);
                var activity = new DataBase.Activity
                {
                    EventId = maxId,
                    Event = @event,
                    ParishionerAmount = parishionerAmount,
                    Duration = duration
                };
                db.Activities.Add(activity);
                db.SaveChanges();
                var model = db.Events;
                return View("../Event/Index", model);
            }
            catch
            {
                return View("Error");
            }
        }
        public ActionResult AboutActivity(int id)
        {
            try
            {
                var model = db.Events.SingleOrDefault(e => e.Id == id);
                var activity = db.Activities.SingleOrDefault(e => e.EventId == id);
                model.Activity = activity;
                SqlParameter parameter = new SqlParameter("@eId", id);
                var parishionerId = db.Database.SqlQueryRaw<int>("SELECT ParishionerId FROM ParishionerEvent WHERE EventId = @eId", parameter).AsEnumerable().ToList();
                foreach (int pId in parishionerId)
                {
                    var parishioner = db.Parishioners.SingleOrDefault(p => p.Id == pId);
                    model.Parishioners.Add(parishioner);
                }
                var priest = db.Priests.SingleOrDefault(p => p.Id == model.PriestId);
                model.Priest = priest;
                return View("../Activity/Index", model);
            }
            catch
            {
                return View("Error");
            }
        }
        [HttpGet]
        public ActionResult EditEvent(int id)
        {
            try
            {
                var model = db.Events.SingleOrDefault(e => e.Id == id);
                var divineService = db.DivineServices.SingleOrDefault(e => e.EventId == id);
                if (divineService != null)
                    model.DivineService = divineService;
                var sEvent = db.SacredEvents.SingleOrDefault(e => e.EventId == id);
                if (sEvent != null)
                    model.SacredEvent = sEvent;
                var activity = db.Activities.SingleOrDefault(e => e.EventId == id);
                if (activity != null)
                    model.Activity = activity;
                return View("../Event/Edit", model);
            }
            catch
            {
                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult EditEvent(int id, string name, DateTime date, int priestId, string justification, string prayer, string place, DateTime finishDate, string transport, string route, string sourceOfFunding, short duration, short amount)
        {
            try
            {
                var @event = db.Events.SingleOrDefault(e => e.Id == id);
                @event.Name = name;
                @event.Date = date;
                @event.PriestId = priestId;
                var divineService = db.DivineServices.SingleOrDefault(e => e.EventId == id);
                if (divineService != null)
                {
                    divineService.Justification = justification;
                    divineService.Prayer = prayer;
                    db.Events.Update(@event);
                    db.DivineServices.Update(divineService);
                    db.SaveChanges();
                }
                var sEvent = db.SacredEvents.SingleOrDefault(e => e.EventId == id);
                if (sEvent != null)
                {
                    sEvent.Place = place;
                    sEvent.FinishDate = finishDate;
                    sEvent.Route = route;
                    sEvent.SourceOfFunding = sourceOfFunding;
                    sEvent.Transport = transport;
                    db.Events.Update(@event);
                    db.SacredEvents.Update(sEvent);
                    db.SaveChanges();
                }
                var activity = db.Activities.SingleOrDefault(e => e.EventId == id);
                if (activity != null)
                {
                    activity.Duration = duration;
                    activity.ParishionerAmount = amount;
                    db.Events.Update(@event);
                    db.Activities.Update(activity);
                    db.SaveChanges();
                }
                var model = db.Events;
                return View("../Event/Index", model);
            }
            catch
            {
                return View("Error");
            }
        }
        public ActionResult DeleteEvent(int id)
        {
            try
            {
                var @event = db.Events.SingleOrDefault(e => e.Id == id);
                db.Remove(@event);
                db.SaveChanges();
                var model = db.Events;
                return View("../Event/Index", model);
            }
            catch
            {
                return View("Error");
            }
        }
        [HttpGet]
        public ActionResult AddParishionersToEvent(int id)
        {
            try
            {
                tempEventId = id;
                IEnumerable<Parishioner> parishioners = db.Parishioners;
                SqlParameter parameter = new SqlParameter("@eId", id);
                var parishionerId = db.Database.SqlQueryRaw<int>("SELECT ParishionerId FROM ParishionerEvent WHERE EventId = @eId", parameter).AsEnumerable().ToList();
                List<Parishioner> model = new();
                foreach (var parishioner in parishioners)
                {
                    if (!parishionerId.Contains(parishioner.Id))
                    {
                        model.Add(parishioner);
                    }
                }
                return View("../Event/AddParishioner", model.AsEnumerable());
            }
            catch
            {
                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult AddParishionersToEvent(string ids)
        {
            try
            {
                List<string> idList = ids.Split(';').ToList();
                foreach (var id in idList)
                {
                    SqlParameter parameter = new SqlParameter("@pId", id);
                    SqlParameter parameter1 = new SqlParameter("@eId", tempEventId);
                    db.Database.ExecuteSqlRaw("AddParishionerEvent @pId , @eId", parameter, parameter1);
                }
                tempEventId = 0;
                IEnumerable<Event> model = db.Events;
                return View("../Event/Index", model);
            }
            catch
            {
                return View("Error");
            }
        }
        [HttpGet]
        public ActionResult DeleteParishionerFromEvent(int id)
        {
            try
            {
                tempEventId = id;
                IEnumerable<Parishioner> parishioners = db.Parishioners;
                SqlParameter parameter = new SqlParameter("@eId", id);
                var parishionerId = db.Database.SqlQueryRaw<int>("SELECT ParishionerId FROM ParishionerEvent WHERE EventId = @eId", parameter).AsEnumerable().ToList();
                List<Parishioner> model = new();
                foreach (var parishioner in parishioners)
                {
                    if (parishionerId.Contains(parishioner.Id))
                    {
                        model.Add(parishioner);
                    }
                }
                return View("../Event/DelParishioner", model.AsEnumerable());
            }
            catch
            {
                return View("Error");
            }
        }
        public ActionResult DeleteParishionerFromEvent(string ids)
        {
            try
            {
                List<string> idList = ids.Split(';').ToList();
                foreach (var id in idList)
                {
                    SqlParameter parameter = new SqlParameter("@pId", id);
                    SqlParameter parameter1 = new SqlParameter("@eId", tempEventId);
                    db.Database.ExecuteSqlRaw("DeleteParishionerEvent @pId , @eId", parameter, parameter1);
                }
                tempEventId = 0;
                IEnumerable<Event> model = db.Events;
                return View("../Event/Index", model);
            }
            catch
            {
                return View("Error");
            }
        }
        public ActionResult Donation()
        {
            try
            {
                IEnumerable<Donation> model = db.Donations;
                foreach (var item in model)
                {
                    var parishioner = db.Parishioners.SingleOrDefault(p => p.Id == item.ParishionerId);
                    item.Parishioner = parishioner;
                }
                return View("../Donation/Index", model);
            }
            catch
            {
                return View("Error");
            }
        }
        public ActionResult DonationActions(string button, int ids)
        {
            try
            {
                switch (button)
                {
                    case "Добавить пожертвование":
                        return CreateDonation();
                    case "Редактировать пожертвование":
                        return EditDonation(ids);
                    case "Удалить пожертвование":
                        return DeleteDonation(ids);
                    default:
                        return null;
                }
            }
            catch
            {
                return View("Error");
            }
        }
        [HttpGet]
        public ActionResult CreateDonation()
        {
            return View("../Donation/Create");
        }
        [HttpPost]
        public ActionResult CreateDonation(string purpose, decimal sum, int parishionerId)
        {
            try
            {
                var donation = new Donation
                {
                    Purpose = purpose,
                    Sum = sum,
                    ParishionerId = parishionerId
                };
                db.Donations.Add(donation);
                db.SaveChanges();
                IEnumerable<Donation> model = db.Donations;
                foreach (var item in model)
                {
                    var parishioner = db.Parishioners.SingleOrDefault(p => p.Id == item.ParishionerId);
                    item.Parishioner = parishioner;
                }
                return View("../Donation/Index", model);
            }
            catch
            {
                return View("Error");
            }
        }
        [HttpGet]
        public ActionResult EditDonation(int id)
        {
            try
            {
                var model = db.Donations.SingleOrDefault(d => d.Id == id);
                return View("../Donation/Edit", model);
            }
            catch
            {
                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult EditDonation(int id, string purpose, decimal sum, int parishionerId)
        {
            try
            {
                var donation = db.Donations.SingleOrDefault(d => d.Id == id);
                donation.Sum = sum;
                donation.ParishionerId = parishionerId;
                donation.Purpose = purpose;
                db.Donations.Update(donation);
                db.SaveChanges();
                IEnumerable<Donation> model = db.Donations;
                foreach (var item in model)
                {
                    var parishioner = db.Parishioners.SingleOrDefault(p => p.Id == item.ParishionerId);
                    item.Parishioner = parishioner;
                }
                return View("../Donation/Index", model);
            }
            catch
            {
                return View("Error");
            }
        }
        public ActionResult DeleteDonation(int id)
        {
            try
            {
                var donation = db.Donations.SingleOrDefault(d => d.Id == id);
                db.Donations.Remove(donation);
                db.SaveChanges();
                IEnumerable<Donation> model = db.Donations;
                foreach (var item in model)
                {
                    var parishioner = db.Parishioners.SingleOrDefault(p => p.Id == item.ParishionerId);
                    item.Parishioner = parishioner;
                }
                return View("../Donation/Index", model);
            }
            catch
            {
                return View("Error");
            }
        }
        public ActionResult DonationByParishionerId(int parishionerId)
        {
            try
            {
                IEnumerable<Donation> model = db.Donations.Where(d => d.ParishionerId == parishionerId);
                foreach (var item in model)
                {
                    var parishioner = db.Parishioners.SingleOrDefault(p => p.Id == item.ParishionerId);
                    item.Parishioner = parishioner;
                }
                return View("../Donation/Index", model);
            }
            catch
            {
                return View("Error");
            }
        }
        public ActionResult Inventory()
        {
            try
            {
                IEnumerable<Inventory> model = db.Inventories;
                foreach (var item in model)
                {
                    var @event = db.Events.SingleOrDefault(e => e.Id == item.EventId);
                    item.Event = @event;
                }
                return View("../Inventory/Index", model);
            }
            catch
            {
                return View("Error");
            }
        }
        public ActionResult InventoryActions(string button, int ids)
        {
            try
            {
                switch (button)
                {
                    case "Добавить инвентарь":
                        return CreateInventory();
                    case "Редактировать инвентарь":
                        return EditInventory(ids);
                    case "Удалить инвентарь":
                        return DeleteInventory(ids);
                    default:
                        return null;
                }
            }
            catch
            {
                return View("Error");
            }
        }
        [HttpGet]
        public ActionResult CreateInventory()
        {
            return View("../Inventory/Create");
        }
        [HttpPost]
        public ActionResult CreateInventory(string name, decimal price, DateTime dateOfpurchase, int eventId)
        {
            try
            {
                var inventory = new Inventory
                {
                    Name = name,
                    Price = price,
                    DateOfPurchase = dateOfpurchase,
                    EventId = eventId
                };
                db.Inventories.Add(inventory);
                db.SaveChanges();
                IEnumerable<Inventory> model = db.Inventories;
                foreach (var item in model)
                {
                    var @event = db.Events.SingleOrDefault(e => e.Id == item.EventId);
                    item.Event = @event;
                }
                return View("../Inventory/Index", model);
            }
            catch
            {
                return View("Error");
            }
        }
        [HttpGet]
        public ActionResult EditInventory(int id)
        {
            try
            {
                var model = db.Inventories.SingleOrDefault(i => i.Id == id);
                return View("../Inventory/Edit", model);
            }
            catch
            {
                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult EditInventory(int id, string name, decimal price, DateTime dateOfpurchase, int eventId)
        {
            try
            {
                var inventory = db.Inventories.SingleOrDefault(i => i.Id == id);
                inventory.Name = name;
                inventory.Price = price;
                inventory.DateOfPurchase = dateOfpurchase;
                inventory.EventId = eventId;
                db.Inventories.Update(inventory);
                db.SaveChanges();
                IEnumerable<Inventory> model = db.Inventories;
                foreach (var item in model)
                {
                    var @event = db.Events.SingleOrDefault(e => e.Id == item.EventId);
                    item.Event = @event;
                }
                return View("../Inventory/Index", model);
            }
            catch
            {
                return View("Error");
            }
        }
        public ActionResult DeleteInventory(int id)
        {
            try
            {
                var inventory = db.Inventories.SingleOrDefault(i => i.Id == id);
                db.Inventories.Remove(inventory);
                db.SaveChanges();
                IEnumerable<Inventory> model = db.Inventories;
                foreach (var item in model)
                {
                    var @event = db.Events.SingleOrDefault(e => e.Id == item.EventId);
                    item.Event = @event;
                }
                return View("../Inventory/Index", model);
            }
            catch
            {
                return View("Error");
            }
        }
        public ActionResult InventoryByEventId(int eventId)
        {
            try
            {
                IEnumerable<Inventory> model = db.Inventories.Where(i => i.EventId == eventId);
                foreach (var item in model)
                {
                    var @event = db.Events.SingleOrDefault(e => e.Id == item.EventId);
                    item.Event = @event;
                }
                return View("../Inventory/Index", model);
            }
            catch
            {
                return View("Error");
            }
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