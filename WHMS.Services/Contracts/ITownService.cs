using System;
using System.Collections.Generic;
using System.Text;
using WHMSData.Models;

namespace WHMS.Services.Contracts
{
    public interface ITownService
    {
        Town Add(string townToAddName);
        Town Edit(string townToEditName);
        Town Delete(string townToDeleteName);
    }
}
