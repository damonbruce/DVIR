using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RailDelivery.Core.Models;

namespace RailDelivery.Core.Query.Interface
{
    public interface IPowerQuery
    {
        List<Powered> FindPowered(Search searchModel);
        Powered GetPowered(int powerId);
    }
}
