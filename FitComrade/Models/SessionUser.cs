using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FitComrade.Models
{
    public class SessionUser
    {
        public string SessionUserID { get; private set; }

        public int ProfileID { get; private set; }

        public int CustomerID { get; private set; }

        public int AdressID { get; private set; }

        public string UserName { get; private set; }

        public SessionUser GetSession(ISession session)
        {

            if (session.Keys.Contains("sessionUserID"))
            {
                this.SessionUserID = session.GetString("sessionUserID");
            }
            else
            {
                session.SetString("sessionUserID", session.Id);
                this.SessionUserID = session.GetString("sessionUserID");
            }

            if (session.Keys.Contains("profileID"))
            {
                this.ProfileID = (int)session.GetInt32("profileID");
            }

            if (session.Keys.Contains("customerID"))
            {
                this.CustomerID = (int)session.GetInt32("customerID");
            }

            if (session.Keys.Contains("adressID"))
            {
                this.AdressID = (int)session.GetInt32("adressID");
            }

            if (session.Keys.Contains("userName"))
            {
                this.UserName = session.GetString("userName");
            }


            return this;
        }

        public void LogOutSession(ISession session)
        {
            session.Clear();
        }

        public void AttemptLogin(ISession session)
        {
            if (session.Keys.Contains("attempts"))
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
            if (!session.Keys.Contains("attempts"))
            {
                session.SetInt32("attempts", 0);
            }
            return (int)session.GetInt32("attempts");
        }

    }
}
