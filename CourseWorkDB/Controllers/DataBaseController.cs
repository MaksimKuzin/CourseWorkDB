﻿using CourseWorkDB.DataBase;
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
            var priest = db.Priests.SingleOrDefault(p => p.Id == ids);
            if (priest != null)
                return View("../Priest/Edit", priest);
            else
            {
                IEnumerable<Priest> model = db.Priests.AsEnumerable();
                return View("../Priest/Index", model);
            }
        }
        [HttpPost]
        public ActionResult EditPriest(int id, string title, string name, DateTime initialDate)
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
        public ActionResult DeletePriest(int id)
        {
            var priest = db.Priests.SingleOrDefault(p => p.Id == id);
            db.Remove(priest);
            db.SaveChanges();
            IEnumerable<Priest> model = db.Priests.AsEnumerable();
            return View("../Priest/Index", model);
        }
        public ActionResult Parishioner()
        {
            IEnumerable<Parishioner> model = db.Parishioners;
            foreach (var parishioner in model)
            {
                var priest = db.Priests.SingleOrDefault(p => p.Id == parishioner.PriestId);
                parishioner.Priest = priest;
            }
            return View("../Parishioner/Index", model);
        }
        public ActionResult ParishionerByPriestId(int priestId)
        {
            IEnumerable<Parishioner> model = db.Parishioners.Where(p => p.PriestId == priestId);
            foreach (var parishioner in model)
            {
                var priest = db.Priests.SingleOrDefault(p => p.Id == parishioner.PriestId);
                parishioner.Priest = priest;
            }
            return View("../Parishioner/Index", model);
        }
        public ActionResult ParishionerActions(string button, int ids)
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
        [HttpGet]
        public ActionResult CreateParishioner()
        {
            return View("../Parishioner/Create");
        }
        [HttpPost]
        public ActionResult CreateParishioner(string name, string surname, string patronymic, short age, bool sex, string address, string phoneNumber, int priestId)
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
        [HttpGet]
        public ActionResult EditParishioner(int id)
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
        [HttpPost]
        public ActionResult EditParishioner(int id, string name, string surname, string patronymic, short age, bool sex, string address, string phoneNumber, int priestId)
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
        public ActionResult DeleteParishioner(int id)
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
        public ActionResult Event()
        {
            var model = db.Events;
            return View("../Event/Index", model);
        }
        public ActionResult EventActions(string button, int ids)
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
        [HttpGet]
        public ActionResult CreateDivineService()
        {
            return View("../DivineService/Create");
        }
        [HttpPost]
        public ActionResult CreateDivineService(string name, DateTime date, int priestId, string justification, string prayer)
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
        public ActionResult AboutDivineService(int id)
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
        [HttpGet]
        public ActionResult CreateSacredEvent()
        {
            return View("../SacredEvent/Create");
        }
        [HttpPost]
        public ActionResult CreateSacredEvent(string name, DateTime date, int priestId, string place, DateTime finishDate, string transport, string route, string sourceOfFunding)
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
        public ActionResult AboutSacredEvent(int id)
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
        [HttpGet]
        public ActionResult CreateActivity()
        {
            return View("../Activity/Create");
        }
        [HttpPost]
        public ActionResult CreateActivity(string name, DateTime date, int priestId, short duration, short parishionerAmount)
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
        public ActionResult AboutActivity(int id)
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
        [HttpGet]
        public ActionResult EditEvent(int id)
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
        [HttpPost]
        public ActionResult EditEvent(int id, string name, DateTime date, int priestId, string justification, string prayer, string place, DateTime finishDate, string transport, string route, string sourceOfFunding, short duration, short amount)
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
        public ActionResult DeleteEvent(int id)
        {
            var @event = db.Events.SingleOrDefault(e => e.Id == id);
            db.Remove(@event);
            db.SaveChanges();
            var model = db.Events;
            return View("../Event/Index", model);
        }
        [HttpGet]
        public ActionResult AddParishionersToEvent(int id)
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
        [HttpPost]
        public ActionResult AddParishionersToEvent(string ids)
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
        [HttpGet]
        public ActionResult DeleteParishionerFromEvent(int id)
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
        public ActionResult DeleteParishionerFromEvent(string ids)
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
        public ActionResult Donation()
        {
            IEnumerable<Donation> model = db.Donations;
            foreach (var item in model)
            {
                var parishioner = db.Parishioners.SingleOrDefault(p => p.Id == item.ParishionerId);
                item.Parishioner = parishioner;
            }
            return View("../Donation/Index", model);
        }
        public ActionResult DonationActions(string button, int ids)
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
        [HttpGet]
        public ActionResult CreateDonation()
        {
            return View("../Donation/Create");
        }
        [HttpPost]
        public ActionResult CreateDonation(string purpose, decimal sum, int parishionerId)
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
        [HttpGet]
        public ActionResult EditDonation(int id)
        {
            var model = db.Donations.SingleOrDefault(d => d.Id == id);
            return View("../Donation/Edit", model);
        }
        [HttpPost]
        public ActionResult EditDonation(int id, string purpose, decimal sum, int parishionerId)
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
        public ActionResult DeleteDonation(int id)
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
        public ActionResult DonationByParishionerId(int parishionerId)
        {
            IEnumerable<Donation> model = db.Donations.Where(d => d.ParishionerId == parishionerId);
            foreach (var item in model)
            {
                var parishioner = db.Parishioners.SingleOrDefault(p => p.Id == item.ParishionerId);
                item.Parishioner = parishioner;
            }
            return View("../Donation/Index", model);
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