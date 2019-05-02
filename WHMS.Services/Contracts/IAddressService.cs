using System.Threading.Tasks;
using WHMSData.Models;

namespace WHMS.Services.Contracts
{
    public interface IAddressService
    {
        Task<Address> AddAsync(Town town, string addressToAdd);

        Task<Address> EditTextAsync(Town town, string oldAddress, string newAddress);

        Task<Address> EditTownAsync(Town oldTown, string address, Town newTown);

        Task<Address> DeleteAsync(Town town, string addressToDelete);

        Address GetAddress(Town town, string addressToGet);
    }
}




