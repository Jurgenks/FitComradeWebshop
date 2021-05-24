using FitComrade.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using FitComrade.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FitComrade.Core.Controller
{
    public class CreditController : ControllerBase
    {
        private readonly FitComradeContext _context;

        public CreditController(FitComradeContext context)
        {
            _context = context;
        }

        public bool RedeemCode(ISession session,CreditCode creditCode)
        {            
            var dataCredit = _context.CreditCodes.FirstOrDefault(item=>item.CreditCodeString.Equals(creditCode.CreditCodeString));
            if(dataCredit == null)
            {
                return false;
            }

            if(dataCredit.CreditIsValid)
            {
                int customerID = (int)session.GetInt32("customerID");
                var customerCredits = _context.Credits.First(item => item.CustomerID.Equals(customerID));

                customerCredits.CreditValue += dataCredit.CreditValue;
                dataCredit.CreditIsValid = false;

                _context.CreditCodes.Attach(dataCredit).State = EntityState.Modified;
                _context.Credits.Attach(customerCredits).State = EntityState.Modified;
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool CreateCode(ISession session, CreditCode creditCode)
        {
            if(session.GetInt32("customerID") == 1)
            {
                var creditCodes = _context.CreditCodes.Where(item=>item.CreditCodeString.Equals(creditCode.CreditCodeString)).ToList();

                if(creditCodes.Count == 0)
                {
                    creditCode.CreditIsValid = true;

                    _context.CreditCodes.Add(creditCode);
                    _context.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            return false;
        }

        public void DeleteCode(ISession session, CreditCode creditCode)
        {
            if(session.GetInt32("customerID") == 1)
            {
                _context.CreditCodes.Remove(creditCode);
                _context.SaveChangesAsync();
            }
            
        }
    }
}
