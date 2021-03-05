using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UCM;
using Dashboard.Entities;

namespace Dashboard.Handlers
{
    public class GetEnv : IRequest<string>
    {
        private DashboardEntities db = new DashboardEntities();

        public string Handle()
        {
            var conString = db.Database.Connection.ConnectionString;
            return conString.Contains("sqldev") ? "logo-dev" :
                   conString.Contains("sqlstg") ? "logo-stg" :
                   conString.Contains("sqlprd") ? "logo-prd" : 
                   "";
        }
    }
}