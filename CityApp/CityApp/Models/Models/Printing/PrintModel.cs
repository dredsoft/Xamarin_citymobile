using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityApp.Models.Models.Printing
{
    public class PrintModel
    {
        #region Properties   
          
        public string ViolationName { get; set; } 
        public string ViolationCode { get; set; } 
        public string LicensePlate { get; set; } 
        public long CitationNumber { get; set; } 
        public string VehicleColor { get; set; } 
        public string VehicleMake { get; set; } 
        public string VehicleModel { get; set; } 
        public string VehicleType { get; set; } 
        public string City { get; set; } 
        public string State { get; set; } 
        public string Location { get; set; } 
        public string OfficerName { get; set; } 
        public string OfficerBadgeNumber { get; set; } 
        public string Fee { get; set; } 
        public DateTime CreateUtc { get; set; }
         
        #endregion
    }
}
