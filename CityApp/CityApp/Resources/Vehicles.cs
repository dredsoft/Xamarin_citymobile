using System.Collections.Generic;

namespace CityApp.Resources
{
    public class Vehicles
    {


        #region Private Fields

        public static Dictionary<string, string> Makes;

        #endregion

        #region Constructors

        static Vehicles()
        {
            Makes = new Dictionary<string, string>();
            InitializeMakes();
        }

        #endregion

        #region Private Fiedls

        private static void InitializeMakes()
        {
            Makes.Add("Acura", "Acura");
            Makes.Add("Audi", "Audi");
            Makes.Add("BMW", "BMW");
            Makes.Add("Buick", "Buick");
            Makes.Add("Cadillac", "Cadillac");
            Makes.Add("Chevrolet", "Chevrolet");
            Makes.Add("Chrystler", "Chrystler");
            Makes.Add("Dodge", "Dodge");
            Makes.Add("Fiat", "Fiat");
            Makes.Add("Ford", "Ford");
            Makes.Add("GMC", "GMC");
            Makes.Add("Honda", "Honda");
            Makes.Add("Hummer", "Hummer");
            Makes.Add("Hyundai", "Hyundai");
            Makes.Add("Infinity", "Infinity");
            Makes.Add("Jaguar", "Jaguar");
            Makes.Add("Jeep", "Jeep");
            Makes.Add("Kai", "Kia");
            Makes.Add("Land Rover", "Land Rover");
            Makes.Add("Lexus", "Lexus");
            Makes.Add("Lincoln", "Lincoln");
            Makes.Add("Mazda", "Mazda");
            Makes.Add("Mercedes-Benz", "Mercedes-Benz");
            Makes.Add("Mercury", "Mercury");
            Makes.Add("Mini", "Mini");
            Makes.Add("Nissan", "Nissan");
            Makes.Add("Pontiac", "Pontiac");
            Makes.Add("Porshe", "Porshe");
            Makes.Add("Ram", "Ram");
            Makes.Add("Saturn", "Saturn");
            Makes.Add("Scion", "Scion");
            Makes.Add("Smart", "Smart");
            Makes.Add("Subaru", "Subaru");
            Makes.Add("Tesla", "Tesla");
	        Makes.Add("Toyota", "Toyota");
			Makes.Add("Volkswagen", "Volkswagen");
            Makes.Add("Volvo", "Volvo");
		}

        #endregion
    }
}
