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
        Customer customer = _context.Customers.SingleOrDefault(c => c.Id == id);

        if (customer == null)
        {
          return HttpNotFound();
        }

        return View(customer);
      }
    }
}