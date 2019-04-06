using WHMSData.Models;

namespace WHMS.Services.Contracts
{
    public interface IAddressSevice
    {
        Address Add(Town town, string addressToAdd);

        Address EditText(Town town, string addressToEdit);

        Address EditTown(Town oldTown, string addressToEdit, Town newTown);

        Address Delete(Town town, string addressToDelete);

        Address GetAddress(Town town, string addressToGet);
    }
}
