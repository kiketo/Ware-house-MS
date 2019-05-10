using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WHMSData.Models;

namespace WHMSWebApp2.Areas.SuperAdmin.Models
{
    public interface IViewModelMapperAsync<TEntity, TViewModel>
    {
        TViewModel MapFromAsync(ApplicationUser entity);
    }
}
