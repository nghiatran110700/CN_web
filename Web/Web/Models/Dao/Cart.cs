using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models.Entity;

namespace Web.Models.Dao
{
    public class ItemCart
    {
        public Product product { get; set; }
        public int soluong { get; set; }
    }
    public class Cart
    {
        List<ItemCart> item = new List<ItemCart>();

        public IEnumerable<ItemCart> ITEMS
        {
            get { return item; }
        }

        public void add(Product pro , int sl = 1)
        {
            var p = item.SingleOrDefault(s => s.product.idProduct == pro.idProduct);
            if(p == null)
            {
                item.Add(new ItemCart
                {
                    product = pro,soluong = sl
                }) ;
            }
            else
            {
                p.soluong += sl;
            }
        }
        public void updateSoLuong(int id, int soluong)
        {
            var sp = item.Find(s => s.product.idProduct == id);
            if (item != null)
            {
                sp.soluong = soluong;
            }
        }
        public void removeCart(int id)
        {
            var p = item.SingleOrDefault(s => s.product.idProduct == id);
            item.Remove(p);
        }
        public void clear()
        {
            item.Clear();
        }
        public int Total_amount_inCart()
        {
            return item.Sum(s => s.soluong);
        }
        public double tota_bill()
        {
            var total = item.Sum(s => s.product.price * s.soluong);
            return total;
        }
    }
}