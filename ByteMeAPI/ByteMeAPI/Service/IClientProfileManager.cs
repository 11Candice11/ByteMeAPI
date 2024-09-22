using ByteMeAPI.Service.Models.Request;
using ByteMeAPI.Service.Models.Response;

namespace ByteMeAPI.Service
{
    public interface IClientProfileManager
    {
        //public ClientResponse GetAllClients(ClientRequest request);
        public PersonalDetailsResponse GetClientProfile(PersonalDetailsRequest request);
    }
}
