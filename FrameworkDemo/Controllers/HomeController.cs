using System.Collections;
using System.Collections.Generic;
using FrameworkDemo.Models;
using IntSoft.DAL.Models;
using IntSoft.DAL.Repositories;
using System;
using System.Web.Mvc;
using IntSoft.DAL.Models;

namespace FrameworkDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            IEnumerable<Operations> operations = null;
            using (var unit = new UnitOfWork(new FIndTeacherDBEntities()))
            {
                unit.Operations.Add(new Operations
                {
                    Id = Guid.NewGuid(),
                    Name = Guid.NewGuid().ToString()
                });
                operations = unit.Operations.GetAll();
                unit.Complete();
            }

            return View(operations);
        }
    }
}