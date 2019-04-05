using WHMSData.Models;

namespace WHMS.Services.Interfaces
{
    public interface IPartnerService
    {
        Partner Add(string partnerName, int vat = 0);

        Partner Edit(string partnerName, string newPartnerName, int newVat = 0);

        Partner Delete(string partnerName);

        Partner FindByName(string partnerName);
    }
}
