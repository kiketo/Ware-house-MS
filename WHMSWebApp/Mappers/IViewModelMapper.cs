using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WHMSWebApp.Mappers
{
    public interface IViewModelMapper<TEntity, TViewModel>
    {
        TViewModel MapFrom(TEntity entity);
    }
}
