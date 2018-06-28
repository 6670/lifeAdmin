using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSeearchMediaService.Model
{
    public interface IRepository
    {
        int LeadSend(string url);
    }

}
