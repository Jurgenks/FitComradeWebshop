using FitComrade.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitComrade.Core.Services
{
    public interface IAccountService
    {
        bool LoginProfile(ISession session, Customer profile);
        bool CreateProfile(Customer profile);
    }
}
