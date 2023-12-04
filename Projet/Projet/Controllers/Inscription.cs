using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Projet.Data;
using Projet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projet.Controllers
{
    public class Inscription : Controller
    {


        public IActionResult Inscription_User()
        {
            return View();
        }
        private readonly AppDB _context; // Declaring the DbContext

        public Inscription(AppDB context) // Injecting the DbContext in the constructor
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Firstname, Lastname, Email, Password")] User user)
        {
            if (ModelState.IsValid)
            {
            

                _context.users.Add(user);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Registration successfully validated.";
                return RedirectToAction("Auth", "Login");
            }

            return View("Inscription_User", user);
        }



    }

}
