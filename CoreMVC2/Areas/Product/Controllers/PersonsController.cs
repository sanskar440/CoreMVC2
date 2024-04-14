using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreMVC2.Data;
using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using DataAccess.Services;
using CoreMVC2.Models;
using Microsoft.AspNetCore.Identity;

namespace CoreMVC2.Areas.Product.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Area("Product")]

   
    public class PersonsController : Controller
    {


        //public IPersonsInterface PersonInterface;

        //public PersonsController(IPersonsInterface PersonInterface)
        //{
        //    this.PersonInterface=PersonInterface;

        //}

        


        public async Task<IActionResult> Index(string search = "", int page =1, int pageSize = 50, string sortby = "", string orderBy = "")
        {
            DataAccess.Repository.PersonsServices personsServices = new DataAccess.Repository.PersonsServices();
            if (string.IsNullOrEmpty(sortby))
            {
                sortby = "FirstName"; // Default sort column
                orderBy = "asc";      // Default sort order
            }

            var persons = personsServices.GetPersons(search, page, pageSize, sortby, orderBy);

            var personViewModel = new PersonViewModel
            {
                Persons = persons,
                Search = search,
                Page = page,
                PageSize = pageSize,
                SortBy = sortby,
                OrderBy = orderBy
            };
            ViewBag.SortBy = sortby;
            ViewBag.OrderBy = orderBy;

            return View(personViewModel);
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult IsFirstNameAvailable(string firstName)
        {
            DataAccess.Repository.PersonsServices personsServices = new DataAccess.Repository.PersonsServices();
            bool isFirstNameAvailable = !personsServices.IsFirstNameExists(firstName);
            return Json(isFirstNameAvailable);
        }

      


        // GET: Product/Persons/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DataAccess.Models.Persons persons)
        {
            if (ModelState.IsValid)
            {
                DataAccess.Repository.PersonsServices personsServices = new DataAccess.Repository.PersonsServices();
                var returnvalue = personsServices.insertpersons(persons);
                return RedirectToAction(nameof(Index));
            }
            return View(persons);
        }

        
       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            DataAccess.Repository.PersonsServices personsServices = new DataAccess.Repository.PersonsServices();
           var result=  personsServices.GetUpdatePersons(id.Value);

            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }


        [HttpPost]
       
        public async Task<IActionResult> Edit(int id, DataAccess.Models.Persons persons)
        {   

             if (ModelState.IsValid)
            {
                DataAccess.Repository.PersonsServices personsServices = new DataAccess.Repository.PersonsServices();
                var returnvalue = personsServices.UpdatePersons(persons);
                return RedirectToAction(nameof(Index));

            }
            return View(persons);
        }

        //public async Task<IActionResult> Delete(int? id)
        //{

        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    DataAccess.Repository.PersonsServices personsServices = new DataAccess.Repository.PersonsServices();
        //    var result = personsServices.GetUpdatePersons(id.Value);

        //    if (result == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(result);
        //}

        //// POST: Product/Persons/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Delete(int id, DataAccess.Models.Persons persons)
        //{

        //        DataAccess.Repository.PersonsServices personsServices = new DataAccess.Repository.PersonsServices();
        //         personsServices.DeletePerson(id);
        //       return RedirectToAction(nameof(Index));

        //    return View(persons);
        //}


        // GET: Product/Persons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DataAccess.Repository.PersonsServices personsServices = new DataAccess.Repository.PersonsServices();
            var personToDelete = personsServices.GetUpdatePersons(id.Value);

            if (personToDelete == null)
            {
                return NotFound();
            }

            return View(personToDelete);
        }

        // POST: Product/Persons/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            DataAccess.Repository.PersonsServices personsServices = new DataAccess.Repository.PersonsServices();
            personsServices.DeletePerson(id);
            return RedirectToAction(nameof(Index));
        }


    }
}



//public ActionResult Edit(int id)
//{
//    Persons person = PersonsServices.GetUpdatePerson(id);
//    return View(person);
//}


//[HttpPost]
//public IActionResult Edit(DataAccess.Models.Persons updatedPerson)
//{
//    if (ModelState.IsValid)
//    {
//        DataAccess.Repository.PersonsServices personsServices = new DataAccess.Repository.PersonsServices();
//        personsServices.UpdatePersons(updatedPerson);

//        return RedirectToAction(nameof(Index));
//    }

//    return View(updatedPerson);
//}




// GET: Product/Persons/Edit/5


//// POST: Product/Persons/Edit/5
//// To protect from overposting attacks, enable the specific properties you want to bind to.
//// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.


//// GET: Product/Persons/Delete/5


//private bool PersonsExists(int id)
//{
//    return _context.Persons.Any(e => e.PersonID == id);
//}
