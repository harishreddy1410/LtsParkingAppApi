//---------------------------------------------------------------------------------------
// Description: dto for email details
//---------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices.Dto
{
    public class EmailDtoInput
    {
        public string FromNameAlias { get; set; }

        public string FromEmail { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }
    }
}
