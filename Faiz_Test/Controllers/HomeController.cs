using Faiz_Test.DB_Folder;
using Faiz_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Faiz_Test.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(user_login obj)
        {
            stuInfoEntities dobj = new stuInfoEntities();
            var UserRes = dobj.UserTables.Where(m => m.email == obj.email).FirstOrDefault();
            if (UserRes == null)
            {
                TempData["Invalid"] = "Email not found";
            }
            else
            {
                if (UserRes.email == obj.email && UserRes.password == obj.password)
                {
                    FormsAuthentication.SetAuthCookie(UserRes.email, false);
                    Session["UserName"] = UserRes.name;
                    return RedirectToAction("Dashboard", "Home");
                }
                else
                {
                    TempData["Wrong"] = "Wrong Password";
                    return View();
                }
            }
           
            return View();
        }
        [Authorize]

        public ActionResult Index()
        {
            return View();
        }


        [Authorize]


        public ActionResult DashBoard()
        {
            return View();
        }

        [Authorize]


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        //[Authorize]

        public ActionResult Tables()
        {
            stuInfoEntities obj = new stuInfoEntities();
            List<StuModel> hobj = new List<StuModel>();
            var res = obj.tb_stu1.ToList();
            foreach (var item in res)
            {
                hobj.Add(new StuModel
                {
                    Id = item.id,
                    name = item.name,
                    age = (int)item.age,
                    Company = item.company,
                    Email = item.email
                });
            }
            return View(hobj);
        }

        [Authorize]

        [HttpGet]
        public ActionResult Forms()
        {
            return View();
        }
        [Authorize]

        [HttpPost]
        public ActionResult Forms(StuModel hobj)
        {
            stuInfoEntities sobj = new stuInfoEntities();

            tb_stu1 tobj = new tb_stu1();

            tobj.id = hobj.Id;
            tobj.name = hobj.name;
            tobj.age = hobj.age;
            tobj.company = hobj.Company;
            tobj.email = hobj.Email;
            if (hobj.Id == 0)
            {
                sobj.tb_stu1.Add(tobj);
                sobj.SaveChanges();
            }
            else
            {
                sobj.Entry(tobj).State = System.Data.Entity.EntityState.Modified;
                sobj.SaveChanges();
            
            
            }



            return RedirectToAction("Tables");
        }
       
        
        [Authorize]
         public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]


        public ActionResult delete(int id)
            {
            stuInfoEntities sobj = new stuInfoEntities();
            var d_item = sobj.tb_stu1.Where(m => m.id == id).First();
            sobj.tb_stu1.Remove(d_item);
            sobj.SaveChanges();
            return RedirectToAction("Tables");
            }
       
        [Authorize]

        public ActionResult edit(int id)
        {
            StuModel hobj = new StuModel();
            stuInfoEntities sobj = new stuInfoEntities();
            var eobj = sobj.tb_stu1.Where(m => m.id == id).First();
            hobj.Id = eobj.id;
            hobj.name = eobj.name;
            hobj.age = (int)eobj.age;
            hobj.Company = eobj.company;
            hobj.Email = eobj.email;

            return View("Forms",hobj);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LogIn");

          
        }
    }
}


        