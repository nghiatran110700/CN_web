using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models.Entity;
using Web.Models.Dao;

namespace Web.Controllers
{
    public class BiLLController : Controller
    {
        // GET: BiLL
        ShopModel db = new ShopModel();
        public ActionResult Index()
        {
            var lst = from b in db.bills select b;
            Session["Doanh Thu"] = lst.Sum(s => s.total_bill);
            return View(lst.ToList());
        }
        public ActionResult Details(int id)
        {
            var lst = from b in db.billDetails where b.idBill == id select b;
            var total = db.bills.Where(s => s.idBill == id).SingleOrDefault();
            Session["Tổng Bill"] = total.total_bill;
            Session["Mã Đơn Hàng"] = total.idBill;
            //var lst = from a in db.bills join b in db.billDetails on a.idBill equals b.idBill select b;
            return View(lst.ToList());
        }
        public ActionResult agree(int id)
        {
            BillDAO b = new BillDAO();
            b.update(id);
            return RedirectToAction("Index", "BiLL");
        }
    }
}