using System;
using System.Collections.Generic;
using System.Text;
using WHMSData.Models;

namespace WHMS.Services.Contracts
{
    public interface IJSONImportService
    {
        void ImportTowns(Town[] towns);
    }
}
