using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web.Models.Entity;
using Web.Models.Dao;
using Web.Models.DTO;
using PagedList;

namespace Web.Controllers
{
    //[Authorize]
    public class AdminController : Controller
    {
        /*private*/ ShopModel db = new ShopModel();

        // GET: Admin
        //[AllowAnonymous]
        public ActionResult Index(string keyword, string minPrice, string maxPrice, int pageNum = 1, int pageSize = 5)
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
            return View(lst.ToList().ToPagedList(pageNum, pageSize));
        }
       
        // GET: Admin/Details/5
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
        public ActionResult Create()
        {
            ViewBag.idCategory = new SelectList(db.categories, "idCategory", "nameCategory");
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idProduct,nameProduct,price,amuont,photo,description,idCategory")] Product product, HttpPostedFileBase uploadhinh)
        {
            if (ModelState.IsValid)
            {
                if (uploadhinh != null && uploadhinh.ContentLength > 0)         
                {
                   
                    var path = Path.Combine(Server.MapPath("~/Access/Imge/"), System.IO.Path.GetFileName(uploadhinh.FileName));
                    uploadhinh.SaveAs(path);
                    product.photo = uploadhinh.FileName;
                    db.SaveChanges();
                }

                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCategory = new SelectList(db.categories, "idCategory", "nameCategory", product.idCategory);
            return RedirectToAction("Index");
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.idCategory = new SelectList(db.categories, "idCategory", "nameCategory", product.idCategory);
            return View(product);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idProduct,nameProduct,price,amuont,photo,description,idCategory")] Product product, HttpPostedFileBase uploadhinh)
        {
            if (ModelState.IsValid)
            {
                if (uploadhinh != null && uploadhinh.ContentLength > 0)   
                {
                    var path = Path.Combine(Server.MapPath("~/Access/Imge/"), System.IO.Path.GetFileName(uploadhinh.FileName));
                    uploadhinh.SaveAs(path);
                    product.photo = uploadhinh.FileName;
                    db.SaveChanges();
                }
                
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");  
            }
            ViewBag.idCategory = new SelectList(db.categories, "idCategory", "nameCategory", product.idCategory);
            return View(product);
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
