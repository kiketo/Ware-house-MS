using System;
using System.Collections.Generic;
using System.Text;
using WHMSData.Models;

namespace WHMS.Services.Interfaces
{
    public interface ITownService
    {
        Town Add(string townToAddName);
        Town Edit(string townToEditName);
        void Delete(string townToDeleteName);
    }
}
