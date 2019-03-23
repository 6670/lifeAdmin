using EntityDataAccess.Core;
using EntityObjects.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using UtilityServices;

namespace UtilityServices
{
    public static class UtilityMethods
    {
       public static EFDbContext  _dbContext = new EFDbContext();

       

        public static string GetProductName(string prodName)
        {
            if (GlobalConstant.Life.Equals(prodName))
            {
                return GlobalConstant.Life;


            }
            else if (GlobalConstant.Funeral.Equals(prodName))
            {
                return GlobalConstant.Funeral;

             
            }
            else if (GlobalConstant.Health.Equals(prodName))
            {
                return GlobalConstant.Health;

                
            }
            else if (GlobalConstant.CorporateHealth.Equals(prodName))
            {
                return GlobalConstant.CorporateHealth;

               
            }
            return null;
        }
        public static T GetProductLeadByLeadId<T>(int leadId, string parentProductName) where T : class
        {
            using (var context = new EFDbContext())
            {
                if (parentProductName == GlobalConstant.Life)
                {
                    LifeLead prodLead = context.LifeLeads.FirstOrDefault(x => x.CommonLeadId == leadId);
                    return (T)Convert.ChangeType(prodLead, typeof(T));
                }
                else if (parentProductName.Equals(GlobalConstant.Funeral))
                {
                    FuneralLead prodLead = context.FuneralLeads.FirstOrDefault(x => x.CommonLeadId == leadId);
                    return (T)Convert.ChangeType(prodLead, typeof(T));
                }
                else if (parentProductName.Equals(GlobalConstant.Health))
                {
                    HealthLead prodLead = context.HealthLeads.FirstOrDefault(x => x.CommonLeadId == leadId);
                    return (T)Convert.ChangeType(prodLead, typeof(T));
                }

                //should add other product
                return (T)Convert.ChangeType(null, typeof(T));
            }
        }

        public static CommonLead GetCommonLeads(int leadId = 0)
        {
            var cmmLead = _dbContext.CommonLeads.Where(m => m.Id == leadId).FirstOrDefault();
            return cmmLead;
        }

    
        public static string GetQueryString(object obj)
        {
            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return String.Join("&", properties.ToArray());
        }
        public static HttpWebRequest GetWebRequestQuery(string serverPost, string accessURL)
        {
            var buyerWebRequest = (HttpWebRequest)WebRequest.Create(accessURL);

            buyerWebRequest.ContentType = "application/x-www-form-urlencoded";
            buyerWebRequest.Method = "POST";
            buyerWebRequest.Referer = serverPost;

            return buyerWebRequest;
        }

        public static HttpWebRequest GetWebRequestXML(string serverPost, string accessURL)
        {
            var buyerWebRequest = (HttpWebRequest)WebRequest.Create(accessURL);

            buyerWebRequest.ContentType = "text/xml;charset=UTF-8";
            buyerWebRequest.Method = "POST";

            return buyerWebRequest;
        }

    }
}