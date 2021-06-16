using FitComrade.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitComrade.Core.Services
{
    public interface ICreditService
    {
        bool RedeemCode(ISession session, CreditCode creditCode);
        bool CreateCode(ISession session, CreditCode creditCode);
        void DeleteCode(ISession session, CreditCode creditCode);
    }
}
