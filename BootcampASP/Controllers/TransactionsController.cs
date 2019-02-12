using BootcampASP.Context;
using BootcampASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootcampASP.Controllers
{
    public class TransactionsController : Controller
    {
        MyContext _context = new MyContext();
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult getItemSuppliers()
        {
            List<Supplier> suppliers = new List<Supplier>();
            suppliers = _context.Suppliers.OrderBy(a => a.Name).ToList();
            return new JsonResult { Data = suppliers, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult getItems(int Id)
        {
            List<Item> items = new List<Item>();
            items = _context.Items.Where(a => a.Suppliers.Id == Id).OrderBy(a => a.Name).ToList();
            return new JsonResult { Data = items, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult save(Transaction transaction)
        {
            bool status = false;
            DateTime dateOrg;
            var isValidDate = DateTime.TryParseExact(transaction.TransactionDate.ToString(), "mm-dd-yyyy", null, System.Globalization.DateTimeStyles.None, out dateOrg);
            if (isValidDate)
            {
                transaction.TransactionDate = dateOrg;
            }

            var isValidModel = TryUpdateModel(transaction);
            if (isValidModel)
            {
                _context.Transactions.Add(transaction);
                _context.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }

    }
}