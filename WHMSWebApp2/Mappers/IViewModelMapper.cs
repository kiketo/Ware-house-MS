using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WHMSWebApp2.Mappers
{
    public interface IViewModelMapper<TEntity, TViewModel>
    {
        TViewModel MapFrom(TEntity entity);
    }
}
