using AutoMapper;
using EntityDataAccess.Core;
using EntityObjects.Objects;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using TransferModules;
using TransferModules.Buyer;
using TransferModules.Model;
using UtilityServices;

namespace Api.LifeInsuranceDeal.Controllers
{
    [EnableCors(headers: "*", origins: "*", methods: "*")]
    [RoutePrefix("dataConverstion")]
    public class HomeController : ApiController
    {
        EFDbContext _dbContext = new EFDbContext();
        TransferServices _tlogService = new TransferServices();

        [HttpGet, Route("test", Name = "test")]
        public IHttpActionResult test()

        {
            var prod = new ProductMaster()
            {
                GenerateDate = DateTime.Now,
                Name = "Funeral",
                State = true,

            };
            var Buyer = new BuyerMaster()
            {
                ApiUrl = "",
                CompanyName = "Syne Craft",
                Email = "Test@tst.com",
                Name = "SyneCraft",
                State = EntityObjects.Enaum.EntityState.active,
                CreatedDateTime = DateTime.Now,


            };
            var buyerProd = new BuyerProduct()
            {
                BuyerApiUrl = "https://secure.traffic-clearing-house.com/Leadsdb/direct_api/tch_xmlhttp.php",
                BuyerMasterId = 2,
                EndDate = DateTime.Now.AddDays(25),
                IntegrationType = EntityObjects.Enaum.BuyerIntegrationType.xml,
                ProductMasterId = 1,
                Piority = 1,
                Quata = 100,
                StartDate = DateTime.Now,
                State = true,


            };
            //var buyer = new BuyerMaster()
            //{
            //    ApiUrl= "http://leadbee.leadspediatrack.com/post.do",
            //    CompanyName ="SuneCraft",
            //     CreatedDateTime = DateTime.Now,

            //};
            // _dbContext.BuyerProducts.Add(buyerProd);
            //  _dbContext.SaveChanges();
            var v = _dbContext.BuyerMasters.ToList();
            // var v = _dbContext.CommonLeads.ToList().OrderByDescending(m=>m.Id).Where(m=>m.ProductName == "Funeral");
            // _dbContext.ProductMasters.Add(prod);
            // _dbContext.BuyerMasters.Add(Buyer);
            //  _dbContext.BuyerProducts.Add(buyerProd);
            //   _dbContext.SaveChanges();

            // CommonLead cmn = UtilityMethods.GetCommonLeads(1276);
            // cmn.Email = "rsurry@rjminternationall.co";
            // _dbContext.Entry(cmn).State = EntityState.Modified;
            // _dbContext.SaveChanges();
            // LeadTransferById(1276);

            //LifeLead life = UtilityMethods.GetProductLeadByLeadId<LifeLead>(1276, "Life");
            //life.DOB = new DateTime(1865, 2, 12);
            //_dbContext.Entry(life).State = EntityState.Modified;
            //_dbContext.SaveChanges();

            var vc = _dbContext.CommonLeads.Where(m => m.Id == 2321).ToList();
            //  BuyerProduct bPod = _dbContext.BuyerProducts.Where(m => m.Id == 4).FirstOrDefault();
            //  bPod.BuyerApiUrl = "http://leadbee.leadspediatrack.com/post.do";
            //   _dbContext.Entry(bPod).State = EntityState.Modified;
            //  _dbContext.SaveChanges();

            LeadTransfer(2387);
            return Ok(vc);
        }

        [HttpPost, Route("capture", Name = "CaptureLead")]
        public IHttpActionResult CaptureLead(AllFieldCapture lead)
        {

            try
            {
                string prodName = UtilityMethods.GetProductName(lead.ProductName);

                CommonLead cmnLead = Mapper.Map<CommonLead>(lead);
                if (cmnLead.FirstName.Contains("test") || cmnLead.LastName.Contains("test") || cmnLead.FirstName.ToLower().Contains("test"))
                {
                    cmnLead.Status = EntityObjects.Enaum.LeadType.test;
                }
                _dbContext.CommonLeads.Add(cmnLead);
                _dbContext.SaveChanges();
                if (prodName == GlobalConstant.Life)
                {

                    LifeLead lifeLead = Mapper.Map<LifeLead>(lead);
                    lifeLead.CommonLeadId = cmnLead.Id;
                    _dbContext.LifeLeads.Add(lifeLead);
                }
                else if (prodName == GlobalConstant.Funeral)
                {

                    FuneralLead funeralLead = Mapper.Map<FuneralLead>(lead);
                    funeralLead.CommonLeadId = cmnLead.Id;
                    _dbContext.FuneralLeads.Add(funeralLead);
                }
                _dbContext.SaveChanges();
                if (cmnLead.FirstName.Contains("test"))
                {

                    return Ok(new { result = "success", ref_id = cmnLead.Id });

                }
                if (cmnLead.FirstName.Contains("Test"))
                {

                    return Ok(new { result = "success", ref_id = cmnLead.Id });

                }

                LeadTransfer(cmnLead.Id);

                return Ok(new { result = "success", ref_id = cmnLead.Id });

            }
            catch (Exception ex)
            {
                return BadRequest("faild");
            }
            return Ok();
        }

        [HttpGet, Route("leadTransfer", Name = "LeadTransfer")]
        public IHttpActionResult LeadTransfer(int leadId = 0)
        {
            try
            {
                var cmnLead = UtilityMethods.GetCommonLeads(leadId);
                if (cmnLead != null)
                {
                    TransferLog tlog = _tlogService.LeadTransferToBuyer(cmnLead);
                    _dbContext.TransferLogs.Add(tlog);
                    _dbContext.SaveChanges();

                    //will change login below
                   
                }
            }
            catch (Exception e)
            {

            }
            return Ok(new { result = "success", ref_id = leadId });

        }

        [HttpGet, Route("leadTransferById", Name = "LeadTransferById")]
        public IHttpActionResult LeadTransferById(int leadid = 0)
        {
            try
            {
                var cmnLead = UtilityMethods.GetCommonLeads(leadid);
                if (cmnLead != null)
                {
                    TransferLog tlog = _tlogService.LeadTransferToBuyer(cmnLead);
                    _dbContext.TransferLogs.Add(tlog);
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {

            }

            return Ok();
        }
    }
}
