using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models.Entity;
using Web.Models.DTO;
using PagedList;
using System.IO;

namespace Web.Models.Dao
{
    public class ProductDAO
    {
        ShopModel db;
        public ProductDAO()
        {
            db = new ShopModel();
        }
        public IQueryable<Product> Products
        {
            get { return db.Products; }
        }
        public IQueryable<Product> ListProduct()
        {
            var res = from p in db.Products select p;
            return res;
        }

        //public int create(string namePro, int Price, int Amount, string Photo, string Description, int IdCate)
        //{
        //    Product pro = new Product();
        //    pro.nameProduct = namePro;
        //    pro.price = Price;
        //    pro.amuont = Amount;
        //    pro.photo = Photo;
        //    pro.description = Description;
        //    pro.idCategory = IdCate;
        //    db.Products.Add(pro);
        //    db.SaveChanges();
        //    return pro.idProduct;
        //}
        public void create(Product pro)
        {
            db.Products.Add(pro);
            db.SaveChanges();
        }
        public void delete(int id)
        {
            Product pro = db.Products.Find(id);
            if (pro != null)
            {
                db.Products.Remove(pro);
                db.SaveChanges();
            }
        }
        public void Update(Product pmt)
        {
            Product pro = db.Products.Find(pmt.idProduct);
            if (pro != null)
            {
                pro.idProduct = pmt.idProduct;
                pro.nameProduct = pmt.nameProduct;
                pro.price = pmt.price;
                pro.amuont = pmt.amuont;
                pro.photo = pmt.photo;
                pro.description = pmt.description;
                pro.idCategory = pmt.idCategory;
                db.SaveChanges();
            }
        }
        public Product FindById(int id)
        {
            return db.Products.Find(id);
        }
        public IEnumerable<ProductDTO> join(int pageNum, int pageSize)
        {
          var lst = db.Database.SqlQuery<ProductDTO>("select " +
               " p.idProduct, " +
               " p.nameProduct, " +
               " p.price, " + 
               " p.amuont, " + 
               " p.photo, " + 
               " p.description, " + 
               " c.idCategory, " + 
               " c.nameCategory " +
               " from Product as p left join category as c on p.idCategory = c.idCategory ").ToPagedList<ProductDTO>(pageNum, pageSize); 
            return lst;
        }
        public IEnumerable<ProductDTO> lstSearch(string keyword, string minPrice,string maxPrice ,int pageNum,int pageSize)
        {
            var lst = db.Database.SqlQuery<ProductDTO>("select" +
               " p.idProduct, " +
               " p.nameProduct, " +
               " p.price, " +
               " p.amuont, " +
               " p.photo, " +
               " p.description, " +
               " c.idCategory, " +
               " c.nameCategory " +
               " from Product as p left join category as c on p.idCategory = c.idCategory " +
                "where  p.price >" + Convert.ToInt32(minPrice) + "and p.price <"+ Convert.ToInt32(maxPrice) + "  and p.nameProduct LIKE '%" + keyword + "%'"
                ).ToPagedList<ProductDTO>(pageNum, pageSize);
            return lst;
        }
        public IEnumerable<ProductDTO> lstSearchByName(string keyword, int pageNum, int pageSize)
        {
            var lst = db.Database.SqlQuery<ProductDTO>("select" +
               " p.idProduct, " +
               " p.nameProduct, " +
               " p.price, " +
               " p.amuont, " +
               " p.photo, " +
               " p.description, " +
               " c.idCategory, " +
               " c.nameCategory " +
               " from Product as p left join category as c on p.idCategory = c.idCategory " +
                "where p.nameProduct LIKE '%" + keyword + "%'"
                ).ToPagedList<ProductDTO>(pageNum, pageSize);
            return lst;
        }
        public IEnumerable<ProductDTO> lstSearchByNameAndMinPrice(string keyword, string minPrice, int pageNum, int pageSize)
        {
            var lst = db.Database.SqlQuery<ProductDTO>("select" +
               " p.idProduct, " +
               " p.nameProduct, " +
               " p.price, " +
               " p.amuont, " +
               " p.photo, " +
               " p.description, " +
               " c.idCategory, " +
               " c.nameCategory " +
               " from Product as p left join category as c on p.idCategory = c.idCategory " +
                "where  p.price >" + Convert.ToInt32(minPrice) + "  and p.nameProduct LIKE '%" + keyword + "%'"
                ).ToPagedList<ProductDTO>(pageNum, pageSize);
            return lst;
        }
        public IEnumerable<ProductDTO> lstSearchByNameAndMaxPrice(string keyword, string maxPrice, int pageNum, int pageSize)
        {
            var lst = db.Database.SqlQuery<ProductDTO>("select" +
               " p.idProduct, " +
               " p.nameProduct, " +
               " p.price, " +
               " p.amuont, " +
               " p.photo, " +
               " p.description, " +
               " c.idCategory, " +
               " c.nameCategory " +
               " from Product as p left join category as c on p.idCategory = c.idCategory " +
                "where   p.price <" + Convert.ToInt32(maxPrice) + "  and p.nameProduct LIKE '%" + keyword + "%'"
                ).ToPagedList<ProductDTO>(pageNum, pageSize);
            return lst;
        }
        public IEnumerable<ProductDTO> lstSearchByMinPrice( string minPrice, int pageNum, int pageSize)
        {
            var lst = db.Database.SqlQuery<ProductDTO>("select" +
               " p.idProduct, " +
               " p.nameProduct, " +
               " p.price, " +
               " p.amuont, " +
               " p.photo, " +
               " p.description, " +
               " c.idCategory, " +
               " c.nameCategory " +
               " from Product as p left join category as c on p.idCategory = c.idCategory " +
                "where  p.price >" + Convert.ToInt32(minPrice) 
                ).ToPagedList<ProductDTO>(pageNum, pageSize);
            return lst;
        }
        public IEnumerable<ProductDTO> lstSearchByMaxPrice( string maxPrice, int pageNum, int pageSize)
        {
            var lst = db.Database.SqlQuery<ProductDTO>("select" +
               " p.idProduct, " +
               " p.nameProduct, " +
               " p.price, " +
               " p.amuont, " +
               " p.photo, " +
               " p.description, " +
               " c.idCategory, " +
               " c.nameCategory " +
               " from Product as p left join category as c on p.idCategory = c.idCategory " +
                "where p.price <" + Convert.ToInt32(maxPrice) 
                ).ToPagedList<ProductDTO>(pageNum, pageSize);
            return lst;
        }
        public IEnumerable<ProductDTO> lstSearchByPrice( string minPrice, string maxPrice, int pageNum, int pageSize)
        {
            var lst = db.Database.SqlQuery<ProductDTO>("select" +
               " p.idProduct, " +
               " p.nameProduct, " +
               " p.price, " +
               " p.amuont, " +
               " p.photo, " +
               " p.description, " +
               " c.idCategory, " +
               " c.nameCategory " +
               " from Product as p left join category as c on p.idCategory = c.idCategory " +
                "where  p.price >" + Convert.ToInt32(minPrice) + "and p.price <" + Convert.ToInt32(maxPrice) 
                ).ToPagedList<ProductDTO>(pageNum, pageSize);
            return lst;
        }
        public IEnumerable<ProductDTO> SearchByName(string keyword)
        {
            var lst = db.Database.SqlQuery<ProductDTO>("select" +
               " p.idProduct, " +
               " p.nameProduct, " +
               " p.price, " +
               " p.amuont, " +
               " p.photo, " +
               " p.description, " +
               " c.idCategory, " +
               " c.nameCategory " +
               " from Product as p left join category as c on p.idCategory = c.idCategory " +
                "where p.nameProduct LIKE '%" + keyword + "%'"
                );
            return lst;
        }
    }
}