
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
using TransferModules.Model;
using UtilityServices;

namespace TransferModules.Buyer
{
    public class VM
    {
        BuyerMaster _buyer;
           

        public static TransferLog SendLead(CommonLead cmnlead,MatchBuyer matchBuyer)
        {
            if (matchBuyer.IntegtaionType.Equals(BuyerIntegrationType.xml))
            {
               TransferLog tlog= SendXML(cmnlead,matchBuyer);
                return tlog;

            }
            return null;
        }
        internal static TransferLog SendXML(CommonLead cmnLead,MatchBuyer matchBuyer)
        {
            TransferLog tLog = null;
            DateTime generatedDateTime = new DateTime();
            string serverPost = "";

            if (cmnLead.ProductName.Equals(GlobalConstant.Life))//Life
            {
                LifeLead prodLead = UtilityMethods.GetProductLeadByLeadId<LifeLead>(cmnLead.Id, cmnLead.ProductName);
                generatedDateTime = DateTime.Now;
                serverPost = BuildLifeURL(cmnLead, prodLead,matchBuyer);
            }
           else if(cmnLead.ProductName.Equals(GlobalConstant.Funeral))// funeral
            {
                FuneralLead prodLead = UtilityMethods.GetProductLeadByLeadId<FuneralLead>(cmnLead.Id, cmnLead.ProductName);
                generatedDateTime = DateTime.Now;
                serverPost = BuildFuneralURL(cmnLead, prodLead, matchBuyer);
            }
            else if(cmnLead.ProductName.Equals(GlobalConstant.Health))// Health
            {
                HealthLead prodLead = UtilityMethods.GetProductLeadByLeadId<HealthLead>(cmnLead.Id, cmnLead.ProductName);
                generatedDateTime = DateTime.Now;
                serverPost = BuildHealthURL(cmnLead, prodLead, matchBuyer);
            }
            else if (cmnLead.ProductName.Equals(GlobalConstant.CorporateHealth))//Corporate Health
            {
                CorporateHealthLead prodLead = UtilityMethods.GetProductLeadByLeadId<CorporateHealthLead>(cmnLead.Id, cmnLead.ProductName);
                generatedDateTime = DateTime.Now;
                serverPost = BuildaCorporateHealthURL(cmnLead, prodLead, matchBuyer);
            }
            if (serverPost != "")
            {
                tLog = SendResponse(cmnLead.Id, generatedDateTime, serverPost,matchBuyer);
                // Send text to customer and buyer after successful transferred 
               
                return tLog;
            }

            return null;
        }
        internal static TransferLog SendResponse(int leadId, DateTime generatedDateTime, string serverPost,MatchBuyer matchBuyer)
        {
            TransferLog tLog = null;
            int bytelength = 0;
            var encoder = new ASCIIEncoding();
            Stream requestStream;
            Stream responseStream;
            StreamReader responseReader;
            HttpWebRequest webRequest;
            WebResponse webResponse;

            try
            {
                bytelength = encoder.GetBytes(serverPost).Length;

                //Get WebRequest Object initialised 
                webRequest = UtilityMethods.GetWebRequestQuery(serverPost,matchBuyer.BuyerApiUrl);

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

                var translatedResponse = responseReader.ReadToEnd();
                var json = JsonConvert.DeserializeObject<dynamic>(translatedResponse);
                string status = json.status;

                //TODO: Update log with succesful status

                webResponse.Close();

                if (status == "success")
                {
                    string buyerReturnLeadid = json.lead_id;
                    double price = json.commission ?? 0;
                    tLog = new TransferLog()
                    {
                        GeneratedDatetime = generatedDateTime,
                        TransferDatetime = DateTime.Now,
                       // BuyerIntegrationTypeID = _buyer.IntegrationType,
                        CommonLeadId = leadId,
                        BuyerReturnLeadId = buyerReturnLeadid,
                        Status = LeadType.transferred,
                      //  UserId = _userId,
                        Description = "Successful - " + translatedResponse,
                        Price = 0,
                        IsActualPrice = price <= 0 ? false : true,
                      //  ProductSlabId = _buyer.SlabId,
                        BuyerMasterId= matchBuyer.BuyerId,
                      //  BuyerProductScheduleId = _buyer.ProductScheduleId
                    };
                }
                else if(status == "error")
                {
                    tLog = new TransferLog()
                    {
                        GeneratedDatetime = generatedDateTime,
                        TransferDatetime = DateTime.Now,
                       // BuyerIntegrationTypeID = matchBuyer.IntegtaionType,
                        CommonLeadId = leadId,
                        BuyerReturnLeadId = "",
                        Status = LeadType.error,
                       // UserId = _userId,
                        Description = "Failed - " + translatedResponse,
                        Price = 0,
                        IsActualPrice = false,
                       // ProductSlabId = _buyer.SlabId,
                        BuyerMasterId = matchBuyer.BuyerId,
                      //  BuyerProductScheduleId = _buyer.ProductScheduleId
                    };
                }
                else if (status == "warning")
                {
                    tLog = new TransferLog()
                    {
                        GeneratedDatetime = generatedDateTime,
                        TransferDatetime = DateTime.Now,
                        // BuyerIntegrationTypeID = matchBuyer.IntegtaionType,
                        CommonLeadId = leadId,
                        BuyerReturnLeadId = "",
                        Status = LeadType.duplicate,
                        // UserId = _userId,
                        Description = "Duplicate - " + translatedResponse,
                        Price = 0,
                        IsActualPrice = false,
                        // ProductSlabId = _buyer.SlabId,
                        BuyerMasterId = matchBuyer.BuyerId,
                        //  BuyerProductScheduleId = _buyer.ProductScheduleId
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
                   // ProductSlabId = _buyer.SlabId,
                    BuyerMasterId = matchBuyer.BuyerId,
                   // BuyerProductScheduleId = _buyer.ProductScheduleId
                };
            }
            catch (Exception ex)
            {
                tLog = new TransferLog()
                {
                    GeneratedDatetime = generatedDateTime,
                    TransferDatetime = DateTime.Now,
                    // BuyerIntegrationTypeID = matchBuyer.IntegtaionType,
                    CommonLeadId = leadId,
                    BuyerReturnLeadId = "",
                    Status = LeadType.error,
                    // UserId = _userId,
                    Description = "Failed - " + ex,
                    Price = 0,
                    IsActualPrice = false,
                    // ProductSlabId = _buyer.SlabId,
                    BuyerMasterId = matchBuyer.BuyerId,
                  //  BuyerProductScheduleId = _buyer.ProductScheduleId
                };
            }
            return tLog;
        }
        internal static string BuildFuneralURL(CommonLead cmnLead, FuneralLead prodLead, MatchBuyer matchbuyer)
        {
            var obj = new
            {
                affiliate_campaign_id = 325,
                vmform_hash = "2EAEE",
                vmform_ip = cmnLead.IpAddress,
                vmform_referer = "https://topfuneralplan.co.uk",
                vmform_siteid = 1517,
                vmform_source = "ppcsearch",

                first_name = cmnLead.FirstName,
                last_name = cmnLead.LastName,
                address_line_1 = cmnLead.Address,
                postcode = cmnLead.PostCode,
                dob = prodLead.DOB.ToString("dd-MM-yyyy"),
                telephone = cmnLead.HomePhone,
                email = cmnLead.Email
            };
            string queryString = UtilityMethods.GetQueryString(obj);
            return queryString;
        }

        internal static string BuildLifeURL(CommonLead cmnLead, LifeLead prodLead,MatchBuyer matchbuyer)
        {
            var src = "ppcsearch";
            try
            {
                if (!string.IsNullOrWhiteSpace(cmnLead.Source) && cmnLead.Source.Contains("facebook"))
                {
                    src = "facebook";
                }
            }
            catch (Exception ex)
            {

            }
                ;

            var obj = new
            {
                affiliate_campaign_id = 273,
                vmform_hash = "03AEE",
                vmform_ip = cmnLead.IpAddress.Trim(),
                vmform_referer = "https://lifeinsurancedeal.co.uk",
                vmform_siteid = 1406,
                vmform_source = src,
                joint_application = prodLead.SingleOwnership ? "No" : "Yes",
                insurance_type = "Life Insurance only",
                cover_amount = prodLead.CoverAmount,
                cover_length = prodLead.CoverPeriod,
                first_name = cmnLead.FirstName.Trim(),
                last_name = cmnLead.LastName.Trim(),
                address_line_1 = cmnLead.Address.Trim(),
                postcode = cmnLead.PostCode,
                dob = prodLead.DOB.ToString("dd-MM-yyyy"),
                smoker = prodLead.Smoker ? "Yes" : "No",
                telephone = cmnLead.HomePhone,
                mobile = cmnLead.WorkPhone,
                email = cmnLead.Email.Trim()
            };
            string queryString = UtilityMethods.GetQueryString(obj);
            return queryString;
        }

        private static string BuildaCorporateHealthURL(CommonLead cmnLead, CorporateHealthLead prodLead, MatchBuyer matchBuyer)
        {

            //Product-Specific Fields
            var obj = new
            {
                affiliate_campaign_id = 340,
                vmform_hash = "06488",
                vmform_ip = cmnLead.IpAddress.Trim(),
                vmform_referer = "https://www.bestmedicalplan.co.uk/gbu/business-medical-plan",
                vmform_siteid = 1567,
                vmform_source = "ppcsearch",

                //company_name = prodLead.CompanyName,
                employees = 20,
               // current_policy = prodLead.ExistingPolicy ? "yes" : "no",

                first_name = cmnLead.FirstName.Trim(),
                last_name = cmnLead.LastName.Trim(),
                gender = cmnLead.Title = "Mr" ?? "Male",
              //  dob = prodLead.Dob.ToString("dd-MM-yyyy"),
               // address_line_1 = cmnLead.Address1,
               // postcode = cmnLead.Postcode,

                telephone = cmnLead.HomePhone.Trim(),
                //mobile = cmnLead.WorkPhone.Trim(),
                email = cmnLead.Email.Trim()
            };

            string queryString = UtilityMethods.GetQueryString(obj);
            return queryString;
        }

        internal static string BuildHealthURL(CommonLead cmnLead, HealthLead prodLead,MatchBuyer matchBuyer)
        {

            //Product-Specific Fields
            var obj = new
            {
                affiliate_campaign_id = 196,
                vmform_hash = "E78E7",
                vmform_ip = cmnLead.IpAddress.Trim(),
                vmform_referer = "https://www.bestmedicalplan.co.uk",
                vmform_siteid = 1276,
                vmform_source = "ppcsearch",
              //  cover_for = GetHealthCover(prodLead.CoverTypeHealth),
                gender = cmnLead.Title == "Mr" ? "Male" : "Female",
              //  current_policy = prodLead.ExistingPolicy ? "yes_myself" : "no",
              //  smoker = prodLead.Smoker ? "Yes" : "No",
                first_name = cmnLead.FirstName.Trim(),
                last_name = cmnLead.LastName.Trim(),
               address_line_1 = cmnLead.Address,
              //postcode = prodLead.Postcode,
               // dob = prodLead.Dob.ToString("dd-MM-yyyy"),
                telephone = cmnLead.HomePhone.Trim(),
                mobile = cmnLead.WorkPhone.Trim(),
                email = cmnLead.Email.Trim()
            };

            string queryString = UtilityMethods.GetQueryString(obj);
            return queryString;
        }
    }
}