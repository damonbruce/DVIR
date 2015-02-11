using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RailDelivery.Core.Models;

namespace RailDelivery.Core.Query.Interface
{
    public interface ITowedQuery
    {
        List<Towed> FindTowed(Search searchModel);
        Towed GetTowed(int towedId);
    }
}
