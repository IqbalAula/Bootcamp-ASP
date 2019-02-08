using BootcampASP.Context;
using BootcampASP.Models;
using BootcampASP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootcampASP.Controllers
{
    public class ItemsController : Controller
    {
        // GET: Items
        MyContext _context = new MyContext();
        public ActionResult Index()
        {
            var Get = _context.Items.Where(x => x.IsDelete == false).ToList();
            return View(Get);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var GetSupplier = _context.Suppliers.Select(item => new SelectListItem
            {
                Text = item.Name,
                Value = item.Id.ToString()
            }).ToList();

            ViewBag.suppliers = GetSupplier;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ItemVM ItemVM)
        {
            if (ModelState.IsValid)
            {
                Item item = new Item();
                item.CreateDate = DateTimeOffset.Now.LocalDateTime;
                item.IsDelete = false;
                _context.Items.Add(item);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ItemVM);
        }


    }
}