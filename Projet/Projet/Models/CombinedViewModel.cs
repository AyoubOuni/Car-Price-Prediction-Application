using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projet.Models
{
    public class CombinedViewModel
    {
        public User Model1Data { get; set; }
        public CarPredictionModel Model2Data
        {
            get; set;
        }
    }
}
