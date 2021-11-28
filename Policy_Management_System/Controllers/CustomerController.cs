using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Policy_Management_System.Models;

namespace Policy_Management_System.Controllers
{
    public class CustomerController : Controller
    {
        //
        // GET: /Customer/

        //Index Page for Viewing Policy.
        public ActionResult Index()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                if (Session["Role"].ToString() == "3")
                {
                    PolicyContext context = new PolicyContext();
                    ViewBag.Categories = new SelectList(context.Category, "Id", "Name");
                    List<PolicyDetail> pd = context.PolicyDetail.Include(i => i.CatId).ToList();
                    return View(pd);
                }
                else
                {
                    Session["Role"] = null;
                    return RedirectToAction("Index", "Login");
                }

            }

        }

        //Action For Searching Policy.
        [HttpPost]
        public ActionResult SearchPolicy()
        {

            PolicyContext context = new PolicyContext();
            int id = int.Parse(Request.Form["Categories"].ToString());
            List<PolicyDetail> pd = new List<PolicyDetail>();
            pd = context.PolicyDetail.Include(i => i.CatId).Where(i => i.CatId.Id == id).ToList();
            ViewBag.Categories = new SelectList(context.Category, "Id", "Name", id);
            return View("Index", pd);


        }

        //Action For Updating the user profile
        public ActionResult UpdateUserProfile()
        {
            if (Session["Role"]==null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                if (Session["Role"].ToString() != "3")
                {
                    return RedirectToAction("Logout", "Proposer");
                }
                else
                {
                    PolicyContext context = new PolicyContext();
                    int userid = int.Parse(Session["UserId"].ToString());
                    Registration reg = context.Registration.Include(i => i.Role).Where(i => i.Id == userid).SingleOrDefault();
                    return View(reg);
                }
            }
            
        }

        [HttpPost]
        public ActionResult UpdateUserProfile(Registration reg)
        {
            PolicyContext context = new PolicyContext();
            if (reg.Mobile == Session["Mobile"].ToString())
            {
                int userid = int.Parse(Session["UserId"].ToString());
                Registration regis = context.Registration.Include(i => i.Role).Where(i => i.Id == userid).SingleOrDefault();
                regis.Mobile = reg.Mobile;
                regis.Name = reg.Name;
                regis.State = reg.State;
                regis.City = reg.City;
                regis.DateOfBirth = reg.DateOfBirth;
                regis.Gender = reg.Gender;
                regis.Address = reg.Address;
                context.SaveChanges();
                Session["Name"] = reg.Name;
                Response.Write("<script>alert('Record Updated Succesfully')</script>");
                return View("UpdateUserProfile", regis);
            }
            else
            {
                Registration r = context.Registration.Where(i => i.Mobile == reg.Mobile).FirstOrDefault();
                if (r == null)
                {
                    int userid = int.Parse(Session["UserId"].ToString());
                    Registration regis = context.Registration.Include(i => i.Role).Where(i => i.Id == userid).SingleOrDefault();
                    regis.Mobile = reg.Mobile;
                    regis.Name = reg.Name;
                    regis.State = reg.State;
                    regis.City = reg.City;
                    regis.DateOfBirth = reg.DateOfBirth;
                    regis.Gender = reg.Gender;
                    regis.Address = reg.Address;
                    context.SaveChanges();
                    return View("UpdateUserProfile", regis);
                }
                else
                {

                    return RedirectToAction("UpdateUserProfile");
                    //Response.Write("<script>alert('The mobile number already exists')</script>");
                }
            }
        }

        //Action For Updating the Password.
        public ActionResult UpdatePassword()
        {
            if (Session["Role"]==null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                if (Session["Role"].ToString() != "3")
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
        public ActionResult UpdatePassword(Registration reg)
        {
            PolicyContext context = new PolicyContext();
            int userid = int.Parse(Session["UserId"].ToString());
            Registration regis = context.Registration.Include(i => i.Role).Where(i => i.Id == userid).SingleOrDefault();
            Login log = context.Login.Include(i => i.UserId).Where(i => i.UserId.Id == userid).SingleOrDefault();

            regis.Password = reg.Password;
            log.Password = reg.Password;
            context.SaveChanges();
            Response.Write("<script>alert('Your password has been updated.')</script>");
            return View();
        }



        //Action for viewing the deatils of policy by the proposer.
        public ActionResult PolicyDetail(int id)
        {
            if (Session["Role"]==null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                if (Session["Role"].ToString() != "3")
                {
                    return RedirectToAction("Logout", "Proposer");
                }
                else
                {
                    PolicyContext context = new PolicyContext();
                    PolicyDetail pd = context.PolicyDetail.Include(i => i.CatId).Where(i => i.Id == id).SingleOrDefault();
                    return View(pd);
                }
            }
            

        }
    }
}