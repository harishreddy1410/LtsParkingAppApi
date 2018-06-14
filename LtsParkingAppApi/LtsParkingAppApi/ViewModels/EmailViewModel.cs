//---------------------------------------------------------------------------------------
// Description: view model for email details
//---------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LtsParkingAppApi.ViewModels
{
    public class EmailViewModel
    {
        public string FromNameAlias { get; set; }

        public string FromEmail { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }
    }
}
