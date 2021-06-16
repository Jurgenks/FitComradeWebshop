using FitComrade.Data;
using FitComrade.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FitComrade.Core.Services
{
    public class AccountService
    {
        private readonly FitComradeContext _context;

        public AccountService(FitComradeContext context)
        {
            _context = context;
        }

        //Account
        public bool LoginProfile(ISession session, Customer profile) //Read Account
        {

            var login = _context.Customers.FirstOrDefault(s => s.UserName.Equals(profile.UserName) && s.Password.Equals(profile.Password));

            if (login != null)
            {

                //Je huidige sessie ontvangen variabelen om toegang te verkrijgen
                session.SetString("userName", profile.UserName);
                session.SetInt32("profileID", login.CustomerID);
                session.SetInt32("customerID", login.CustomerID);
                //Login succes
                return true;
            }
            //Login failed
            return false;
        }

        public bool CreateProfile(Customer profile) // Create Account
        {
            var register = _context.Customers.Where(s => s.UserName.Equals(profile.UserName));
            //Check of profile al bestaat
            if (register.Count() == 0)
            {
                _context.Customers.Add(profile);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
