using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models.Entity;

namespace Web.Models.Dao
{
    public class BillDAO
    {
        ShopModel db = new ShopModel();
        public int? revenue()
        {
            var lst = from b in db.bills select b;
            return lst.Sum(s => s.total_bill);
        }
        public void update(int id)
        {
            var bill = db.bills.Find(id);
                bill.status = "Đơn Hàng Đã Được Chuyển Đi";
                db.SaveChanges();
        }
    }
}