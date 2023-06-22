using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomerController : Controller
    {
        // GET: /customer
        public ActionResult Index()
        {
            List<Customer> customers = new List<Customer>
            {
              new Customer { Name = "John Smith"},
              new Customer {  Name = "Millie Jones"}
            };

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
         List<Customer> customers = new List<Customer>
        {
          new Customer {Id = 1, Name = "John Smith"},
          new Customer {Id = 2, Name = "Millie Jones"}
        };

        Customer customer = customers.Find(c => c.Id == id);

        if (customer == null)
        {
          return HttpNotFound();
        }

        return View(customer);
      }
    }
}