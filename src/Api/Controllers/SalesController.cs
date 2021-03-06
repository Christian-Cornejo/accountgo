﻿using Microsoft.AspNet.Mvc;
using Services.Administration;
using Services.Sales;
using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class SalesController : Controller
    {
        private readonly IAdministrationService _adminService;
        private readonly ISalesService _salesService;

        public SalesController(IAdministrationService adminService,
            ISalesService salesService)
        {
            _adminService = adminService;
            _salesService = salesService;
        }

        // GET api/sales/getcustomerbyid/1
        [HttpGet]
        [Route("[action]/{id:int}")]
        public IActionResult GetCustomerById(int id)
        {
            try
            {
                var customer = _salesService.GetCustomerById(id);

                if (customer == null)
                {
                    return HttpNotFound();
                }

                var customerModel = new Model.Sales.Customer()
                {
                    Id = customer.Id,
                    No = customer.No,
                    Name = customer.Party.Name,
                    Email = customer.Party.Email,
                    Phone = customer.Party.Phone,
                    Fax = customer.Party.Fax,
                    Balance = customer.Balance
                };

                return new ObjectResult(customerModel);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult CreateCustomer([FromBody]Model.Sales.CreateCustomer model)
        {
            try
            {
                var customer = new Core.Domain.Sales.Customer()
                {
                    Party = new Core.Domain.Party()
                    {
                        PartyType = Core.Domain.PartyTypes.Customer,
                        Name = model.Name,
                        Phone = model.Phone,
                    },
                };

                _salesService.AddCustomer(customer);

                var customerModel = new Model.Sales.Customer();
                customerModel.Id = customer.Id;
                customerModel.No = customer.No;
                customerModel.Name = customer.Party.Name;
                customerModel.Phone = customer.Party.Phone;

                return new ObjectResult(customerModel);
            }
            catch (Exception ex)
            {
                return new ObjectResult(ex);
            }
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetCustomers()
        {
            IList<Model.Sales.Customer> model = new List<Model.Sales.Customer>();
            try
            {
                var customers = _salesService.GetCustomers();
                foreach(var customer in customers)
                {
                    var customerModel = new Model.Sales.Customer()
                    {
                        Id = customer.Id,
                        No = customer.No,
                        Name = customer.Party.Name,
                        Email = customer.Party.Email,
                        Phone = customer.Party.Phone,
                        Fax = customer.Party.Fax,
                        Balance = customer.Balance
                    };

                    model.Add(customerModel);
                }

                return new ObjectResult(model);
            }
            catch(Exception ex)
            {
                return new ObjectResult(ex);
            }
        }
    }
}
