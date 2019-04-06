using System;
using System.Collections.Generic;
using System.Text;
using WHMSData.Models;

namespace WHMS.Services.Contracts
{
    public interface ITownService
    {
        Town Add(string townToAddName);
        Town Edit(string oldTownName, string newTownName);
        Town Delete(string townToDeleteName);
        Town GetTown(string townToGetName);
    }
}
