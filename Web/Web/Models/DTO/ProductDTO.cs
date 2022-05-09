using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models.Dao;

namespace Web.Models.DTO
{
    public class ProductDTO
    {
        public int idProduct { get; set; }
        public string nameProduct { get; set; }
        public int price { get; set; }
        public int amuont { get; set; }
        public string photo { get; set; }
        public string description { get; set; }
        public int idCategory { get; set; }
        public string nameCategory { get; set; }
    }
}