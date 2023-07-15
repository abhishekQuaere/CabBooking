using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CabBooking.Utilities
{
    public class SessionManager : IDisposable
    {
        public void RemoveSession()
        {
            try
            {
                HttpContext.Current.Session.Clear();
                HttpContext.Current.Session.Abandon();
                HttpContext.Current.Session.RemoveAll();
            }
            catch
            { }
        }

        public string userName
        {
            get
            {
                if (HttpContext.Current.Session["userName"] != null)
                {
                    return HttpContext.Current.Session["userName"].ToString();
                }
                else
                {
                    return null;
                }
            }
            set
            {
                HttpContext.Current.Session["userName"] = value;
            }
        }

        public Int64 userId
        {
            get
            {
                if (HttpContext.Current.Session["userId"] != null)
                {
                    return Convert.ToInt64(HttpContext.Current.Session["userId"].ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                HttpContext.Current.Session["userId"] = value;
            }
        }

        public Int64 ExternalId
        {
            get
            {
                if (HttpContext.Current.Session["ExternalId"] != null)
                {
                    return Convert.ToInt64(HttpContext.Current.Session["ExternalId"].ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                HttpContext.Current.Session["ExternalId"] = value;
            }
        }

        public Int64 divisionId
        {
            get
            {
                if (HttpContext.Current.Session["divisionId"] != null)
                {
                    return Convert.ToInt64(HttpContext.Current.Session["divisionId"].ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                HttpContext.Current.Session["divisionId"] = value;
            }
        }

        public Int64 profileId
        {
            get
            {
                if (HttpContext.Current.Session["profileId"] != null)
                {
                    return Convert.ToInt64(HttpContext.Current.Session["profileId"].ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                HttpContext.Current.Session["profileId"] = value;
            }
        }

        public Int32 roleId
        {
            get
            {
                if (HttpContext.Current.Session["roleId"] != null)
                {
                    return Convert.ToInt32(HttpContext.Current.Session["roleId"].ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                HttpContext.Current.Session["roleId"] = value;
            }
        }

        public string roleName
        {
            get
            {
                if (HttpContext.Current.Session["roleName"] != null)
                {
                    return HttpContext.Current.Session["roleName"].ToString();
                }
                else
                {
                    return "";
                }
            }
            set
            {
                HttpContext.Current.Session["roleName"] = value;
            }
        }

        public string displayName
        {
            get
            {
                if (HttpContext.Current.Session["displayName"] != null)
                {
                    return HttpContext.Current.Session["displayName"].ToString();
                }
                else
                {
                    return "";
                }
            }
            set
            {
                HttpContext.Current.Session["displayName"] = value;
            }
        }

        public string Email
        {
            get
            {
                if (HttpContext.Current.Session["Email"] != null)
                {
                    return HttpContext.Current.Session["Email"].ToString();
                }
                else
                {
                    return "";
                }
            }
            set
            {
                HttpContext.Current.Session["Email"] = value;
            }
        }

        public string DIdiscomId
        {
            get
            {
                if (HttpContext.Current.Session["DIdiscomId"] != null)
                {
                    return HttpContext.Current.Session["DIdiscomId"].ToString();
                }
                else
                {
                    return "";
                }
            }
            set
            {
                HttpContext.Current.Session["DIdiscomId"] = value;
            }
        }

        public string externalUserId
        {
            get
            {
                if (HttpContext.Current.Session["externalUserId"] != null)
                {
                    return HttpContext.Current.Session["externalUserId"].ToString();
                }
                else
                {
                    return "";
                }
            }
            set
            {
                HttpContext.Current.Session["externalUserId"] = value;
            }
        }

        public string Organization
        {
            get
            {
                if (HttpContext.Current.Session["Organization"] != null)
                {
                    return HttpContext.Current.Session["Organization"].ToString();
                }
                else
                {
                    return "";
                }
            }
            set
            {
                HttpContext.Current.Session["Organization"] = value;

            }
        }

        public string lastLoginDate
        {
            get
            {
                if (HttpContext.Current.Session["lastLoginDate"] != null)
                {
                    return HttpContext.Current.Session["lastLoginDate"].ToString();
                }
                else
                {
                    return "";
                }
            }
            set
            {
                HttpContext.Current.Session["lastLoginDate"] = value;
            }
        }

        public string ProfilePicUrl
        {
            get
            {
                if (HttpContext.Current.Session["ProfilePicUrl"] != null)
                {
                    return HttpContext.Current.Session["ProfilePicUrl"].ToString();
                }
                else
                {
                    return "";
                }
            }
            set
            {
                HttpContext.Current.Session["ProfilePicUrl"] = value;

            }
        }

        public string OfficeName
        {
            get
            {
                if (HttpContext.Current.Session["OfficeName"] != null)
                {
                    return HttpContext.Current.Session["OfficeName"].ToString();
                }
                else
                {
                    return "";
                }
            }
            set
            {
                HttpContext.Current.Session["OfficeName"] = value;

            }
        }

        public Int64 RegistraionId
        {
            get
            {
                if (HttpContext.Current.Session["RegistraionId"] != null)
                {
                    return Convert.ToInt64(HttpContext.Current.Session["RegistraionId"].ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                HttpContext.Current.Session["RegistraionId"] = value;

            }
        }
        
        public string UserExceptionSession
        {
            get
            {
                if (HttpContext.Current.Session["UserExceptionSession"] != null)
                {
                    return HttpContext.Current.Session["UserExceptionSession"].ToString();
                }
                else
                {
                    return "";
                }
            }
            set
            {
                HttpContext.Current.Session["UserExceptionSession"] = value;

            }
        }

        public string MobileNo
        {
            get
            {
                if (HttpContext.Current.Session["MobileNo"] != null)
                {
                    return HttpContext.Current.Session["MobileNo"].ToString();
                }
                else
                {
                    return "";
                }
            }
            set
            {
                HttpContext.Current.Session["MobileNo"] = value;
            }
        }

        public int Size => 30;

        public DataTable UserPermissionDt
        {
            get
            {
                if (HttpContext.Current.Session["Permissions"] == null)
                {
                    return null;
                }
                else
                {
                    return (DataTable)HttpContext.Current.Session["Permissions"];
                }
            }
            set
            {
                HttpContext.Current.Session["Permissions"] = value;
            }
        }

        public DataTable PhamaType
        {
            get
            {
                if (HttpContext.Current.Session["PhamaType"] == null)
                {
                    return null;
                }
                else
                {
                    return (DataTable)HttpContext.Current.Session["PhamaType"];
                }
            }
            set
            {
                HttpContext.Current.Session["PhamaType"] = value;
            }
        }

        public string accessToken
        {
            get
            {
                if (HttpContext.Current.Session["accessToken"] != null)
                {
                    return HttpContext.Current.Session["accessToken"].ToString();
                }
                else
                {
                    return "";
                }
            }
            set
            {
                HttpContext.Current.Session["accessToken"] = value;

            }
        }
        
        public string refreshToken
        {
            get
            {
                if (HttpContext.Current.Session["refreshToken"] != null)
                {
                    return HttpContext.Current.Session["refreshToken"].ToString();
                }
                else
                {
                    return "";
                }
            }
            set
            {
                HttpContext.Current.Session["refreshToken"] = value;

            }
        }

        public void Dispose()
        {

        }
    }
}