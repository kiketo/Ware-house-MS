using WHMSData.Models;

namespace WHMS.Services.Contracts
{
    public interface IAddressSevice
    {
        Address Add(Town town, string addressToAdd);

        Address EditText(Town town, string oldAddress, string newAddress);

        Address EditTown(Town oldTown, string address, Town newTown);

        Address Delete(Town town, string addressToDelete);

        Address GetAddress(Town town, string addressToGet);
    }
}
