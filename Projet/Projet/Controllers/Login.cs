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
    public class Login : Controller
    {

        public IActionResult Auth()
        {
            return View();
        }
        private readonly AppDB _context;

        public Login(AppDB context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult authentification()
        {
           
                string email = Request.Form["Email"];
                string password = Request.Form["Password"];

                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                {
                    var existingUser = _context.users.FirstOrDefault(u => u.Email == email);

                    if (existingUser != null)
                    {
                       

                        if (existingUser.Password == password)
                        {
                        // Passwords match, authentication successful
                        // Perform authentication logic (e.g., set authentication cookie)
                        // Redirect to a dashboard or home page upon successful login
                        Response.Cookies.Append("userId", existingUser.Id.ToString());

                        TempData["CurrentUser"] = Newtonsoft.Json.JsonConvert.SerializeObject(existingUser);
                        return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            // Passwords do not match, add model error for invalid credentials
                            ModelState.AddModelError(string.Empty,"Invalide Email or Password");

                        return View("Auth");
                        }
                    }
                    else
                    {
                        // User not found in the database, add model error for invalid credentials
                        ModelState.AddModelError(string.Empty, "Invalide Email or Password");
                        return View("Auth");
                    }
                }

                // If email or password is empty, add model error and return to the login view
                ModelState.AddModelError(string.Empty, "Email and password are required");
                return View("Auth");
            }


        }
    }
