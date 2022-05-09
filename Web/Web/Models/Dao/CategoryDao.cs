using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models.Entity;

namespace Web.Models.Dao
{
    public class CategoryDao
    {
        ShopModel db;
        public CategoryDao()
        {
            db = new ShopModel();
        }
        public List<category> LstCate()
        {
            return db.categories.ToList();
        }
        public category GetById(int id)
        {
            return db.categories.Single(s=>s.idCategory.Equals(id));
        }
    }
}