using ByteMeAPI.Service;
using ByteMeAPI.Service.Models.Request;
using ByteMeAPI.Service.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace ByteMeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientProfileController : ControllerBase
    {
        private readonly ILogger<ClientProfileController> _logger;
        private readonly IClientProfileManager _clientProfileManager;

        public ClientProfileController(ILogger<ClientProfileController> logger, IClientProfileManager clientProfileManager)
        {
            _logger = logger;
            _clientProfileManager = clientProfileManager;
        }


        [HttpGet("GetAllClients")]
        public IActionResult GetAllClients()
        {
            try
            {
                var clients = _clientProfileManager.GetAllClients();
                if (clients == null || !clients.Any())
                {
                    return NotFound("No clients found.");
                }
                return Ok(clients);
            }
            catch (Exception ex)
            {
                // Log the exception (using a logging framework)
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost("GetClientProfile")]
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

        [HttpPost("GetCompliance")]
        public ComplianceStatusResponse GetCompliance([FromBody] int request)
        {
            //var personalDetailsRequest = new PersonalDetailsRequest { EntityID = 101 };
            var personalDetails = _clientProfileManager.GetCompliance(request);

            if (personalDetails != null)
            {
                return personalDetails;
            }
            return null;
        }

        [HttpPost("GetBankDetails")]
        public BankDetailsResponse GetBankDetails([FromBody] int request)
        {
            //var personalDetailsRequest = new PersonalDetailsRequest { EntityID = 101 };
            var personalDetails = _clientProfileManager.GetBankDetails(request);

            if (personalDetails != null)
            {
                return personalDetails;
            }
            return null;
        }

        [HttpPost("GetTaxDetails")]
        public TaxDetailsResponse GetTaxDetails([FromBody] int request)
        {
            //var personalDetailsRequest = new PersonalDetailsRequest { EntityID = 101 };
            var personalDetails = _clientProfileManager.GetTaxDetails(request);

            if (personalDetails != null)
            {
                return personalDetails;
            }
            return null;
        }

        [HttpPost("GetContactDetails")]
        public ContactDetailsResponse GetContactDetails([FromBody] int request)
        {
            //var personalDetailsRequest = new PersonalDetailsRequest { EntityID = 101 };
            var personalDetails = _clientProfileManager.GetContactDetails(request);

            if (personalDetails != null)
            {
                return personalDetails;
            }
            return null;
        }

        [HttpPost("GetRelationshipDetails")]
        public RelationshipDetailsResponse GetRelationshipDetails([FromBody] int request)
        {
            //var personalDetailsRequest = new PersonalDetailsRequest { EntityID = 101 };
            var personalDetails = _clientProfileManager.GetRelationshipDetails(request);

            if (personalDetails != null)
            {
                return personalDetails;
            }
            return null;
        }

        [HttpPost("GetInteractionDetails")]
        public InteractionDetailsResponse GetInteractionDetails([FromBody] int request)
        {
            //var personalDetailsRequest = new PersonalDetailsRequest { EntityID = 101 };
            var personalDetails = _clientProfileManager.GetInteractionDetails(request);

            if (personalDetails != null)
            {
                return personalDetails;
            }
            return null;
        }
    }
}
