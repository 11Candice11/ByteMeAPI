using ByteMeAPI.Service;
using ByteMeAPI.Service.Models.Request;
using ByteMeAPI.Service.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace ByteMeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientProfileController : ControllerBase
    {
        private readonly ILogger<ClientProfileController> _logger;
        private readonly IClientProfileManager _clientProfileManager;

        public ClientProfileController(ILogger<ClientProfileController> logger, IClientProfileManager clientProfileManager)
        {
            _logger = logger;
            _clientProfileManager = clientProfileManager;
        }


        //[HttpPost(Name = "AllClients")]
        //public ClientResponse GetAllClients([FromBody] ClientRequest request)
        //{
        //    var clients = _clientProfileManager.GetAllClients(request);

        //    if (clients != null)
        //    {
        //        Console.WriteLine($"Retrieved all clients");
        //        return clients;
        //    }
        //    return null;
        //}

        [HttpPost(Name = "GetClientProfile")]
        public PersonalDetailsResponse GetClientProfile([FromBody] PersonalDetailsRequest request)
        {
            //var personalDetailsRequest = new PersonalDetailsRequest { EntityID = 101 };
            var personalDetails = _clientProfileManager.GetClientProfile(request);

            if (personalDetails != null)
            {
                Console.WriteLine($"Client Name: {personalDetails.FirstName} {personalDetails.Surname}");
                return personalDetails;
            }
            return null;
        }
    }
}
