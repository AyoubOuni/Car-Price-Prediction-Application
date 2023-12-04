using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Projet.Data;
using Projet.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Projet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        public IActionResult Index()
        {
            if (Request.Cookies.TryGetValue("userId", out string userIdStr))
            {
                if (int.TryParse(userIdStr, out int userId))
                {
                    var existingUser = _context.users.FirstOrDefault(u => u.Id == userId);

                    // Check if the user exists and handle accordingly
                    if (existingUser != null)
                    {
                        // Use the 'existingUser' object as needed
                        // Example: ViewBag.CurrentUser = existingUser; // Pass it to the view using ViewBag
                        ViewBag.CurrentUser = existingUser;

                        return View(); // Return the Index view
                    }
                }
            }

            // Handle cases where userId is not found, user doesn't exist, or cookie information is invalid
            // For example:
            return RedirectToAction("Auth", "Login"); // Redirect to the authentication page or handle accordingly

        }

        private readonly AppDB _context;

        public HomeController(AppDB context)
        {
            _context = context;
        }

        public IActionResult Logout()
        {
            TempData["CurrentUser"] = null; // Clear the TempData storing user information
            Response.Cookies.Delete("userId");

            return RedirectToAction("Auth", "Login"); // Redirect to the authentication page
        }
        public IActionResult Setting()
        {
            if (Request.Cookies.TryGetValue("userId", out string userIdStr))
            {
                if (int.TryParse(userIdStr, out int userId))
                {
                    var existingUser = _context.users.FirstOrDefault(u => u.Id == userId);

                    // Check if the user exists and handle accordingly
                    if (existingUser != null)
                    {
                        ViewBag.user = existingUser;
                        return View();
                    }
                }
            }

            // Handle cases where userId is not found or user doesn't exist
            // For example:
            ViewBag.ErrorMessage = "User not found or invalid user ID";
            return View(); // Or redirect to an error view/page
        }




        public IActionResult History()
        {

            if (Request.Cookies.TryGetValue("userId", out string userIdStr))
            {
                if (int.TryParse(userIdStr, out int userId))
                {
                    var existingUser = _context.users.FirstOrDefault(u => u.Id == userId);

                    // Check if the user exists and handle accordingly
                    if (existingUser != null)
                    {
                        // Use the 'existingUser' object as needed
                        // Example: ViewBag.CurrentUser = existingUser; // Pass it to the view using ViewBag
                        ViewBag.CurrentUser = existingUser;
                        ViewBag.username= existingUser.Firstname + " " + existingUser.Lastname;
                        var id = existingUser.Id;
                        List<CarPredictionModel> userHistory = _context.CarPredictions
              .Where(model => model.UserId == userId)
              .ToList();
                        ViewBag.list = userHistory;
                        // Pass the user's history data to the view
                        return View(userHistory);
                    }
                }
            }

            // Handle cases where userId is not found, user doesn't exist, or cookie information is invalid
            // For example:
            return RedirectToAction("Auth", "Login");
            // Fetch history data for a specific user by their ID
          
        }



        [HttpPost]
        public ActionResult UpdateUser(User updatedUser, string oldPassword, string newPassword, string confirmPassword)
        {
            if (Request.Cookies.TryGetValue("userId", out string userIdStr))
            {
                if (int.TryParse(userIdStr, out int userId))
                {
                    var existingUser = _context.users.FirstOrDefault(u => u.Id == userId);

                    if (existingUser != null)
                    {
                        // Verify old password if it's provided
                        if (!string.IsNullOrEmpty(oldPassword) && oldPassword != existingUser.Password)
                        {
                            TempData["InvalidOldPasswordError"] = "The old password is incorrect.";
                            return RedirectToAction("Setting", "Home");
                        }

                        // Check if the new email is already in use by another user
                        var emailInUse = _context.users.Any(u => u.Email == updatedUser.Email && u.Id != userId);

                        // If the email is in use by another user and it's not the user's current email, show the error
                        if (emailInUse && updatedUser.Email != existingUser.Email)
                        {
                            existingUser.Firstname = updatedUser.Firstname;
                            existingUser.Lastname = updatedUser.Lastname;
                            _context.SaveChanges();

                            TempData["EmailInUseError"] = "The email is already in use.";
                            return RedirectToAction("Setting", "Home");
                        }

                        // Update user details
                        existingUser.Firstname = updatedUser.Firstname;
                        existingUser.Lastname = updatedUser.Lastname;
                        existingUser.Email = updatedUser.Email;

                        // Update password only if both new password and confirm password are provided and match
                        if (!string.IsNullOrEmpty(newPassword) && !string.IsNullOrEmpty(confirmPassword))
                        {
                            if (newPassword != confirmPassword)
                            {
                                TempData["PasswordMismatchError"] = "New password and confirm password do not match.";
                                return RedirectToAction("Setting", "Home");
                            }

                            // Update password
                            existingUser.Password = newPassword;
                        }

                        // Save changes to the database
                        _context.SaveChanges();
                        TempData["SuccessMessage"] = "User details updated successfully.";

                        // Redirect to the Settings page or success page
                        return RedirectToAction("Setting", "Home");
                    }
                }
            }

            // Handle other cases (e.g., user not found, incorrect input, etc.)
            return RedirectToAction("Error", "Home");
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
