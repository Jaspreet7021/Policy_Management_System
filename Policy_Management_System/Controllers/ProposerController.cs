using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Policy_Management_System.Models;

namespace Policy_Management_System.Controllers
{
    public class ProposerController : Controller
    {
        //
        // GET: /Proposer/

        //Action for updating the user details.
        public ActionResult UpdateRecord()
        {
            if (Session["Role"].ToString() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                if (Session["Role"].ToString() != "2")
                {
                    return RedirectToAction("Logout", "Proposer");
                }
                else
                {
                    PolicyContext context = new PolicyContext();
                    int userid = int.Parse(Session["UserId"].ToString());
                    Registration reg = context.Registration.Include(i => i.Role).Where(i => i.Id == userid).SingleOrDefault();
                    return View("UpdateRecord", reg);
                }
            }
            
            
            
        }

        [HttpPost]
        public ActionResult UpdateRecord(Registration reg)
        {
            PolicyContext context = new PolicyContext();
            if (reg.Mobile==Session["Mobile"].ToString())
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
                return View("UpdateRecord", regis);
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
                    return View("UpdateRecord", regis);
                }
                else
                {

                    return RedirectToAction("UpdateRecord");
                    //Response.Write("<script>alert('The mobile number already exists')</script>");
                }
            }
            
            
        }

        //Action for updating the password.
        public ActionResult UpdatePassword()
        {
            if (Session["Role"]== null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                if (Session["Role"].ToString() != "2")
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
            Login log = context.Login.Include(i=>i.UserId).Where(i => i.UserId.Id == userid).SingleOrDefault();
            
            regis.Password = reg.Password;
            log.Password = reg.Password;
            context.SaveChanges();
            Response.Write("<script>alert('Your password has been updated.')</script>");
            return View("UpdatePassword");
        }

        //Action for adding the policy by the proposer
        public ActionResult Index()
        {
            if (Session["UserId"] == null)
            {

                return RedirectToAction("Index", "Login");
            }
            else
            {
                if (Session["Role"].ToString()=="2")
                {
                    PolicyContext context = new PolicyContext();
                    ViewBag.Categories = new SelectList(context.Category, "Id", "Name");
                    return View("Index");
                }
                else
                {
                    Session["Role"] = null;
                    return RedirectToAction("Index", "Login");
                }
                
            }
            
        }

        [HttpPost]
        public ActionResult Index(PolicyDetail pd)
        {
            PolicyContext context = new PolicyContext();
            int catid = int.Parse(Request.Form["Categories"].ToString());
            int userId = int.Parse(Session["UserId"].ToString());
            Registration reg = context.Registration.Where(i=>i.Id==userId).SingleOrDefault();
            Category cat = context.Category.Where(i => i.Id == catid).SingleOrDefault();
            pd.CatId = cat;
            pd.ProposerId = reg;
            context.PolicyDetail.Add(pd);
            context.SaveChanges();
            ViewBag.Categories = new SelectList(context.Category, "Id", "Name");
            Response.Write("<script>alert('The Policy details have been added succesfully.')</script>");
            ModelState.Clear();
            return View();
        }

        //Action for viewing the added policy.
        public ActionResult ViewPolicy()
        {
            if (Session["Role"]==null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                if (Session["Role"].ToString() != "2")
                {
                    return RedirectToAction("Logout", "Proposer");

                }
                else
                {
                    PolicyContext context = new PolicyContext();
                    int userId = int.Parse(Session["UserId"].ToString());
                    List<PolicyDetail> pd = context.PolicyDetail.Include(i => i.CatId).Where(i => i.ProposerId.Id == userId).ToList();
                    return View(pd);
                }
            }
            
            
        }

        //Action for editing the added policy.
        public ActionResult EditPolicy(int id)
        {
            if (Session["Role"]==null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                if (Session["Role"].ToString() != "2")
                {
                    return RedirectToAction("Logout", "Proposer");
                }
                else
                {
                    PolicyContext context = new PolicyContext();
                    PolicyDetail pd = context.PolicyDetail.Include(i => i.CatId).Where(i => i.Id == id).SingleOrDefault();
                    ViewBag.Categories = new SelectList(context.Category, "Id", "Name", pd.CatId.Id);
                    return View(pd);
                }
            }
            
            
        }

        [HttpPost]
        public ActionResult EditPolicy(PolicyDetail pd,int id)
        {
            PolicyContext context = new PolicyContext();
            int catid = int.Parse(Request.Form["Categories"].ToString());
            PolicyDetail pde = context.PolicyDetail.Include(i => i.CatId).Where(i => i.Id == id).SingleOrDefault();
            Category cat = context.Category.Where(i => i.Id == catid).SingleOrDefault();
            pde.PolicyName = pd.PolicyName;
            pde.Premium = pd.Premium;
            pde.SumAssured = pd.SumAssured;
            pde.Tenure = pd.Tenure;
            pde.CatId = cat;
            context.SaveChanges();
            ViewBag.Categories = new SelectList(context.Category, "Id", "Name", pde.CatId.Id);
            Response.Write("<script>alert('The policy details have been updated succesfully.')</script>");
            return View(pd);
        }

        //Action for deleting the added policy.
        public ActionResult DeletePolicy(int id)
        {
            PolicyContext context = new PolicyContext();
            PolicyDetail pd = context.PolicyDetail.Include(i => i.CatId).Where(i => i.Id == id).SingleOrDefault();
            context.PolicyDetail.Remove(pd);
            context.SaveChanges();
            Response.Write("<script>alert('The selected policy has been deleted succesfully.')</script>");

            
            return RedirectToAction("ViewPolicy");
        }

        //Action for closing all the sessions.
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index","Login");   
        }

	}
}