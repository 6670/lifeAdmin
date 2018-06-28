using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSeearchMediaService.Model;

namespace WebSeearchMediaService
{
    public class CaptureService
    {
        protected IRepository _repository = null;
        public CaptureService()
        {
            _repository = new Repository();
        }
        public int LeadTransfer(string url)
        {
            return _repository.LeadSend(url);
        }
    }
}
