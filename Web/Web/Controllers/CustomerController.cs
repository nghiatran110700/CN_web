using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models.Entity;
using Web.Models.Dao;
using System.Net;
using Web.Models.DTO;
using PagedList;

namespace Web.Controllers
{
    public class CustomerController : Controller
    {
        ShopModel db = new ShopModel();
        // GET: Customer
        public ActionResult Index(string keyword , int pageNum = 1,int pageSize = 5)
        {
            if (keyword != null)
            {
                ProductDAO dao = new ProductDAO();
                return View(dao.lstSearchByName(keyword,pageNum,pageSize));
            }
            var lst = from p in db.Products
                      join c in db.categories on p.idCategory equals c.idCategory
                      select new ProductDTO()
                      {
                          idProduct = p.idProduct,
                          nameProduct = p.nameProduct,
                          price = p.price,
                          amuont = p.amuont,
                          photo = p.photo,
                          description = p.description,
                          idCategory = c.idCategory,
                          nameCategory = c.nameCategory
                      };
            return View(lst.ToList().ToPagedList(pageNum,pageSize));
            //var pro = from p in db.Products select p;
            //return View(pro.ToList());
        }
        public ActionResult Details(int id)
        {
            ProductDAO dao = new ProductDAO();
            Product product = new Product();
            product = dao.FindById(id);
            return View(product);
        }
        public PartialViewResult catePartail()
        {
            var lst = db.categories.ToList();
            return PartialView(lst);
        }
        public ActionResult SearchBycate(int id)
        {
                var proWithCate = from p in db.Products where p.idCategory == id select p;
                return View(proWithCate.ToList());
           
        }
       
    }
}