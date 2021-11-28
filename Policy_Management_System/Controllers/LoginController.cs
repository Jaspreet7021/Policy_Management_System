using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Policy_Management_System.Models;

namespace Policy_Management_System.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        public ActionResult Index()
        {
            return View("Login");
        }

        //Action for logging in the account.
        [HttpPost]
        public ActionResult Index(Login log)
        {
            PolicyContext context = new PolicyContext();
            Login val = context.Login.Include(i=>i.UserId).Where(i=>i.Mobile==log.Mobile).SingleOrDefault();
            
            if (val!=null)
            {
                Registration reg = context.Registration.Include(i => i.Role).Where(i => i.Id == val.UserId.Id).SingleOrDefault();
                if (val.Password==log.Password)
                {
                    Session["Name"] = val.UserId.Name;
                    Session["UserId"] = val.UserId.Id;
                    Session["Role"] = reg.Role.Id;
                    Session["Mobile"] = reg.Mobile;
                    //Response.Write("<script>alert('Welcome to the dashboard.')</script>");
                    if (Session["Role"].ToString()=="2")
                    {
                        return RedirectToAction("Index", "Proposer");
                    }
                    else if (Session["Role"].ToString() == "3")
                    {
                        return RedirectToAction("Index","Customer");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                }
                else
                {
                    Response.Write("<script>alert('The Password is not correct.')</script>");
                    return View("Login");
                }
            }
            else
            {
                Response.Write("<script>alert('The User Id does not exist.')</script>");
                return View("Login");

            }
            
        }
	}
}