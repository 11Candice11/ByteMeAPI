using ByteMeAPI.Service.Models.Request;
using ByteMeAPI.Service.Models.Response;

namespace ByteMeAPI.Service
{
    public interface IClientProfileManager
    {
        public List<PersonalDetailsResponse> GetAllClients();
        public PersonalDetailsResponse GetClientProfile(PersonalDetailsRequest request);
        public ComplianceStatusResponse GetCompliance(int entityId);
        public BankDetailsResponse GetBankDetails(int entityId);
        public TaxDetailsResponse GetTaxDetails(int entityId);
        public ContactDetailsResponse GetContactDetails(int entityId);
        public RelationshipDetailsResponse GetRelationshipDetails(int entityId);
        public InteractionDetailsResponse GetInteractionDetails(int entityId);
    }
}
