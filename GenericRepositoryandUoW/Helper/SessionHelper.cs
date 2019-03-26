using System;
using System.Web;

namespace GenericRepositoryandUoW.Helper
{
    public static class SessionHelper
    {
        public static object GetSessionObject(string name)
        {
            try
            {
                return HttpContext.Current.Session[name];
            }
            catch (Exception ex)
            {
                return null;
                //throw new ApplicationException("Failed to get the session. Exception Message: " + ex.Message);
            }
        }
    }
}
