using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _91APP_TEST_V2.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace _91APP_TEST_V2.Controllers
{
    public class HomeController : Controller
    {
        MyDataBase db = new MyDataBase();

        public bool CheckUserData(string account, string password)
        {
            try
            {
                string sql = "SELECT 1 FROM user WHERE account = @account AND password = @password;";
                MySqlCommand cmd = db.MySqlCommand(sql);
                cmd.Parameters.Add("@account", MySqlDbType.VarChar).Value = account;
                cmd.Parameters.Add("@password", MySqlDbType.VarChar).Value = password;

                db.Connect();

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return false;
            }
            finally
            {
                db.Disconnect();
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection post)
        {
            string account = post["account"];
            string password = post["password"];

            //驗證密碼
            if (CheckUserData(account, password))
            {
                Session["account"] = account;

                return Redirect("OrderList");
            }
            else
            {
                ViewBag.Msg = "登入失敗...";
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session["account"] = "";
            return Redirect("Login");
        }

        public ActionResult OrderList()
        {
            
            string sql = String.Format(@" select * from order_list where order_list.account = '{0}'", Session["account"]);

            MySqlCommand cmd = db.MySqlCommand(sql);

            List<Item> list = new List<Item>();

            db.Connect();

            using (MySqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    Item item = new Item();
                    item.id = dr["id"].ToString();
                    item.name = dr["item"].ToString();
                    item.price = Convert.ToInt16(dr["price"]);
                    item.cost = Convert.ToInt16(dr["cost"]);
                    item.status = dr["status"].ToString();
                    list.Add(item);
                }
            }

            db.Disconnect();

            ViewBag.List = list;
            return View();
        }

        [HttpPost]
        public ContentResult Shipping(String checklist)
        {
            if (checklist == "")
            {
                return Content("Success");
            }
            string[] select = System.Text.RegularExpressions.Regex.Split(checklist, ",");
            
            string sql = "";
            MySqlCommand cmd = null;
            db.Connect();
            for (int i = 0; i < select.Length; i++)
            {
                sql = String.Format(@" INSERT shippingorder (orderid, status) VALUES({0}, 'New')", select[i]);
                cmd = db.MySqlCommand(sql);
                cmd.ExecuteNonQuery();
            }
            sql = @" UPDATE order_list SET status = 'To be shipped' WHERE id IN (" + String.Join(",", select) + ");";

            cmd = db.MySqlCommand(sql);

            int n = cmd.ExecuteNonQuery();

            db.Disconnect();

            return Content("Success");
        }

        public ActionResult Product(string id)
        {
            if (id == null)
            {
                return Redirect("OrderList");
            }
            
            string sql = @" SELECT * FROM order_list WHERE id = " + id;

            MySqlCommand cmd = db.MySqlCommand(sql);

            Item item = new Item();

            db.Connect();

            using (MySqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    item.id = dr["id"].ToString();
                    item.name = dr["item"].ToString();
                    item.price = Convert.ToInt16(dr["price"]);
                    item.cost = Convert.ToInt16(dr["cost"]);
                    item.status = dr["status"].ToString();
                }
            }

            db.Disconnect();

            ViewBag.Item = item;
            return View();
        }
    }
}