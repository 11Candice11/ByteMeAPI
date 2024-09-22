namespace ByteMeAPI.Service.Models.Response
{
    public class PersonalDetailsResponse
    {
        public int EntityID { get; set; }
        public string ClientCode { get; set; }
        public string Company { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public DateTime DateOfBirth { get; set; }
        // Other fields...
    }
}
