namespace ShelterManagementSystem.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    public class City
    {
        public int CityID { get; set; }
        public string CityName { get; set; }
        public string ZipCode { get; set; }
    }

    public class Enclosure
    {
        public int EnclosureID { get; set; }
        public string EnclosureName { get; set; }
        public int Capacity { get; set; }
        public int CityID { get; set; }
    }

    public class Animal
    {
        public int AnimalID { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public string Gender { get; set; }
        public string AdmissionDate { get; set; } 
        public int EnclosureID { get; set; }
        public int CityID { get; set; }
        public string HealthStatus { get; set; }
        public string AdoptionStatus { get; set; }
    }

    public class Adoption
    {
        public int AdoptionID { get; set; }
        public int AnimalID { get; set; }
        public int UserID { get; set; }
        public string RequestDate { get; set; }
        public string Status { get; set; }
    }

    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public int CityID { get; set; }
        public int EnclosureID { get; set; }
    }
}
