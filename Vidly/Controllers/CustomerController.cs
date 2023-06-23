using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomerController : Controller
    {
    private ApplicationDbContext _context;

    public CustomerController()
    {
      _context = new ApplicationDbContext();
    }

    protected override void Dispose(bool disposing)
    {
      _context.Dispose();
    }

    public ActionResult New()
    {
      var types = _context.MembershipTypes.ToList();

      var viewModel = new NewCustomerViewModel
      {
        Customer = new Customer(),
        MembershipTypes = types
      };

      return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Save(Customer customer)
    {
      if (!ModelState.IsValid)
      {
        var viewModel = new NewCustomerViewModel 
        { 
          Customer = customer,
          MembershipTypes = _context.MembershipTypes
        };

        return View("new", viewModel);
      }
      
      if (customer.Id == 0)
        _context.Customers.Add(customer);
      else
      {
        var oldCustomer = _context.Customers.Single(c => c.Id == customer.Id);

        oldCustomer.Name = customer.Name;
        oldCustomer.Birthdate = customer.Birthdate;
        oldCustomer.MembershipTypeId = customer.MembershipTypeId;
        oldCustomer.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
      }
      
      _context.SaveChanges();

      return RedirectToAction("Index", "Customer");
    }

    // GET: /customer
    public ActionResult Index()
    {
      var customers = _context.Customers.Include(c => c.MembershipType).ToList();

      CustomerViewModel viewModel = new CustomerViewModel
      {
        Customer = new Customer(),
        Customers = customers
      };

      return View(viewModel);
    }

    // GET: /customer/details/1
      public ActionResult Details(int id)
      {
        if (id == 0)
        {
           return HttpNotFound();
        }

        Customer customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

        if (customer == null)
        {
          return HttpNotFound();
        }

        return View(customer);
      }
    }
}