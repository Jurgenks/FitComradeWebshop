using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FitComrade.Models
{
    public class SessionUser
    {
        public string SessionUserID { get; set; }
        public int ProfileID { get; set; }
        public string CartID { get; set; }
        public int CustomerID { get; set; }
        public string UserName { get; set; }
        
        public SessionUser GetSession(ISession session, SessionUser sessionUser)
        {
            
            if(session.Keys.Contains("sessionUserID"))
            {
                sessionUser.SessionUserID = session.GetString("sessionUserID");
            }
            else
            {
                session.SetString("sessionUserID", session.Id);
                sessionUser.SessionUserID = session.GetString("sessionUserID");
            }

            if (session.Keys.Contains("profileID"))
            {
                sessionUser.ProfileID = (int)session.GetInt32("profileID");
            } 
            if(session.Keys.Contains("cartID"))
            {
                sessionUser.CartID = session.GetString("cartID");
            }
            if(session.Keys.Contains("customerID"))
            {
                sessionUser.CustomerID = (int)session.GetInt32("customerID");
            }
            if(session.Keys.Contains("userName"))
            {
                sessionUser.UserName = session.GetString("userName");
            }
            
            return sessionUser;
        }
        public void LogOutSession(ISession session)
        {                
            session.Clear();
        }
        public void AttemptLogin(ISession session)
        {
            if(session.Keys.Contains("attempts"))
            {
               session.SetInt32("attempts", (int)session.GetInt32("attempts") + 1);               
            }
            else
            {
                session.SetInt32("attempts", 1);                
            }
        }
        public int GetAttemptLogin(ISession session)
        {
            if(!session.Keys.Contains("attempts"))
            {
                session.SetInt32("attempts", 0);
            }
            return (int)session.GetInt32("attempts");
        }
    }
}
