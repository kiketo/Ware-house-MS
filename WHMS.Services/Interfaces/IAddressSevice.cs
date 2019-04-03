using WHMSData.Models;

namespace WHMS.Services.Interfaces
{
    public interface IAddressSevice
    {
        Address Add(string town, string addressToAdd);

        Address Edit(string town, string addressToEdit);

        bool Delete(string town, string addressToDelete);
    }
}
