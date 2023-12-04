using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projet.Models
{
    public class CarPredictionModel
    {
        [Key]
        public int Id { get; set; }
       

        [Required(ErrorMessage = "Make is required")]
        public string Make { get; set; }

        [Range(-3, 0, ErrorMessage = "Symboling must be between -3 and 0")]
        public int Symboling { get; set; }

        [Required(ErrorMessage = "Fuel type is required")]
        public string FuelType { get; set; }

        [Required(ErrorMessage = "Aspiration is required")]
        public string Aspiration { get; set; }

        [Range(2, 4, ErrorMessage = "Number of doors must be 2, 4")]
        public int NumOfDoors { get; set; }

        [Required(ErrorMessage = "Body style is required")]
        public string BodyStyle { get; set; }

        [Required(ErrorMessage = "Drive wheels is required")]
        public string DriveWheels { get; set; }

        [Required(ErrorMessage = "Engine type is required")]
        public string EngineType { get; set; }

        [Range(3, 8, ErrorMessage = "Number of cylinders must be between 3 and 8")]
        public int NumOfCylinders { get; set; }

        [Required(ErrorMessage = "Fuel system is required")]
        public string FuelSystem { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Horsepower must be a positive number")]
        public int Horsepower { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Peak RPM must be a positive number")]
        public int PeakRpm { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "City MPG must be a positive number")]
        public int CityMPG { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Highway MPG must be a positive number")]
        public int HighwayMPG { get; set; }


        // Other properties...

        [Required(ErrorMessage = "User ID is required")]
        public int UserId { get; set; }
        public int Price { get; set; }

        public User User { get; set; }
    }
}
