using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Policy_Management_System.Models;

namespace Policy_Management_System.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        //Get existing Categories
        public ActionResult Index()
        {
            if (Session["Role"]==null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                if (Session["Role"].ToString()=="1")
                {
                    PolicyContext context = new PolicyContext();
                    List<Category> clist = context.Category.ToList();
                    return View("Index",clist);
                }
                else
                {
                    Session["Role"] = null;
                    return RedirectToAction("Index", "Login");
                }
            }
            
        }

        [HttpGet]
        public ActionResult AddCategory()
        {
            if (Session["Role"]==null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                if (Session["Role"].ToString() != "1")
                {
                    return RedirectToAction("Logout", "Proposer");
                }
                else
                {
                    return View();
                }
            }
           
            
        }

        [HttpPost]
        public ActionResult AddCategory(Category cat)
        {
            PolicyContext context = new PolicyContext();
            context.Category.Add(cat);
            context.SaveChanges();
            ModelState.Clear();
            Response.Write("<script>alert('Category has been added succesfully')</script>");
            return View("AddCategory");
        }

        public ActionResult EditCategory(int id)
        {
            PolicyContext context = new PolicyContext();
            Category cat = context.Category.Where(i => i.Id == id).SingleOrDefault();
            return View("EditCategory",cat);
        }

        [HttpPost]
        public ActionResult EditCategory(Category cat)
        {
            PolicyContext context = new PolicyContext();
            Category catUp = context.Category.Where(i => i.Id == cat.Id).SingleOrDefault();
            catUp.Name = cat.Name;
            context.SaveChanges();
            Response.Write("<script>alert('The category has been updated succesfully')</script>");
            return View("EditCategory");
        }

        public ActionResult ViewProposer()
        {
            if (Session["Role"].ToString()!="1")
            {
                return RedirectToAction("Logout", "Proposer");
            }
            else
            {
                PolicyContext context = new PolicyContext();
                List<Registration> regList = context.Registration.Include("Role").Where(i => i.Role.Id == 2).ToList();
                return View("ViewProposer", regList);
            }
            
        }

        public ActionResult ViewCustomer()
        {
            if (Session["Role"].ToString() != "1")
            {
                return RedirectToAction("Logout", "Proposer");
            }
            else
            {
                PolicyContext context = new PolicyContext();
                List<Registration> regList = context.Registration.Include("Role").Where(i => i.Role.Id == 3).ToList();
                return View("ViewProposer", regList);
            }
            
        }


	}
}