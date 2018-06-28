using EntityObjects.Enaum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransferModules.Model
{
    public class MatchBuyer
    {
        public int BuyerId { get; set; }
        public string BuyerName { get; set; }
        public BuyerIntegrationType IntegtaionType { get; set; }
        public string BuyerApiUrl { get; set; }
    }
}