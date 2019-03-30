using System;
using System.Collections.Generic;
using System.Text;

namespace WHMSData.Contracts
{
    public class Model
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public  bool IsDeleted { get; set; }
    }
}
