using System.Collections.Generic;
using System.Threading.Tasks;
using WHMSData.Models;

namespace WHMS.Services.Contracts
{
    public interface IPartnerService
    {
        Task<Partner> AddAsync(string partnerName, ApplicationUser user, Address address = null, string vat = null);

        Task<Partner> UpdateAsync(Partner partner);

        Task<Partner> DeleteAsync(int id);

        Task<Partner> GetByNameAsync(string partnerName);

        Task<Partner> GetByIdAsync(int Id);

        Task<Partner> GetByVATAsync(string VAT);

        Task<List<Partner>> GetAllPartners();

        Task<List<Partner>> GetPartnersByCreatorId(string userId);
    }
}
