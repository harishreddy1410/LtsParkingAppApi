//---------------------------------------------------------------------------------------
// Description: contains generic function for the api
//---------------------------------------------------------------------------------------
using AppServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices.Services
{
    public class GenericServices : IGenericServices
    {
        /// <summary>
        /// return current date
        /// </summary>
        /// <returns></returns>
        public DateTime GetCurrentTime()
        {
            try
            {
                return DateTime.Now;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
