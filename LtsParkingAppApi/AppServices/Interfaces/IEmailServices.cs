//---------------------------------------------------------------------------------------
// Description: interface containing all the functions related to emails
//---------------------------------------------------------------------------------------
using AppServices.Dto;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppServices.Interfaces
{
    public interface IEmailServices
    {
        Task<bool> SendEmailAsync(EmailDtoInput email,IConfiguration config);
    }
}
