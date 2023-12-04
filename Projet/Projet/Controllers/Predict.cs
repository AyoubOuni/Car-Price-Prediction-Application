using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projet.Data;
using Projet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projet.Controllers
{
    public class Predict : Controller
    {
        // GET: Predict
        private readonly AppDB _context;

        public Predict(AppDB context)
        {
            _context = context;
        }

            public ActionResult Prediction()
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





        [HttpPost]
        public IActionResult SubmitForm(int UserId, string Make, int Symboling, string FuelType, string Aspiration, int NumOfDoors, string BodyStyle, string DriveWheels, string EngineType, int NumOfCylinders, string FuelSystem, int Horsepower, int PeakRpm, int CityMPG, int HighwayMPG)
        {
            // Create a new instance of the CarData model and populate it with form data
            var carData = new CarPredictionModel
            {
                UserId = UserId,
                Make = Make,
                Symboling = Symboling,
                FuelType = FuelType,
                Aspiration = Aspiration,
                NumOfDoors = NumOfDoors,
                BodyStyle = BodyStyle,
                DriveWheels = DriveWheels,
                EngineType = EngineType,
                NumOfCylinders = NumOfCylinders,
                FuelSystem = FuelSystem,
                Horsepower = Horsepower,
                PeakRpm = PeakRpm,
                CityMPG = CityMPG,
                HighwayMPG = HighwayMPG,
                Price=2000
                // Populate other properties as needed based on your CarData model
            };

            // Add the newly created CarData object to the context
            _context.CarPredictions.Add(carData);

            // Save changes to the database
            _context.SaveChanges();

            // Redirect to a success page or return a view
            return RedirectToAction("Index", "Home");
        }


    }
}
