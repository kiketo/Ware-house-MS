using WHMSData.Models;

namespace WHMS.Services.Contracts
{
    public interface IPartnerService
    {
        Partner Add(string partnerName, Address address=null, string vat = null);

        Partner Edit(string partnerName, string newPartnerName, string newVat = null);

        Partner Delete(string partnerName);

        Partner FindByName(string partnerName);
    }
}
