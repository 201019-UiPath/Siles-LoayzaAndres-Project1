namespace StoreDB.Models
{
    /// <summary>
    /// Represents a real-world mailing address.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Unique ID. Primary key in the database.
        /// </summary>
        /// <value></value>
        public int Id {get; set;}
        /// <summary>
        /// Street address.
        /// </summary>
        /// <value></value>
        public string Street {get; set;}
        /// <summary>
        /// City.
        /// </summary>
        /// <value></value>
        public string City {get; set;}
        /// <summary>
        /// Zip code.
        /// </summary>
        /// <value></value>
        public int Zip {get; set;}
        /// <summary>
        /// State or territory.
        /// </summary>
        /// <value></value>
        public string State {get; set;}
        /// <summary>
        /// Country or nation.
        /// </summary>
        /// <value></value>
        public string Country {get; set;}

        public Address(){}

        public Address(string street, string city, string state, int zip, string country)
        {
            this.Street = street;
            this.City = city;
            this.State = state;
            this.Zip = zip;
            this.Country = country;
        }

        public override string ToString()
        {
            return $"{Street} {City}, {State} {Zip}, {Country}";
        }
    }
}