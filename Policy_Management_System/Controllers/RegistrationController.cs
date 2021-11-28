using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Policy_Management_System.Models;

namespace Policy_Management_System.Controllers
{
    public class RegistrationController : Controller
    {
        //
        // GET: /Registration/
        public ActionResult Index()
        {
            PolicyContext context = new PolicyContext();
            ViewBag.Role=new SelectList(context.Role,"Id","Name").Where(i=>i.Text!="Admin");
            return View("Registration");
        }

        //Action for the registration of user.
        [HttpPost]
        public ActionResult Index(Registration reg)
        {
            PolicyContext context = new PolicyContext();
            Registration r = context.Registration.Where(i=>i.Mobile==reg.Mobile).FirstOrDefault();
            if (r==null)
            {
                int RoleId = Convert.ToInt32(Request.Form["Role"].ToString());
                Role role = context.Role.Where(i => i.Id == RoleId).FirstOrDefault();
                reg.Role = role;

                Login log = new Login();
                log.Mobile = reg.Mobile;
                log.Password = reg.Password;
                log.UserId = reg;
                context.Registration.Add(reg);
                context.Login.Add(log);
                context.SaveChanges();
                ViewBag.Role = new SelectList(context.Role, "Id", "Name").Where(i => i.Text != "Admin");
                Response.Write("<script>alert('Registered Successfully')</script>");
                ModelState.Clear();
                return View("Registration");
            }
            else
            {
                Response.Write("<script>alert('The mobile number already exists.')</script>");
                ViewBag.Role = new SelectList(context.Role, "Id", "Name").Where(i => i.Text != "Admin");
                return View("Registration");
            }
            
        }
	}
}