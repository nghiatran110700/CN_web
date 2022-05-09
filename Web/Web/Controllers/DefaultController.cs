using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models.Entity;
using Web.Models.Dao;
using Web.Models.DTO;
using PagedList;
using System.Data.Entity;
using System.Net;
using System.IO;

namespace Web.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        ShopModel db = new ShopModel();
        public ActionResult Index(string keyword, string minPrice, string maxPrice, int pageNum = 1, int pageSize = 2)
        {
            ProductDAO dao = new ProductDAO();
            if (!string.IsNullOrEmpty(keyword) && !string.IsNullOrEmpty(minPrice) && !string.IsNullOrEmpty(maxPrice))
            {
                return View(dao.lstSearch(keyword, minPrice, maxPrice, pageNum, pageSize));
            }

            else if (!string.IsNullOrEmpty(keyword) && string.IsNullOrEmpty(minPrice) && string.IsNullOrEmpty(maxPrice))
            {
                return View(dao.lstSearchByName(keyword, pageNum, pageSize));
            }

            else if (string.IsNullOrEmpty(keyword) && !string.IsNullOrEmpty(minPrice) && string.IsNullOrEmpty(maxPrice))
            {
                return View(dao.lstSearchByMinPrice(minPrice, pageNum, pageSize));
            }

            else if (string.IsNullOrEmpty(keyword) && string.IsNullOrEmpty(minPrice) && !string.IsNullOrEmpty(maxPrice))
            {
                return View(dao.lstSearchByMaxPrice(maxPrice, pageNum, pageSize));
            }

            else if (string.IsNullOrEmpty(keyword) && !string.IsNullOrEmpty(minPrice) && !string.IsNullOrEmpty(maxPrice))
            {
                return View(dao.lstSearchByPrice(minPrice, maxPrice, pageNum, pageSize));
            }

            else if (!string.IsNullOrEmpty(keyword) && !string.IsNullOrEmpty(minPrice) && !string.IsNullOrEmpty(maxPrice))
            {
                return View(dao.lstSearchByNameAndMaxPrice(keyword, maxPrice, pageNum, pageSize));
            }

            else if (!string.IsNullOrEmpty(keyword) && !string.IsNullOrEmpty(minPrice) && string.IsNullOrEmpty(maxPrice))
            {
                return View(dao.lstSearchByNameAndMinPrice(keyword, minPrice, pageNum, pageSize));
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
            //ProductDAO dao = new ProductDAO();
            //return View(dao.join(pageNum,pageSize));
            return View(lst.ToList().ToPagedList(pageNum, pageSize));
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Create
      
    }
}