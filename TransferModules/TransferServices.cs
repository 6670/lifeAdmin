using EntityDataAccess.Core;
using EntityObjects.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransferModules.Buyer;
using TransferModules.Model;
using UtilityServices;

namespace TransferModules
{
    public class TransferServices
    {
        TransferLog tlog = new TransferLog();
        EFDbContext _db = new EFDbContext();
        public TransferLog LeadTransferToBuyer(CommonLead cmnlead)
        {
            MatchBuyer matchBuyer = GetMatchBuyer(cmnlead);


            if (GlobalConstant.BuyerVM.Equals(matchBuyer.BuyerName))
            {
                tlog = VM.SendLead(cmnlead, matchBuyer);
                return tlog;
            }
            else if (GlobalConstant.BuyerSyneCraft.Equals(matchBuyer.BuyerName))
            {
                tlog = SyneCraft.SendLead(cmnlead, matchBuyer);
                return tlog;
            }

            return null;
        }

        private MatchBuyer GetMatchBuyer(CommonLead cmnlead)
        {
            var prod = _db.ProductMasters.Where(m => m.Name.Equals(cmnlead.ProductName)).FirstOrDefault();
            var tlog = _db.TransferLogs.Where(m => m.CommonLeadId == cmnlead.Id).ToList();



            var matchBuyer = (from c in _db.BuyerProducts

                              where c.State == true && c.ProductMasterId == prod.Id
                              
                              orderby c.Piority descending
                              select new MatchBuyer()
                              {
                                  BuyerId = c.BuyerMasterId,
                                  BuyerName = c.BuyerMaster.Name,
                                  IntegtaionType = c.IntegrationType,
                                  BuyerApiUrl = c.BuyerApiUrl
                              }).FirstOrDefault();
            return matchBuyer;
        }


    }
}