using System;
using System.Collections.Generic;
using System.Text;
using WHMSData.Context;

namespace WHMS.Commands.Contracts
{
    public abstract class BaseCommand : ICommand
    {
        public BaseCommand(IWHMSContext context)
        {
            this.WarehouseContext = context;
        }

        protected IWHMSContext WarehouseContext { get; }

        public abstract string Execute(IList<string> parameters);
        

    }
}
