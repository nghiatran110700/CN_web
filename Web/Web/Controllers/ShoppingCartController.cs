using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Web.Models.Dao;
using Web.Models.Entity;

namespace Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        ShopModel db = new ShopModel();
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if(cart==null|| Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        } 
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddToCart(int id)
        {
            var pro = db.Products.Single(s => s.idProduct == id);
            if (pro != null)
            {
                GetCart().add(pro);
            }
            return RedirectToAction("ShowCart", "ShoppingCart");
        }

        public ActionResult ShowCart()
        {
            if (Session["Cart"] == null)
            {
                return RedirectToAction("ShowCart", "ShoppingCart");
            }
                Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }
        public ActionResult Update_soluong(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int id_pro = Int32.Parse(form["idProduct"]);
            int soluong = Int32.Parse(form["soluong"]);
            cart.updateSoLuong(id_pro, soluong);
            return RedirectToAction("ShowCart", "ShoppingCart");
        }
       public ActionResult RemoveCart(int id)
       {
            Cart cart = Session["Cart"] as Cart;
            cart.removeCart(id);
            return RedirectToAction("ShowCart", "ShoppingCart");
       }

        public ActionResult Success()
        {
            return View();
        }
       public ActionResult Checkout(FormCollection form)
       {
            try
            {
                Cart cart = Session["Cart"] as Cart;
                bill _bill = new bill();
                var t = cart.tota_bill();
                _bill.phone = form["Phone"];
                _bill.address = form["Address"];
                _bill.status = "Đơn mới";
                _bill.total_bill = Int32.Parse(Convert.ToString(t));
                db.bills.Add(_bill);
                foreach(var item in cart.ITEMS)
                {
                         billDetail _billDetail = new billDetail();
                        _billDetail.idBill = _bill.idBill;
                        _billDetail.idProduct = item.product.idProduct;
                        _billDetail.amount = item.soluong;
                        _billDetail.total = item.product.price * item.soluong;
                        db.billDetails.Add(_billDetail);
                }
                db.SaveChanges();
                cart.clear();
            return RedirectToAction("Success", "ShoppingCart");

            }
            catch
            {
                return Content("Lỗi khi mua hàng mời bạn kiểm tra lại thông tin ");
            }
        }
    }
}