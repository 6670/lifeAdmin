using EntityObjects.Enaum;
using EntityObjects.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using TransferModules.Model;
using UtilityServices;

namespace TransferModules.Buyer
{
    public class SyneCraft
    {
        BuyerMaster _buyer;


        public static TransferLog SendLead(CommonLead cmnlead, MatchBuyer matchBuyer)
        {
            if (matchBuyer.IntegtaionType.Equals(BuyerIntegrationType.xml))
            {
                TransferLog tlog = SendXML(cmnlead, matchBuyer);
                return tlog;

            }
            return null;
        }
        internal static TransferLog SendXML(CommonLead cmnLead, MatchBuyer matchBuyer)
        {
            TransferLog tLog = null;
            DateTime generatedDateTime = new DateTime();
            string serverPost = "";

            if (cmnLead.ProductName.Equals(GlobalConstant.Life))//Life
            {
                LifeLead prodLead = UtilityMethods.GetProductLeadByLeadId<LifeLead>(cmnLead.Id, cmnLead.ProductName);
                generatedDateTime = DateTime.Now;
                serverPost = BuildLifeURL(cmnLead, prodLead, matchBuyer);
            }


            if (serverPost != "")
            {
                tLog = SendResponse(cmnLead.Id, generatedDateTime, serverPost, matchBuyer);
                // Send text to customer and buyer after successful transferred 

                return tLog;
            }

            return null;
        }
        internal static TransferLog SendResponse(int leadId, DateTime generatedDateTime, string serverPost, MatchBuyer matchBuyer)
        {

            TransferLog tLog = null;
            int bytelength = 0;
            var encoder = new ASCIIEncoding();
            Stream requestStream;
            Stream responseStream;
            StreamReader responseReader;
            HttpWebRequest webRequest;
            WebResponse webResponse;
            string translatedResponse = string.Empty;

            try
            {
                bytelength = encoder.GetBytes(serverPost).Length;

                //Get WebRequest Object initialised 
                webRequest = UtilityMethods.GetWebRequestQuery(serverPost, matchBuyer.BuyerApiUrl);

                webRequest.ContentLength = bytelength;

                //Wrap request stream 
                requestStream = webRequest.GetRequestStream();

                //Save post url to stream
                requestStream.Write(encoder.GetBytes(serverPost), 0, encoder.GetBytes(serverPost).Length);

                requestStream.Close();

                webResponse = webRequest.GetResponse();
                responseStream = webResponse.GetResponseStream();
                responseReader = new StreamReader(responseStream);

                //Process Response    
                string status = "";
                string returnId = "";
                string retPrice = "";
                translatedResponse = responseReader.ReadToEnd();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(translatedResponse);
                XmlNodeList parentNode = xmlDoc.GetElementsByTagName("response");
                foreach (XmlNode childrenNode in parentNode)
                {
                    status = childrenNode.SelectSingleNode("//result").InnerText;
                    returnId = childrenNode.SelectSingleNode("//lead_id").InnerText;
                    retPrice = childrenNode.SelectSingleNode("//price").InnerText;

                }


                //TODO: Update log with succesful status

                webResponse.Close();

                if (status != "faild")
                {
                    int price =44;
                                                   
                    tLog = new TransferLog()
                    {
                        GeneratedDatetime = generatedDateTime,
                        TransferDatetime = DateTime.Now,
                     //   BuyerIntegrationTypeID = matchBuyer.IntegrationType,
                        CommonLeadId = leadId,
                        BuyerReturnLeadId = returnId,
                        Status = LeadType.transferred,
                       
                        Description = "Successful - " + translatedResponse,
                        Price = price <= 0 ? 0 : price,
                        BuyerMasterId = matchBuyer.BuyerId,
                        IsActualPrice = price <= 0 ? false : true,
                       
                    };
                }
                else
                {
                    tLog = new TransferLog()
                    {
                        GeneratedDatetime = generatedDateTime,
                        TransferDatetime = DateTime.Now,
                      //  BuyerIntegrationTypeID = _buyer.IntegrationType,
                        CommonLeadId = leadId,
                        BuyerReturnLeadId = "",
                        Status = LeadType.error,
                      //  UserId = _userId,
                        Description = "Failed - " + translatedResponse,
                        Price = 0,
                        IsActualPrice = false,
                      //  ProductSlabId = _buyer.SlabId,
                        BuyerMasterId = matchBuyer.BuyerId,
                       // BuyerProductScheduleId = _buyer.ProductScheduleId
                    };
                }
            }
            catch (WebException wex)
            {
                tLog = new TransferLog()
                {
                    GeneratedDatetime = generatedDateTime,
                    TransferDatetime = DateTime.Now,
                  //  BuyerIntegrationTypeID = _buyer.IntegrationType,
                    CommonLeadId = leadId,
                    BuyerReturnLeadId = "",
                    Status = LeadType.error,
                   // UserId = _userId,
                    Description = wex.Message,
                    Price = 0,
                    IsActualPrice = false,
                 //   ProductSlabId = _buyer.SlabId,
                    BuyerMasterId = matchBuyer.BuyerId,
                  //  BuyerProductScheduleId = _buyer.ProductScheduleId
                };
            }
            catch (Exception ex)
            {
                tLog = new TransferLog()
                {
                    GeneratedDatetime = generatedDateTime,
                    TransferDatetime = DateTime.Now,
                //    BuyerIntegrationTypeID = _buyer.IntegrationType,
                    CommonLeadId = leadId,
                    BuyerReturnLeadId = "",
                    Status = LeadType.error,
                   // UserId = _userId,
                    Description = ex.Message,
                    Price = 0,
                    IsActualPrice = false,
                   // ProductSlabId = _buyer.SlabId,
                    BuyerMasterId = matchBuyer.BuyerId,
                    //BuyerProductScheduleId = _buyer.ProductScheduleId
                };
            }
            return tLog;
        }
        internal static string BuildLifeURL(CommonLead cmnLead, LifeLead prodLead, MatchBuyer matchbuyer)
        {
            //initialise
            var ob = new
            {
                lp_campaign_id = "5ac60d402aed5",
                lp_campaign_key= "GHdng47p3TkxmbWzJNML",
                //lp_test =1,
                first_name=cmnLead.FirstName ?? string.Empty,
                last_name=cmnLead.LastName ?? string.Empty,
                life_insurance_type =prodLead.ProductType,
                cover_for= prodLead.SingleOwnership ? "Single" : "Joint",
                cover_size =prodLead.CoverAmount.ToString(),
                cover_length=prodLead.CoverPeriod.ToString(),
                tobacco =prodLead.Smoker ? "yes" : "No",
                dob_day=prodLead.DOB.Day,
                dob_month=prodLead.DOB.Month,
                dob_year=prodLead.DOB.Year,
                house_number = cmnLead.Address,
                postcode=cmnLead.PostCode,
                mobile=cmnLead.HomePhone,
                email_address =cmnLead.Email,

            };

            string queryString = UtilityMethods.GetQueryString(ob);
            return queryString;

        }
    }
}