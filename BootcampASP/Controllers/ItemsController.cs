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
        ItemVM itemVM = new ItemVM();
        public ActionResult Index()
        {
            var Get = _context.Items.Where(x => x.IsDelete == false).ToList();
            return View(Get);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var GetCombo = _context.Suppliers.Where(x => x.IsDelete == false);
            var GetSupplier = GetCombo.Select(item => new SelectListItem
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
                item.Name = ItemVM.Name;
                item.Stock = ItemVM.Stock;
                item.Price = ItemVM.Price;
                item.CreateDate = DateTimeOffset.Now.LocalDateTime;
                item.Suppliers = _context.Suppliers.Find(ItemVM.Suppliers_id);
                item.IsDelete = false;
                _context.Items.Add(item);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ItemVM);
        }

        [HttpGet]
        public ActionResult Edit(int? Id)
        {            
            if (Id == null)
            {
                throw new NullReferenceException();
            }
            var Get = _context.Items.SingleOrDefault(x => x.Id == Id);
            if (Get == null)
            {
                throw new NullReferenceException();
            }


            var GetCombo = _context.Suppliers.Where(x => x.IsDelete == false);
            var GetSupplier = GetCombo.Select(item => new SelectListItem()
            {
                Selected = (item.Id == Id),
                Text = item.Name,
                Value = item.Id.ToString()
            }).ToList();
            ViewBag.Suppliers = GetSupplier;

            foreach (var get in GetSupplier)
            {
                if (get.Value.Equals(Get.Suppliers.Id.ToString()))
                {
                    get.Selected = true;
                    break;
                }
            }
            itemVM.Name = Get.Name;
            itemVM.Stock = Get.Stock;
            itemVM.Price = Get.Price;
            itemVM.Suppliers_id = Get.Suppliers.Id;
            return View(itemVM);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ItemVM ItemVM)
        {
            if (ModelState.IsValid)
            {
                var Get = _context.Items.Find(ItemVM.Id);
                Get.Name = ItemVM.Name;
                Get.Stock = ItemVM.Stock;
                Get.Price = ItemVM.Price;
                Get.CreateDate = DateTimeOffset.Now.LocalDateTime;
                Get.Suppliers = _context.Suppliers.Find(ItemVM.Suppliers_id);
                Get.UpdateDate = DateTimeOffset.Now.LocalDateTime;
                _context.Entry(Get).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ItemVM);
        }

        //[HttpGet]
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        throw new NullReferenceException();
        //    }
        //    var Get = _context.Items.SingleOrDefault(x => x.Id == id);
        //    if (Get == null)
        //    {
        //        throw new NullReferenceException();
        //    }
        //    return View(Get);
        //}

        //[HttpPost, ActionName("Delete")]
        [HttpPost]
        public ActionResult Delete(int? Id)
        {
            if (ModelState.IsValid)
            {
                var Get = _context.Items.SingleOrDefault(x => x.Id == Id);
                Get.DeleteDate = DateTimeOffset.Now.LocalDateTime;
                Get.IsDelete = true;
                _context.Entry(Get).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}