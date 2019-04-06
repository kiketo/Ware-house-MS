using WHMSData.Models;

namespace WHMS.Services.Contracts
{
    public interface IAddressSevice
    {
        Address Add(string town, string addressToAdd);

        Address EditText(string town, string addressToEdit);

        Address EditTown(string oldTown, string addressToEdit, string newTown);

        Address Delete(string town, string addressToDelete);
    }
}
