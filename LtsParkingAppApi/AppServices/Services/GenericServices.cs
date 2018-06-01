using AppServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices.Services
{
    public class GenericServices : IGenericServices
    {
        public DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }
    }
}
