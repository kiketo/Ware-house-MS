using WHMSData.Models;

namespace WHMS.Services.Interfaces
{
    public interface IAddressSevice
    {
        Address Add(string town, string addressToAdd);

        Address Edit(string town, string addressToEdit);

        Address Delete(string town, string addressToDelete);
    }
}
