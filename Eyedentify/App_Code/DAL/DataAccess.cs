using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Common;
using System.Web.Caching;

namespace Eyedentify.App_Code
{
    public class DataAccess
    {

        private string _connectionString = "";
        protected string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        private bool _enableCaching = true;
        protected bool EnableCaching
        {
            get { return _enableCaching; }
            set { _enableCaching = value; }
        }

        private int _cacheDuration = 0;
        protected int CacheDuration
        {
            get { return _cacheDuration; }
            set { _cacheDuration = value; }
        }

        protected Cache Cache
        {
            get { return HttpContext.Current.Cache; }
        }

        protected int ExecuteNonQuery(DbCommand cmd)
        {
            return cmd.ExecuteNonQuery();
        }

        protected IDataReader ExecuteReader(DbCommand cmd)
        {
            return ExecuteReader(cmd, CommandBehavior.Default);
        }

        protected IDataReader ExecuteReader(DbCommand cmd, CommandBehavior behavior)
        {
            return cmd.ExecuteReader(behavior);
        }

        protected object ExecuteScalar(DbCommand cmd)
        {
            return cmd.ExecuteScalar();
        }
    }
}