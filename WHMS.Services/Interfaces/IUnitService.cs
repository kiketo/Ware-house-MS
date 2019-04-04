using System.Collections.Generic;
using WHMSData.Models;

namespace WHMS.Services.Interfaces
{
    public interface IUnitService
    {
        Unit CreateUnit(string name);
        Unit DeleteUnitName(int unitId);
        List<Unit> GetAllUnits();
        Unit ModifyUnitName(string name);
    }
}