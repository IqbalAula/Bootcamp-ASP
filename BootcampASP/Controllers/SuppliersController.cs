using BootcampASP.Context;
using BootcampASP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootcampASP.Controllers
{
    public class SuppliersController : Controller
    {
        // GET: Suppliers
        MyContext _context = new MyContext();
        public ActionResult Index()
        {
            var Get = _context.Suppliers.Where(x => x.IsDelete == false).ToList();
            return View(Get);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name, JoinDate")]Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                supplier.CreateDate = DateTimeOffset.Now.LocalDateTime;
                supplier.IsDelete = false;
                _context.Suppliers.Add(supplier);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(supplier);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {            
            if (id == null)
            {
                throw new NullReferenceException();
            }
            var Get = _context.Suppliers.SingleOrDefault(x => x.Id == id);
            if (Get == null)
            {
                throw new NullReferenceException();
            }
            return View(Get);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, Name")]Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                var Get = _context.Suppliers.SingleOrDefault(x => x.Id == supplier.Id);
                Get.Name = supplier.Name;
                Get.UpdateDate = DateTimeOffset.Now.LocalDateTime;
                _context.Entry(Get).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                throw new NullReferenceException();
            }
            var Get = _context.Suppliers.SingleOrDefault(x => x.Id == id);
            if (Get == null)
            {
                throw new NullReferenceException();
            }
            return View(Get);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([Bind(Include = "Id, Name")]Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                var Get = _context.Suppliers.SingleOrDefault(x => x.Id == supplier.Id);
                Get.DeleteDate = DateTimeOffset.Now.LocalDateTime;
                Get.IsDelete = true;
                _context.Entry(Get).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}