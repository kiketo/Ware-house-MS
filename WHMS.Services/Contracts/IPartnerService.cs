using System.Collections.Generic;
using System.Threading.Tasks;
using WHMSData.Models;

namespace WHMS.Services.Contracts
{
    public interface IPartnerService
    {
        Task<Partner> AddAsync(string partnerName, Address address=null, string vat = null);

        Task<Partner> UpdateAsync(Partner partner);

        Task<Partner> DeleteAsync(int id);

        Task<Partner> FindByNameAsync(string partnerName);

        Task<Partner> FindByIdAsync(int Id);

        Task<Partner> FindByVATAsync(string VAT);

        Task<IEnumerable<Partner>> GetAllPartners();
    }
}
