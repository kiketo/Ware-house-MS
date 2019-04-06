using System;
using System.Collections.Generic;
using System.Text;
using WHMS.Commands.Contracts;
using WHMS.Services.Contracts;

namespace WHMS.Commands.Creating
{
    public class CreatePartnerCommand : ICommand
    {
        private IPartnerService partnerService;

        public CreatePartnerCommand(IPartnerService partnerService)
        {
            this.partnerService = partnerService ?? throw new ArgumentNullException(nameof(partnerService));
        }

        public string Execute(IReadOnlyList<string> parameters)
        {


            throw new NotImplementedException();
        }
    }
}
