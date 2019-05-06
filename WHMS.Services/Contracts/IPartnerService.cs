using System.Collections.Generic;
using System.Threading.Tasks;
using WHMSData.Models;

namespace WHMS.Services.Contracts
{
    public interface IPartnerService
    {
        Task<Partner> AddAsync(string partnerName, Address address=null, string vat = null);

        Task<Partner> EditAsync(string partnerName, string newPartnerName, string newVat = null);

        Task<Partner> DeleteAsync(string partnerName);

        Task<Partner> FindByNameAsync(string partnerName);

        Task<Partner> FindByIdAsync(int Id);

        Task<Partner> FindByVATAsync(string VAT);

        Task<IEnumerable<Partner>> GetAllPartners();
    }
}
